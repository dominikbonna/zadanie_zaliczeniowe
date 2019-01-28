//Wykonano na potrzeby zadania zaliczeniowego z podstaw programowania AP
//Autor: Dominik Bonna
//Problemy z zapisem XML, reszta sprawna
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;

namespace zadanie_zaliczeniowe
{
    #region Struktura danych studenta
    public struct StudentsData
    {
        public string firstName;
        public string name;
        public int index;
        public int semester;
        public Dictionary<string, double> subjects { get; set; }
        public StudentsData(string firstNameF, string nameF, int indexF, int semesterF, string subjectF, double ratingF, int counter)
        {
            firstName = firstNameF;
            name = nameF;
            index = indexF;
            semester = semesterF;

            subjects = new Dictionary<string, double>();
            if (counter == 1)
            {
                subjects.Add(subjectF, ratingF);
            }
            else if (counter > 1)
            {
                for (int i = 1; i < counter; i++)
                {
                    Console.Write("Podaj nazwę przedmiotu: ");
                    subjectF = Console.ReadLine();
                    Console.Write("Podaj ocenę z przedmiotu: ");
                    ratingF = double.Parse(Console.ReadLine());
                    subjects.Add(subjectF, ratingF);
                }
            }
            else
                Console.WriteLine("Podałeś złą wartość! Spróbuj ponownie...");
        }
    }
    #endregion
    class Program
    {
        public static int indexF;
        
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Console.ForegroundColor = ConsoleColor.Yellow;
            List<StudentsData> listOfStudents = new List<StudentsData>();
            #region MENU
            for (int i = 0; i <= 0;)
            {
                Console.WriteLine("_____________________________________________________________________________________");
                Console.WriteLine(@"                          WITAJ W BAZIE DANYCH STUDENTÓW!");
                Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                Console.WriteLine("1. Wyświetl listę studentów");
                Console.WriteLine("_____________________________________________________________________________________");
                Console.WriteLine("2. Dodaj/usuń studenta");
                Console.WriteLine("_____________________________________________________________________________________");
                Console.WriteLine("3. Modyfikuj dane studenta");
                Console.WriteLine("_____________________________________________________________________________________");
                Console.WriteLine("4. Wyświetl średnie, ich mediany i odchylenie standardowe ocen wszystkich studentów");
                Console.WriteLine("_____________________________________________________________________________________");
                Console.WriteLine("5. Zapisz/wczytaj dane z pliku");
                Console.WriteLine("_____________________________________________________________________________________");
                Console.WriteLine("ESC. Wyjdź z programu");
                Console.WriteLine("_____________________________________________________________________________________");
                #endregion
                ConsoleKeyInfo menuKey = Console.ReadKey();
                switch (menuKey.Key)
                {
                    #region Wyświetlanie studentów
                        case ConsoleKey.D1:
                        Console.Clear();
                        i++;
                        Console.WriteLine("_____________________________________________________________________________________");
                        Console.WriteLine(@"                          WITAJ W BAZIE DANYCH STUDENTÓW!");
                        Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                        Console.WriteLine("1. Wyświetl wszystkich studentów.");
                        Console.WriteLine("_____________________________________________________________________________________");
                        Console.WriteLine("2. Wyszukaj studentów o wybranym imieniu i nazwisku...");
                        Console.WriteLine("_____________________________________________________________________________________");
                        Console.WriteLine("3. Wyszukaj studentów po numerze indeksu...");
                        Console.WriteLine("_____________________________________________________________________________________");
                        Console.WriteLine("4. Wyszukaj studentów po numerze semestru...");
                        Console.WriteLine("_____________________________________________________________________________________");
                        Console.WriteLine("ESC. Powrót do MENU");
                        Console.WriteLine("_____________________________________________________________________________________");
                        ConsoleKeyInfo menuP = Console.ReadKey();
                        for (int j = 0; j <= 0;)
                        {
                            switch (menuP.Key)
                            {
                                case ConsoleKey.D1:
                                    j++;
                                    Console.Clear();
                                    foreach (StudentsData student in listOfStudents)
                                    {
                                        Console.Write($"Indeks: {student.index}, imię: {student.firstName}, nazwisko: {student.name}, semestr: {student.semester}, średnia ocen: {student.subjects.Values.Average()}, oceny: ");
                                        foreach(KeyValuePair<string, double> student2 in student.subjects)
                                        {
                                            Console.Write($"{student2.Value}, ");
                                        }
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("_____________________________________________________________________________________");
                                    Console.WriteLine("ESC. Powrót do MENU");
                                    Console.WriteLine("_____________________________________________________________________________________");
                                    menuP = Console.ReadKey();
                                    if (menuP.Key == ConsoleKey.Escape)
                                    {
                                        Console.Clear();
                                        j--;
                                    }
                                    else
                                        Console.Write("Wciśnąłeś zły przycisk, spróbuj ponownie...");
                                    break;
                               case ConsoleKey.D2:
                                   j++;
                                   Console.Clear();
                                   Console.Write("Podaj imię wyszukiwanych studentów: ");
                                   string firstNameK = Console.ReadLine().Trim();
                                   Console.Write("Podaj nazwisko wyszukiwanych studentów: ");
                                   string nameK = Console.ReadLine().Trim();
                                    var arr = listOfStudents.Where(x => x.firstName == firstNameK && x.name == nameK).OrderBy(x => x.index);
                                    foreach (StudentsData student in arr)
                                    {
                                        Console.Write($"Indeks: {student.index}, imię: {student.firstName}, nazwisko: {student.name}, semestr: {student.semester}, średnia ocen: {student.subjects.Values.Average()}, oceny: ");
                                        foreach (KeyValuePair<string, double> student2 in student.subjects)
                                        {
                                            Console.Write($"{student2.Value}, ");
                                        }
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("_____________________________________________________________________________________");
                                    Console.WriteLine("ESC. Powrót do MENU");
                                    Console.WriteLine("_____________________________________________________________________________________");
                                    menuP = Console.ReadKey();
                                    if (menuP.Key == ConsoleKey.Escape)
                                    {
                                        Console.Clear();
                                        j--;
                                    }
                                    else
                                        Console.Write("Wciśnąłeś zły przycisk, spróbuj ponownie...");
                                    break;
                               case ConsoleKey.D3:
                                    j++;
                                    Console.Clear();
                                    Console.Write("Podaj indeks wyszukiwanego studenta: ");
                                    int indexK = int.Parse(Console.ReadLine().Trim());
                                    var arr2 = listOfStudents.Where(x => x.index == indexK);
                                    foreach (StudentsData student in arr2)
                                    {
                                        Console.Write($"Indeks: {student.index}, imię: {student.firstName}, nazwisko: {student.name}, semestr: {student.semester}, średnia ocen: {student.subjects.Values.Average()}, oceny: ");
                                        foreach (KeyValuePair<string, double> student2 in student.subjects)
                                        {
                                            Console.Write($"{student2.Value}, ");
                                        }
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("_____________________________________________________________________________________");
                                    Console.WriteLine("ESC. Powrót do MENU");
                                    Console.WriteLine("_____________________________________________________________________________________");
                                    menuP = Console.ReadKey();
                                    if (menuP.Key == ConsoleKey.Escape)
                                    {
                                        Console.Clear();
                                        j--;
                                    }
                                    else
                                        Console.Write("Wciśnąłeś zły przycisk, spróbuj ponownie...");
                                    break;
                               case ConsoleKey.D4:
                                    j++;
                                    Console.Clear();
                                    Console.Write("Podaj semestr wyszukiwanych studentów: ");
                                    int semesterK = int.Parse(Console.ReadLine().Trim());
                                    var arr3 = listOfStudents.Where(x => x.semester == semesterK);
                                    foreach (StudentsData student in arr3)
                                    {
                                        Console.Write($"Indeks: {student.index}, imię: {student.firstName}, nazwisko: {student.name}, semestr: {student.semester}, średnia ocen: {student.subjects.Values.Average()}, oceny: ");
                                        foreach (KeyValuePair<string, double> student2 in student.subjects)
                                        {
                                            Console.Write($"{student2.Value}, ");
                                        }
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("_____________________________________________________________________________________");
                                    Console.WriteLine("ESC. Powrót do MENU");
                                    Console.WriteLine("_____________________________________________________________________________________");
                                    menuP = Console.ReadKey();
                                    if (menuP.Key == ConsoleKey.Escape)
                                    {
                                        Console.Clear();
                                        j--;
                                    }
                                    else
                                        Console.Write("Wciśnąłeś zły przycisk, spróbuj ponownie...");
                                    break;
                               case ConsoleKey.Escape:
                                   Console.Clear();
                                   j++;
                                   break;
                           }

                       }
                       i--;
                       #endregion
                       break;
                   #region Dodawanie/usuwanie studenta 
                       case ConsoleKey.D2:
                       Console.Clear();
                       i++;
                       for (int j = 0; j <= 0;)
                       {
                           Console.WriteLine("_____________________________________________________________________________________");
                           Console.WriteLine(@"                          WITAJ W BAZIE DANYCH STUDENTÓW!");
                           Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                           Console.WriteLine("1. Dodaj studenta");
                           Console.WriteLine("_____________________________________________________________________________________");
                           Console.WriteLine("2. Usuń studenta");
                           Console.WriteLine("_____________________________________________________________________________________");
                           Console.WriteLine("ESC. Powrót do menu");
                           Console.WriteLine("_____________________________________________________________________________________");
                           ConsoleKeyInfo menuH = Console.ReadKey();
                           switch (menuH.Key)
                           {
                               case ConsoleKey.D1:
                                   Console.Clear();
                                   j++;
                                   Console.Write("Wprowadź imię nowego studenta: ");
                                   string firstNameF = Console.ReadLine();
                                   Console.Write("Wprowadź nazwisko nowego studenta: ");
                                   string nameF = Console.ReadLine();
                                   Console.Write("Wprowadź liczbowo semestr nowego studenta: ");
                                   int semesterF = int.Parse(Console.ReadLine());
                                   if (semesterF > 0)
                                   {
                                       Console.Write("Wprowadź liczbowo ile przedmiotów chcesz wprowadzić? ");
                                       int counter = int.Parse(Console.ReadLine());
                                       if (counter > 0)
                                       {
                                           Console.Write("Podaj nazwę przedmiotu: ");
                                           string subjectF = Console.ReadLine();
                                           Console.Write("Podaj ocenę z przedmiotu: ");
                                           double ratingF = double.Parse(Console.ReadLine());
                                           listOfStudents.Add(new StudentsData(firstNameF, nameF, indexF = listOfStudents.Count + 1, semesterF, subjectF, ratingF, counter));
                                           Console.WriteLine($"Student {firstNameF} {nameF} został utworzony!");
                                       }
                                       else

                                           Console.WriteLine("Podana wartość jest nieprawidłowa. Spróbuj ponownie...");
                                   }
                                   else
                                       Console.WriteLine("Podana wartość jest nieprawidłowa. Spróbuj ponownie...");
                                   Thread.Sleep(2000);
                                   Console.Clear();
                                   j--;
                                   break;
                               case ConsoleKey.D2:
                                   j++;
                                   Console.Write("Podaj indeks studenta, którego chcesz usunąć: ");
                                   indexF = int.Parse(Console.ReadLine());
                                   if (indexF - 1 >= 0 && indexF - 1 <= listOfStudents.Count)
                                   {
                                       listOfStudents.RemoveAt(indexF - 1);
                                       Console.WriteLine($"Usunąłeś studenta o indeksie {indexF}.");
                                   }
                                   else
                                   {
                                       Console.WriteLine("Wprowadziłeś zły indeks, spróbuj ponownie");
                                   }
                                   Thread.Sleep(2000);
                                   Console.Clear();
                                   j--;
                                   break;
                               case ConsoleKey.Escape:
                                   j++;
                                   break;
                               default:
                                   j++;
                                   Console.WriteLine("Wcisnąłeś zły przycisk, spróbuj jeszcze raz.");
                                   Thread.Sleep(2000);
                                   Console.Clear();
                                   j--;
                                   break;
                           }
                       }
                       Console.Clear();
                       i--;
                       #endregion 
                       break;
                   case ConsoleKey.D3:
                       #region Modyfikacja danych
                       Console.Clear();
                       i++;
                       for (int j = 0; j <= 0;)
                       {
                           Console.WriteLine("_____________________________________________________________________________________");
                           Console.WriteLine(@"                          WITAJ W BAZIE DANYCH STUDENTÓW!");
                           Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                           Console.WriteLine("1. Modyfikuj imię...");
                           Console.WriteLine("_____________________________________________________________________________________");
                           Console.WriteLine("2. Modyfikuj nazwisko...");
                           Console.WriteLine("_____________________________________________________________________________________");
                           Console.WriteLine("3. Modyfikuj semestr...");
                           Console.WriteLine("_____________________________________________________________________________________");
                           Console.WriteLine("4. Dodaj/usuń ocenę...");
                           Console.WriteLine("_____________________________________________________________________________________");
                           Console.WriteLine("ESC. Powrót do menu");
                           Console.WriteLine("_____________________________________________________________________________________");
                           ConsoleKeyInfo menuK = Console.ReadKey();
                           switch (menuK.Key)
                           {

                               case ConsoleKey.D1:
                                   j++;
                                   Console.Clear();
                                   Console.Write("Wprowadź indeks studenta do modyfikacji: ");
                                   int indexF = int.Parse(Console.ReadLine());
                                   StudentsData students = listOfStudents[indexF-1];
                                   Console.Write("Wprowadź nowe imię studenta: ");
                                   students.firstName = Console.ReadLine();
                                   Console.WriteLine($"Zmieniłeś imię studenta o indeksie {indexF}");
                                   listOfStudents[indexF-1] = students;
                                   Thread.Sleep(2000);
                                   Console.Clear();
                                   j--;
                                   break;
                               case ConsoleKey.D2:
                                    j++;
                                    Console.Clear();
                                    Console.Write("Wprowadź indeks studenta do modyfikacji: ");
                                    int indexJ = int.Parse(Console.ReadLine());
                                    StudentsData studentsJ = listOfStudents[indexJ - 1];
                                    Console.Write("Wprowadź nowe nazwisko studenta: ");
                                    studentsJ.name = Console.ReadLine();
                                    Console.WriteLine($"Zmieniłeś nazwisko studenta o indeksie {indexJ}");
                                    listOfStudents[indexJ - 1] = studentsJ;
                                    Thread.Sleep(2000);
                                   Console.Clear();
                                   j--;
                                   break;
                               case ConsoleKey.D3:
                                    j++;
                                    Console.Clear();
                                    Console.Write("Wprowadź indeks studenta do modyfikacji: ");
                                    int indexH = int.Parse(Console.ReadLine());
                                    StudentsData studentsH = listOfStudents[indexH - 1];
                                    Console.Write("Wprowadź nowy semestr studenta: ");
                                    studentsH.semester = int.Parse(Console.ReadLine());
                                    Console.WriteLine($"Zmieniłeś nazwisko studenta o indeksie {indexH}");
                                    listOfStudents[indexH - 1] = studentsH;
                                    Thread.Sleep(2000);
                                   Console.Clear();
                                   j--;
                                   break;
                               case ConsoleKey.D4:
                                    j++;
                                    Console.Clear();
                                    for (int k = 0; k <= 0;)
                                    {
                                        Console.WriteLine("_____________________________________________________________________________________");
                                        Console.WriteLine(@"                          WITAJ W BAZIE DANYCH STUDENTÓW!");
                                        Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                                        Console.WriteLine("1. Dodaj ocenę...");
                                        Console.WriteLine("_____________________________________________________________________________________");
                                        Console.WriteLine("2. Usuń ocenę...");
                                        Console.WriteLine("_____________________________________________________________________________________");
                                        Console.WriteLine("ESC. Powrót");
                                        Console.WriteLine("_____________________________________________________________________________________");
                                        ConsoleKeyInfo menu = Console.ReadKey();
                                        if(menu.Key==ConsoleKey.D1)
                                        {
                                            k++;
                                            Console.Clear();
                                            Console.Write("Wprowadź indeks studenta do modyfikacji: ");
                                            int indexU = int.Parse(Console.ReadLine());
                                            StudentsData studentsU = listOfStudents[indexU - 1];
                                            Console.Write("Wprowadź nazwę przedmiotu: ");
                                            string subject = Console.ReadLine();
                                            Console.Write("Wprowadź ocenę z przedmiotu: ");
                                            double rating = double.Parse(Console.ReadLine().Trim());
                                            studentsU.subjects.Add(subject, rating);
                                            Console.WriteLine($"Dodano przedmiot {subject} z oceną {rating}");
                                            Thread.Sleep(2500);
                                            Console.Clear();
                                            k--;
                                        }
                                        else if(menu.Key==ConsoleKey.D2)
                                        {
                                            k++;
                                            Console.Clear();
                                            Console.Write("Wprowadź indeks studenta do modyfikacji: ");
                                            int indexU = int.Parse(Console.ReadLine());
                                            StudentsData studentsU = listOfStudents[indexU - 1];
                                            string subject = Console.ReadLine();
                                            Console.Write("Wprowadź nazwę przedmiotu z jakiego chcesz usunąć ocenę: ");
                                            studentsU.subjects.Remove(subject);
                                            Console.WriteLine($"Usunąłeś ocenę z przedmiotu {subject}");
                                            Console.Clear();
                                            k--;
                                        }
                                        else if(menu.Key==ConsoleKey.Escape)
                                        {
                                            j++;
                                            Console.Clear();
                                        }
                                    }

                                    break;
                               case ConsoleKey.Escape:
                                   j++;
                                   break;
                               default:
                                   Console.Clear();
                                   j++;
                                   Console.WriteLine("Wcisnąłeś zły przycisk, spróbuj jeszcze raz.");
                                   Thread.Sleep(2000);
                                   Console.Clear();
                                   j--;
                                   break;
                           }
                       }
                       #endregion
                       Console.Clear();
                       i--;
                       break;
                   case ConsoleKey.D4:
                       i++;
                        #region Wyświetlanie średniej, mediany średnich...
                        Console.Clear();
                        double studentsAverage = 0;
                        int counter2 = 0;
                        foreach(StudentsData student in listOfStudents)
                        {
                            counter2++;
                            studentsAverage += student.subjects.Values.Average();
                        }
                        studentsAverage = studentsAverage / counter2;
                        Console.WriteLine($"Średnia ocen wszystkich studentów wynosi: {studentsAverage}");
                        if (counter2 % 2 == 0)
                        {
                            StudentsData student = listOfStudents[(counter2 / 2)-1];
                            StudentsData student2 = listOfStudents[counter2 / 2];
                            Console.WriteLine($"Mediana średnich wynosi: {(student.subjects.Values.Average() + student2.subjects.Values.Average()) / 2}");
                        }
                        else
                        {
                            StudentsData student = listOfStudents[(counter2 / 2)];
                            Console.WriteLine($"Mediana średnich wynosi: {student.subjects.Values.Average()}");
                        }
                        double variance = 0;
                        counter2 = 0;
                        foreach (StudentsData student in listOfStudents)
                        {
                            counter2++;
                            variance += (student.subjects.Values.Average() - studentsAverage) * (student.subjects.Values.Average() - studentsAverage);
                        }
                        variance = variance / counter2;
                        double standardDev = Math.Sqrt(variance*variance);
                        Console.WriteLine($"Odchylenie standardowe wynosi: {standardDev}");
                        Console.WriteLine("_____________________________________________________________________________________");
                        Console.WriteLine("ESC. Powrót do MENU");
                        Console.WriteLine("_____________________________________________________________________________________");
                        var menuD = Console.ReadKey();
                        if (menuD.Key == ConsoleKey.Escape)
                        {
                            i--;
                            Console.Clear();
                        }
                        #endregion 
                        break;
                   case ConsoleKey.D5:
                       i++;
                       Console.Clear();
                        #region Zapis/odczyt XML
                       for(int j=0;j<=0;)
                        {
                            Console.WriteLine("_____________________________________________________________________________________");
                            Console.WriteLine(@"                          WITAJ W BAZIE DANYCH STUDENTÓW!");
                            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                            Console.WriteLine("1. Zapisz bazę do pliku XML...");
                            Console.WriteLine("_____________________________________________________________________________________");
                            Console.WriteLine("2. Wczytaj bazę z pliku XML...");
                            Console.WriteLine("_____________________________________________________________________________________");
                            Console.WriteLine("ESC. Powrót do MENU");
                            Console.WriteLine("_____________________________________________________________________________________");
                            ConsoleKeyInfo menu = Console.ReadKey();
                            switch (menu.Key)
                            {
                                case ConsoleKey.D1:
                                    Console.Clear();
                                    j++;
                                    /*StreamWriter sw = new StreamWriter("students.xml");
                                    XmlSerializer serializer = new XmlSerializer(typeof(List<StudentsData>));
                                    serializer.Serialize(sw, listOfStudents);
                                    sw.Flush();
                                    sw.Close();*/
                                    Console.WriteLine("Baza danych została zapisana!");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                    j--;
                                    break;
                                case ConsoleKey.D2:
                                    Console.Clear();
                                    j++;
                                    XElement xelement = XElement.Load("students.xml");
                                    IEnumerable<XElement> students = xelement.Elements();
                                    foreach (var element in students)
                                    {
                                        StudentsData student = new StudentsData();
                                        student.index = int.Parse(element.Element("index").Value);
                                        student.firstName = element.Element("firstName").Value;
                                        student.name = element.Element("name").Value;
                                        student.semester = int.Parse(element.Element("semester").Value);
                                        student.subjects = element.Elements().ToDictionary(x => x.Name.LocalName, x => double.Parse(x.Value));listOfStudents.Add(student);
                                    }
                                    Console.WriteLine("Baza danych została wczytana!");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                    j--;
                                    break;
                                case ConsoleKey.Escape:
                                    Console.Clear();
                                    j++;
                                    break;
                                default:
                                    Console.WriteLine("Wcisnąłeś zły przycisk, spróbuj ponownie...");
                                    Thread.Sleep(2000);
                                    Console.Clear();
                                    break;
                            }
                            } 
                       Console.Clear();
                       i--;
                            #endregion
                            break;
                   case ConsoleKey.D7:
                       i++;
                       Thread.Sleep(2000);
                       Console.Clear();
                       i--;
                       break;
                   case ConsoleKey.Escape:
                       i++;
                       break;
                   default:
                       Console.Clear();
                       i++;
                       Console.WriteLine("Wcisnąłeś zły przycisk, spróbuj jeszcze raz.");
                       Thread.Sleep(2000);
                       Console.Clear();
                       i--;
                       break;
               }
           }
       }
   }
}
 