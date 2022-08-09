using System;
using System.Collections.Generic;
using System.IO;

namespace DatabaseOfStudents
{
    class Program
    {
        struct TStudent
        {
            public string name;
            public string surname;
            public string subjects;
            public int? yearOfBirth;
        }

        static TStudent AddStudentFromConsole()
        {
            TStudent student;
            Console.Write("\nZadejte krestni jmeno: ");
            student.name = Console.ReadLine();
            Console.Write("\nZadejte prijmeni: ");
            student.surname = Console.ReadLine();
            Console.Write("\nZadejte obor: ");
            student.subjects = Console.ReadLine();
            student.yearOfBirth = null;
            Console.Write("\nZadejte rok narozeni: ");

            while (student.yearOfBirth == null)
            {
                try
                {
                    student.yearOfBirth = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Write($"\nRok narozeni zadejte v cislech: ");
                }
            }
            return student;
        }

        static TStudent AddStudentFromFile(string line)
        {
            TStudent student;
            string[] field = line.Split(';');
            student.name = field[0];
            student.surname = field[1];
            student.subjects = field[2];
            student.yearOfBirth = int.Parse(field[3]);
            return student;
        }

        static string WriteStudent(List<TStudent> students, int index)
        {
            string record;
            try
            {
                record = $"{index,-15}{students[index].name,-20}{students[index].surname,-20}{students[index].subjects,-20}{students[index].yearOfBirth,-10}";
            }
            catch (ArgumentOutOfRangeException)
            {
                record = null;
            }
            return record;
        }

        static List<TStudent> RemoveStudent(List<TStudent> students, int index)
        {
            try
            {
                students.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Neexistuje zadny zaznam s indexem: {index}");
            }
            return students;
        }

