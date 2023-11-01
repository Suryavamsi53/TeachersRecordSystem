using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherDataSystem
{
  
class Teacher
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Class { get; set; }
            public string Section { get; set; }
        }

        class TeacherRecordSystem
        {
            private List<Teacher> teachers;

            public TeacherRecordSystem()
            {
                teachers = new List<Teacher>();
            }

            public void AddTeacher(Teacher teacher)
            {
                // Assigning a unique ID 
                teacher.ID = teachers.Count + 1;
                teachers.Add(teacher);
            }

            public void UpdateTeacher(int id, Teacher updatedTeacher)

            
            {
                Teacher teacherToUpdate = teachers.Find(t => t.ID == id);
                if (teacherToUpdate != null)

                {
                
                    teacherToUpdate.Name = updatedTeacher.Name;
                    teacherToUpdate.Class = updatedTeacher.Class;
                    teacherToUpdate.Section = updatedTeacher.Section;
                }
            }

            public void SaveToFile(string fileName)
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (Teacher teacher in teachers)
                    {
                        writer.WriteLine($"{teacher.ID},{teacher.Name},{teacher.Class},{teacher.Section}");
                    }
                }
            }

            public void LoadFromFile(string fileName)
            {
                if (File.Exists(fileName))
                {
                    teachers.Clear();
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 4 && int.TryParse(parts[0], out int id))
                            {
                                teachers.Add(new Teacher
                                {
                                    ID = id,
                                    Name = parts[1],
                                    Class = parts[2],
                                    Section = parts[3]
                                });
                            }
                        }
                    }
                }
            }

            public void DisplayAllTeachers()
            {
                foreach (Teacher teacher in teachers)
                {
                    Console.WriteLine($"ID: {teacher.ID}, Name: {teacher.Name}, Class: {teacher.Class}, Section: {teacher.Section}");
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                TeacherRecordSystem recordSystem = new TeacherRecordSystem();

                // Load teacher records from a file if available
                recordSystem.LoadFromFile("teacher_records.txt");

                bool exit = false;

                while (!exit)
                {
                    Console.WriteLine("Teacher Record System");
                    Console.WriteLine("1. Add Teacher");
                    Console.WriteLine("2. Update Teacher");
                    Console.WriteLine("3. Display All Teachers");
                    Console.WriteLine("4. Save and Exit");

                    Console.Write("Enter your choice: ");
                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Enter teacher's name: ");
                                string name = Console.ReadLine();
                                Console.Write("Enter class: ");
                                string teacherClass = Console.ReadLine();
                                Console.Write("Enter section: ");
                                string section = Console.ReadLine();

                                recordSystem.AddTeacher(new Teacher { Name = name, Class = teacherClass, Section = section });
                                break;

                            case 2:
                                Console.Write("Enter teacher ID to update: ");
                                if (int.TryParse(Console.ReadLine(), out int id))
                                {
                                    Console.Write("Enter updated name: ");
                                    string updatedName = Console.ReadLine();
                                    Console.Write("Enter updated class: ");
                                    string updatedClass = Console.ReadLine();
                                    Console.Write("Enter updated section: ");
                                    string updatedSection = Console.ReadLine();

                                    recordSystem.UpdateTeacher(id, new Teacher { Name = updatedName, Class = updatedClass, Section = updatedSection });
                                }
                                else
                                {
                                    Console.WriteLine("Invalid ID.");
                                }
                                break;

                            case 3:
                                recordSystem.DisplayAllTeachers();
                                break;

                            case 4:
                                // Save teacher records to a file and exit
                                recordSystem.SaveToFile("A:\\FileHandling\\A");
                                exit = true;
                                break;

                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }
            }
        }

    }
