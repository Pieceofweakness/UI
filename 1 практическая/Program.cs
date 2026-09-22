using _1_практическая;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

List<Patient> patients = new List<Patient>();


while (true)
{
    Console.WriteLine("\nВведите пункт меню");
    Console.WriteLine("1 - добавить нового пациента");
    Console.WriteLine("2 - показать пациентов");
    Console.WriteLine("3 - выход");

    string userInput = Console.ReadLine();
    

    if (userInput == "1")
    {
        Patient patient = new Patient();

        patient.Passport = InputPassport();
        patient.Name = InputName();
        patient.BirthDate = InputBirthDay();
        patient.Phone = InputPhone();
        patient.Temperature = InputTemperature();

        patients.Add(patient);
        Console.WriteLine("\nПациент успешно добавлен");
    }
    else if (userInput == "2")
    {
        ShowPatients();
    }
    else if (userInput == "3")
    {
        break;
    }
    else
    {
        Console.WriteLine("Неизвестная команда\n");
    }
}


string InputPassport()
{
    Regex regex = new Regex(@"^\d{2} \d{2}-\d{6}$");
    while (true)
    {
        Console.WriteLine("\nВведите паспорт (формат - ХХ ХХ-ХХХХХХ)");

        string passport = Console.ReadLine();
        if (regex.IsMatch(passport))
        {
            return passport;
        }
        Console.WriteLine("Неправильно введено, введите повторно (формат - ХХ ХХ-ХХХХХХ)\n");
    }
}

string InputName()
{
    Regex regex = new Regex(@"^[A-Za-zА-Яа-яЁё]+$");
    while (true)
    {
        Console.WriteLine("\nВведите имя (только кириллица и латиница)");
        string name = Console.ReadLine();
        if (regex.IsMatch(name))
        {
            return name;
        }
        Console.WriteLine("Неправильно введено, введите повторно (только кириллица и латиница)");

    }
}

Date InputBirthDay()
{
    DateTime date;
    while (true)
    {
        Console.WriteLine("\nВведите дату рождения (формат - yyyy-MM-dd)");
        string birthDay = Console.ReadLine();
        if (DateTime.TryParseExact(birthDay,"yyyy-MM-dd",CultureInfo.InvariantCulture,DateTimeStyles.None, out date))
        {
            if (date <= DateTime.Today)
            {
                return new Date(date.Day, date.Month, date.Year);
            }
            else
            {
                Console.WriteLine("Дата рождения должна быть меньше сегодняшней даты\n");
            }
        }
        else
        {
            Console.WriteLine("Неправильно введено, введите повторно (формат - yyyy-MM-dd)\n");
        }
    }
}

string InputPhone()
{
    Regex regex1 = new Regex(@"^\+[0-9]\([0-9]{3}\) [0-9]{3}-[0-9]{2}-[0-9]{2}$");
    Regex regex2 = new Regex(@"^[0-9]\([0-9]{3}\) [0-9]{3}-[0-9]{4}$");
    while (true)
    {
        Console.WriteLine("\nВведите номер телефона (формат - +Х(ХХХ) ХХХ-ХХ-ХХ или X(XXX) XXX-XXXX)");

        string phone = Console.ReadLine();
        if (regex1.IsMatch(phone) || regex2.IsMatch(phone))
        {
            return phone;
        }
        Console.WriteLine("Неправильно введено, введите повторно (формат - +Х(ХХХ) ХХХ-ХХ-ХХ или X(XXX) XXX-XXXX)\n");
    }
    
}

double InputTemperature()
{
    Regex regex = new Regex(@"^\d{2},\d{2}$");
    while (true)
    {
        Console.WriteLine("\nВведите температуру (формат XX,XX)");

        string temperature = Console.ReadLine();
        if (regex.IsMatch(temperature))
        {
            return Convert.ToDouble(temperature);
        }
        Console.WriteLine("Неправильно введено, введите повторно (формат XX,XX)\n");
    }
    
}

void ShowPatients()
{
    if (patients.Count > 0)
    {
        foreach (var p in patients)
        {
            Console.WriteLine(p.Print());
        }
    }
    else
    {
        Console.WriteLine("Пациентов не найдено\n");
    }
    
}