        static void Main()
        {
            List<TStudent> students = new List<TStudent>();
            char responseFromSection;
            char responseFromMenu;
            string record;
            int? index;

            do
            {
                Console.Clear();
                Console.WriteLine("Databaze studentu - MENU");
                Console.WriteLine("------------------------\n");
                Console.WriteLine("Pridat studenta [p]");
                Console.WriteLine("Vypsat studenta [v]");
                Console.WriteLine("Vypsat studenty [w]");
                Console.WriteLine("Vymazat studenta [m]");
                Console.WriteLine("Ulozit do souboru [u]");
                Console.WriteLine("Nacist studenty [n]");
                Console.WriteLine("Ukoncit program [k]");
                Console.Write("\nZadejte akci: ");
                responseFromMenu = char.ToLower(Console.ReadKey().KeyChar);

                switch (responseFromMenu)
                {


                    //Add student into database.
                    case 'p':
                        Console.Clear();
                        Console.WriteLine("Databaze studentu - Pridani studenta");
                        Console.WriteLine("------------------------------------");
                        students.Add(AddStudentFromConsole());
                        break;


                    //Write student from database by student's index.
                    case 'v':
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Databaze studentu - Vypsani studenta");
                            Console.WriteLine("------------------------------------");

                            if (students.Count == 0)
                            {
                                Console.WriteLine("\nV databazi neni zapsani zadny student.\n");
                                Console.WriteLine("\n\nVratit do menu [stiskni tlacitko]");
                                Console.ReadKey();
                                responseFromSection = 'k';
                            }
                            else
                            {
                                try
                                {
                                    Console.Write("Zadejte index: ");
                                    index = int.Parse(Console.ReadLine());
                                    record = WriteStudent(students, index.Value);

                                    if (record != null)
                                    {
                                        Console.WriteLine($"\n{"index",-15}{"jmeno",-20}{"prijmeni",-20}{"obor",-20}{"rok narozeni",-10}");
                                        Console.WriteLine("---------------------------------------------------------------------------------------\n");
                                        Console.WriteLine(record);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\nNeexistuje zadny zaznam s indexem: {index}");
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("\nByl zadan index ve spatnem formatu. Index je potreba zadat cislem.");
                                }

                                Console.WriteLine("\n\n\nVratit do menu [k]");
                                Console.WriteLine("Vypsat znovu studenta [stisknout cokoliv jineho]");
                                responseFromSection = Console.ReadKey().KeyChar;
                            }

                        } while (responseFromSection != 'k');
                        break;


                    //Write all students from database.
                    case 'w':
                        Console.Clear();
                        Console.WriteLine("Databaze studentu - Vypsani studentu");
                        Console.WriteLine("------------------------------------\n");

                        if (students.Count == 0)
                        {
                            Console.WriteLine("V databazi neni zapsani zadny student.\n");
                        }
                        else
                        {
                            Console.WriteLine($"{"index", -15}{"jmeno",-20}{"prijmeni",-20}{"obor",-20}{"rok narozeni",-10}");
                            Console.WriteLine("---------------------------------------------------------------------------------------\n");

                            for (int i = 0; i < students.Count; i++)
                            {
                                record = WriteStudent(students, i);
                                Console.WriteLine(record);
                            }
                        }

                        Console.WriteLine("\n\nVratit do menu [stiskni tlacitko]");
                        Console.ReadKey();
                        break;


                    //Remove student from database by student's index
                    case 'm':
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Databaze studentu - Vymazani studenta");
                            Console.WriteLine("-------------------------------------");

                            if (students.Count == 0)
                            {
                                Console.WriteLine("\nV databazi neni zapsani zadny student.\n");
                                Console.WriteLine("\n\nVratit do menu [stiskni tlacitko]");
                                Console.ReadKey();
                                responseFromSection = 'k';
                            }
                            else
                            {
                                try
                                {
                                    Console.Write("Zadejte index: ");
                                    index = int.Parse(Console.ReadLine());
                                    record = WriteStudent(students, index.Value);

                                    if (record != null)
                                    {
                                        Console.WriteLine("\nDany student byl vymazan:\n");
                                        Console.WriteLine($"{"index",-15}{"jmeno",-20}{"prijmeni",-20}{"obor",-20}{"rok narozeni",-10}");
                                        Console.WriteLine("---------------------------------------------------------------------------------------\n");
                                        Console.WriteLine(record);
                                        students = RemoveStudent(students, index.Value);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\nNeexistuje zadny zaznam s indexem: {index}");
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("\nByl zadan index ve spatnem formatu. Index je potreba zadat cislem.");
                                }

                                Console.WriteLine("\n\n\nVratit do menu [k]");
                                Console.WriteLine("Vypsat znovu studenta [stisknout cokoliv jineho]");
                                responseFromSection = Console.ReadKey().KeyChar;
                            }

                        } while (responseFromSection != 'k');
                        break;


                    //Import all students from txt file into database.
                    case 'n':
                        Console.Clear();
                        Console.WriteLine("Databaze studentu - Nacteni studentu");
                        Console.WriteLine("------------------------------------");
       
                        try
                        {
                            int number_of_lines = File.ReadAllLines("studenti.txt").Length;
                            string line;

                            using StreamReader file = new StreamReader("studenti.txt");

                            if (number_of_lines != 0)
                            {
                                students.Clear();

                                while (!file.EndOfStream)
                                {
                                    line = file.ReadLine();
                                    students.Add(AddStudentFromFile(line));
                                }
                                Console.WriteLine("\nNacteni studentu probehlo uspesne.");

                                if (number_of_lines == 1)
                                {
                                    Console.WriteLine("\nByl nacten pouze 1 student.");
                                }
                                else if (new List<int> { 2, 3, 4}.Contains(number_of_lines))
                                {
                                    Console.WriteLine($"\nByli nacteni pouze {number_of_lines} studenti.");
                                }
                                else
                                {
                                    Console.WriteLine($"\nCelkem bylo nacteno {students.Count} studentu.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nNebyl nacteny zadny student, protoze soubor je prazdny.");
                            }   
                        }
                        catch (FileNotFoundException)
                        {
                            Console.WriteLine("\nSoubor se studenty nebyl nalezen. Musite nejdrive ulozit databazi studentu do souboru.");
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine($"\nNacteni studentu se nezdarilo. Podrobny duvod: \n\n{exception}");
                        }

                        Console.WriteLine("\n\n\nVratit do menu [stiskni tlacitko]");
                        Console.ReadKey();
                        break;


                    //Save all students from database into txt file.
                    case 'u':
                        Console.Clear();
                        Console.WriteLine("Databaze studentu - Ukladani studentu");
                        Console.WriteLine("-------------------------------------");

                        if (students.Count != 0)
                        {
                            try
                            {
                                using StreamWriter file = new StreamWriter("studenti.txt");

                                foreach (TStudent student in students)
                                {
                                    file.WriteLine($"{student.name}; {student.surname}; {student.subjects}; {student.yearOfBirth}");
                                }

                                Console.WriteLine("\nZapis do souboru probehl uspesne.");
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine($"\n\nZapis do souboru se nezdaril. Podrobny duvod: \n\n{exception}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\nZapis do souboru se neprovedl, jelikoz databaze studentu je prazdna.");
                        }

                        Console.WriteLine("\n\n\nVratit do menu [stiskni tlacitko]");
                        Console.ReadKey();
                        break;


                    //Turn off application - Databaze studentu.
                    case 'k':
                        break;

                    default:
                        Console.WriteLine("\nSpatna volba!");
                        Console.ReadKey();
                        break;
                }
            } while (responseFromMenu != 'k');
        }
    }
}