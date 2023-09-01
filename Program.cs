using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

//Definition av klassen 'Student'
public class Student
{
    //Egenskaper (properties) för student-de tillåter att få och sätta värden för varje attribut av studenten
    public string Förnamn { get; set; }
    public string Efternamn { get; set; }

    //Privat medlemsvariabel för personnumer (Inte direkt åtkomlig utanför klassen)
    private string _personnummer;
    
    //Egenskap (property) för personnummer med logik för att validera format
    // (till¨ter extern kod att läsa eller skriva till _personnumer, men med visssa regler)
    public string Personnummer
    {
        get => _personnummer; //Detta returnerar värdet av _personnummer när någon försöker läsa propertyn
        set
        {
            //Kontrollerar om värdet som sätts matchar ett specifik format om inte kastas en undantag.
            if (Regex.IsMatch(value, @"^\d{8}-\d{4}$"))
                _personnummer = value;
            else
                throw new ArgumentException("Personnummer är i fel format!");
        }
    }

    //Fler properties för studentklassen
    public string Telefonnummer { get; set; }
    public string Epost { get; set; }
    public string Klass { get; set; }

    // Konstruktor för studentklassen, används för att skapa en ny student med specifika värden.
    public Student(string förnamn, string efternamn, string personnummer, string telefonnummer, string klass, string epost)
    {
        Förnamn = förnamn ?? string.Empty;
        Efternamn = efternamn ?? string.Empty;
        _personnummer = "00000000-0000";
        Personnummer = personnummer ?? string.Empty;
        Telefonnummer = telefonnummer ?? "";
        Klass = klass ?? string.Empty;
        Epost = epost ?? string.Empty;
    }
}

//StudentManager klassen hanterar studentrelaterade operationer som att registrera, lista eller söka studenter.
public class StudentManager
{
    // En dictionary som lagrar baserat på deras personnummer
    private Dictionary<string, Student> students = new();

    //Huvudmenu för programmet
    public void ShowMainMenu()
    {
        // En evig loop som fortsätter visa menyn tills användaren väljer att avsluta
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Registrera studerande");
            Console.WriteLine("2. Lista studerande");
            Console.WriteLine("3. Sök studerande");
            Console.WriteLine("4. Avsluta");

            // Hanterar användarens val
            switch (Console.ReadLine())
            {
                case "1":
                    RegisterStudent();
                    break;
                case "2":
                    ListStudentsByClass();
                    break;
                case "3":
                    SearchStudentByPersonnummer();
                    break;
                case "4":
                    return; // Avslutar metoden, vilket bryter loopen.
            }
        }
    }

    // Metod för att registrera en ny student.
    public void RegisterStudent()
    {
        Console.WriteLine("Förnamn:");
        string förnamn = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Efternamn:");
        string efternamn = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Personnummer:");
        string personnummer = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Telefonnummer:");
        string telefonnummer = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("E-post:");
        string epost = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Klass:");
        string klass = Console.ReadLine() ?? string.Empty;

        // Skapa en ny studentobjekt med den insamlade informationen.
        var student = new Student(förnamn, efternamn, personnummer, telefonnummer, klass, epost);
        
        // Lägger till den nya studenten i dictionaryn
        students[personnummer] = student;

        Console.WriteLine("Studerande registrerad!");
        System.Threading.Thread.Sleep(2000);
    }

    //Metod för att lista studenter baserat på klass
    public void ListStudentsByClass()
    {
        Console.WriteLine("Klass:");
        string klass = Console.ReadLine() ?? string.Empty;

        var found = false;
        foreach (var student in students.Values)
        {
            if (student.Klass.Equals(klass, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"{student.Förnamn} {student.Efternamn} {student.Personnummer} {student.Telefonnummer}");
                found = true;
            }
        }

        if (!found) //om ingen student hittades i den angivna klassen->skriver meddelandet.
        {
            Console.WriteLine("Inga studenter funna i den angivna klassen.");
        }
        Console.ReadLine();
    }

    // Metod för att söka studentbaserat personnummer.
    public void SearchStudentByPersonnummer()
{
    Console.WriteLine("Personnummer:");
    string personnummer = Console.ReadLine() ?? string.Empty;

        // försöker hitta en student i dictionaryn baserat på personnummer.
    if (students.TryGetValue(personnummer, out Student? student) && student != null)
    {
        Console.WriteLine($"Förnamn: {student.Förnamn}");
        Console.WriteLine($"Efternamn: {student.Efternamn}");
        Console.WriteLine($"Personnummer: {student.Personnummer}");
        Console.WriteLine($"Telefonnummer: {student.Telefonnummer}");
        Console.WriteLine($"E-post: {student.Epost}");
        Console.WriteLine($"Klass: {student.Klass}");
        Console.ReadLine();
    }
    else
    {
        Console.WriteLine("Studerande saknas");
        System.Threading.Thread.Sleep(2000);
    }
}
    
}

//Programklassen innehåller entry point (Main-metoden) för aplokationen
class Program
{
    public static void Main(string[] args)
    {
        //Skapar en instans av studentManager och kallar dess ShowMainMenu
        var manager = new StudentManager();
        manager.ShowMainMenu();
    }
}
