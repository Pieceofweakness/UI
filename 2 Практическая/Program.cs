using _2_Практическая;
using System.Globalization;
using System.Text.RegularExpressions;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

string num = @"[+-]?\d*\.?\d+";
string intNum = @"\d+";
string color = $@"\s+color\s*=\s*\(\s*({intNum})\s*,\s*({intNum})\s*,\s*({intNum})\s*\)";

var rePoint = new Regex($@"^Point\(\s*({num})\s*,\s*({num})\s*\){color}$");
var reLine = new Regex($@"^Line\(\s*Point\(\s*({num})\s*,\s*({num})\s*\)\s*,\s*Point\(\s*({num})\s*,\s*({num})\s*\)\s*\){color}$");
var reCircle = new Regex($@"^Circle\(\s*Point\(\s*({num})\s*,\s*({num})\s*\)\s*,\s*({num})\s*\){color}$");
var reTriangle = new Regex($@"^Triangle\(\s*Point\(\s*({num})\s*,\s*({num})\s*\)\s*,\s*Point\(\s*({num})\s*,\s*({num})\s*\)\s*,\s*Point\(\s*({num})\s*,\s*({num})\s*\)\s*\){color}$");



string filePath = null;
var shapes = new List<Shape>();

while (true)
{
    Console.WriteLine("\nМеню");
    Console.WriteLine("-f (задать файл)");
    Console.WriteLine("-o print (напечатать фигуры)");
    Console.WriteLine("-o count (посчитать фигуры)");
    Console.WriteLine("exit (выход)");
    Console.Write("\nВыбор: ");

    string choice = Console.ReadLine().Trim();

    switch (choice)
    {
        case "-f":
            Console.Write("Путь к файлу: ");
            string path = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(path))
            {
                LoadFile(path);
            }
            break;

        case "-o print":
            PrintShapes();
            break;

        case "-o count":
            Console.WriteLine($"\nКоличество фигур: {shapes.Count}");
            break;

        case "exit":
            return 0;

        default:
            Console.WriteLine("\nНеверный пункт меню");
            break;
    }
}

void LoadFile(string path)
{
    if (!File.Exists(path))
    {
        Console.WriteLine($"\nФайл не найден: {path}");
        return;
    }

    filePath = path;
    shapes.Clear();

    int lineNo = 0;
    foreach (var raw in File.ReadAllLines(path))
    {
        lineNo++;
        var line = raw.Trim();

        if (line.Length == 0)
        {
            continue;
        }
        ParseLine(line, lineNo);
    }

    Console.WriteLine($"\nВсе возможные фигуры загружены");
}
RGB ParseColor(Match m, int colorStart)
{
    int r = int.Parse(m.Groups[colorStart].Value);
    int g = int.Parse(m.Groups[colorStart + 1].Value);
    int b = int.Parse(m.Groups[colorStart + 2].Value);

    return new RGB(r, g, b);
}


void ParseLine(string line, int lineNo)
{
    line = line.Trim();

    var m = rePoint.Match(line);
    if (m.Success)
    {
        double x = double.Parse(m.Groups[1].Value);
        double y = double.Parse(m.Groups[2].Value);
        
        var p = new Point(x, y);
        p.Color = ParseColor(m, 3);
        shapes.Add(p);
        return;
    }

    m = reLine.Match(line);
    if (m.Success)
    {
        double x1 = double.Parse(m.Groups[1].Value);
        double y1 = double.Parse(m.Groups[2].Value);
        double x2 = double.Parse(m.Groups[3].Value);
        double y2 = double.Parse(m.Groups[4].Value);

        var ln = new Line(new Point(x1, y1), new Point(x2, y2));
        ln.Color = ParseColor(m, 5);
        shapes.Add(ln);
        return;
    }

    m = reCircle.Match(line);
    if (m.Success)
    {
        double x = double.Parse(m.Groups[1].Value);
        double y = double.Parse(m.Groups[2].Value);
        double r = double.Parse(m.Groups[3].Value);


        if (r < 0)
        {
            Console.WriteLine($"Строка {lineNo}: отрицательный радиус");
            return;
        }

        var c = new Circle(new Point(x, y), r);
        c.Color = ParseColor(m, 4);
        shapes.Add(c);
        return;
    }

    m = reTriangle.Match(line);
    if (m.Success)
    {
        double x1 = double.Parse(m.Groups[1].Value);
        double y1 = double.Parse(m.Groups[2].Value);
        double x2 = double.Parse(m.Groups[3].Value);
        double y2 = double.Parse(m.Groups[4].Value);
        double x3 = double.Parse(m.Groups[5].Value);
        double y3 = double.Parse(m.Groups[6].Value);

        var tri = new Triangle(new Point(x1, y1), new Point(x2, y2), new Point(x3, y3));

        tri.Color = ParseColor(m, 7);
        shapes.Add(tri);
        return;
    }


    Console.WriteLine($"Строка {lineNo}: некорректно \"{line}\"");

}

void PrintShapes()
{
    if (shapes.Count == 0)
    {
        Console.WriteLine("\nСписок пуст. Сначала загрузите файл");
        return;
    }

    Console.WriteLine();
    for (int i = 0; i < shapes.Count; i++)
    {
        Console.WriteLine($"{shapes[i]} | {shapes[i].Color}");
    }
}