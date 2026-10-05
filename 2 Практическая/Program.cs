using _2_Практическая;
using System.Globalization;
using System.Text.RegularExpressions;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

string num = @"[+-]?\d*\.?\d+";

var rePoint = new Regex($@"^Point\(\s*({num})\s*,\s*({num})\s*\)$");
var reLine = new Regex($@"^Line\(\s*Point\(\s*({num})\s*,\s*({num})\s*\)\s*,\s*Point\(\s*({num})\s*,\s*({num})\s*\)\s*\)$");
var reCircle = new Regex($@"^Circle\(\s*Point\(\s*({num})\s*,\s*({num})\s*\)\s*,\s*({num})\s*\)$");

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

void ParseLine(string line, int lineNo)
{
    line = line.Trim();

    var m = rePoint.Match(line);
    if (m.Success)
    {
        double x = double.Parse(m.Groups[1].Value);
        double y = double.Parse(m.Groups[2].Value);
        shapes.Add(new Point(x,y));
        return;
    }

    m = reLine.Match(line);
    if (m.Success)
    {
        double x1 = double.Parse(m.Groups[1].Value);
        double y1 = double.Parse(m.Groups[2].Value);
        double x2 = double.Parse(m.Groups[3].Value);
        double y2 = double.Parse(m.Groups[4].Value);
        shapes.Add(new Line(new Point(x1,y1), new Point(x2,y2)));
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

        shapes.Add(new Circle(new Point(x, y), r));
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
        Console.WriteLine($"{shapes[i]}");
    }
}