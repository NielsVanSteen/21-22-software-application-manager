using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using SoftwareApplicationManager.UI.CA.Extensions;
using SoftwareApplicationManager.BL;
using SoftwareApplicationManager.BL.Domain;
using SoftwareApplicationManager.DAL;
using SoftwareApplicationManager.DAL.EF;

namespace SoftwareApplicationManager.UI.CA
{
    class Program
    {

        private IManager _iManager;
        
        public static void Main(string[] args)
        {
            var iManager = new ApplicationManager(new SoftwareApplicationRepository(new SoftwareApplicationManagerContext()));
            Program program = new Program(iManager);
            program.Run();
        } // Main.

        public Program(IManager iManager)
        {
            _iManager = iManager;
        } // Program.

        // Run.
        public void Run()
        {
            byte option = 0;
            
            do
            {
                option = GetOption();
                Console.Write("\n");
                bool correctInput;

                do
                {
                    correctInput = true;
                    try
                    {
                        switch (option)
                        {
                            case 0: Console.WriteLine("Application exited!"); break;
                            case 1: PrintApplications(); break;
                            case 2: PrintApplicationsByType(); break;
                            case 3: PrintOperatingSystems(); break;
                            case 4: PrintOperatingSystemsWithFilters(); break;
                            case 5: AddSoftwareApplication(); break;
                            case 6: AddOperatingSystem(); break;
                            case 7: AddSoftwareApplicationToOperatingSystem(); break;
                            case 8: RemoveSoftwareApplicationFromOperatingSystem();break;
                            default: Console.WriteLine("Invalid input!"); break;
                        }
                    }
                    catch (ValidationException ex)
                    {
                        Console.WriteLine(ex.Message + "\n Try again..");
                        correctInput = false;
                    }
                    Console.Write("\n");
                } while(!correctInput);
                
            } while (option != 0);
        } // Run.
        
        private void PrintApplications()
        {
            foreach (var application in _iManager.GetAllSoftwareApplications(true, true))
            {
                Console.WriteLine(application.GetInfoWithOperatingSystems());
            } // Foreach.
        } // PrintApplications.
        
        private void PrintApplicationsByType()
        {
            foreach (var application in _iManager.GetSoftwareApplicationsOfType(askUserToEnterSoftwareApplicationType()))
            {
                Console.WriteLine(application.GetInfo());
            }
        } // PrintApplicationsByType.
        
        private void PrintOperatingSystems()
        {
            foreach (var operatingSystem in _iManager.GetAllOperatingSystems(true))
            {
                Console.WriteLine(operatingSystem.GetInfoWithSoftwareApplications());
            } // Foreach.
        } // PrintOperatingSystems.
        
        private void PrintOperatingSystemsWithFilters()
        {
            Console.Write("Enter (part of) name or leave blank: ");
            string name = Console.ReadLine();
            Console.Write("Enter full date (yyyy/mm/dd) or leave blank: ");
            string sDate = Console.ReadLine();
            DateTime? date = String.IsNullOrEmpty(sDate) ? null : DateTime.Parse(sDate);
            foreach (var operatingSystem in _iManager.GetOperatingSystemsByNameAndReleaseDate(name, date))
            {
                Console.WriteLine(operatingSystem.GetInfo());
            } // Foreach.
        } // PrintOperatingSystems.

        private void AddSoftwareApplication()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            
            Console.Write("Enter score (0-10): ");
            string readLine = Console.ReadLine();
            byte? score = String.IsNullOrEmpty(readLine) ? null : Byte.Parse(readLine);
            
            Console.Write("Enter release date (yyyy/mm/dd): ");
            readLine = Console.ReadLine();
            DateTime date = String.IsNullOrWhiteSpace(readLine) ? throw new ValidationException("Date can't be empty!") : DateTime.Parse(readLine);
            
            Console.Write("Enter amount of downloads: ");
            int downloads = Int32.Parse(Console.ReadLine() ?? String.Empty);

            _iManager.AddSoftwareApplication(name, score, "", "", date, askUserToEnterSoftwareApplicationType(), downloads);
        } // AddSoftwareApplication.

        private void AddOperatingSystem()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter description: ");
            string description = Console.ReadLine();
            Console.Write("Enter release date (yyyy/mm/dd): ");
            string readLine = Console.ReadLine();
            DateTime date = String.IsNullOrWhiteSpace(readLine) ? throw new ValidationException("Date can't be empty!") : DateTime.Parse(readLine);

            _iManager.AddOperatingSystem(name, date, description, null);
        } // AddOperatingSystem.
        
        private void AddSoftwareApplicationToOperatingSystem()
        {
            Console.WriteLine("Which Operating System Would you like to add a Software Application to? ");
            PrintAllOperatingSystemNames();
            
            Console.Write("Please enter a Operating System Id: ");
            long operatingSystemId = long.Parse(Console.ReadLine());

            Console.WriteLine("\nWhich Software Application would you like to add to this Operating System? ");

            foreach (var softwareApplication in _iManager.GetAllSoftwareApplications())
            {
                Console.WriteLine($"[{softwareApplication.SoftwareApplicationId}]: {softwareApplication.Name}");
            } // Foreach.
            
            Console.Write("Please enter a Software Application Id: ");
            long softwareApplicationId = long.Parse(Console.ReadLine());
            Console.WriteLine();
            
            _iManager.AddSoftwareApplicationToOperatingSystem(softwareApplicationId, operatingSystemId);
            
        } // AddSoftwareApplicationToOperatingSystem.
        
        private void RemoveSoftwareApplicationFromOperatingSystem()
        {
            Console.WriteLine("Which Operating System would you like to remove a Software Application from? ");
            PrintAllOperatingSystemNames();
            
            Console.Write("Please enter a Operating System Id: ");
            long operatingSystemId = long.Parse(Console.ReadLine());

            Console.WriteLine("\nWhich Software Application would you like to remove from this Operating System? ");
            PrintAllSoftwareApplicationNamesOfOperatingSystem(operatingSystemId);
            
            Console.Write("Please enter a Software Application Id: ");
            long softwareApplicationId = long.Parse(Console.ReadLine());
            Console.WriteLine();
            
            _iManager.RemoveSoftwareApplicationToOperatingSystem(softwareApplicationId, operatingSystemId);

        } // RemoveSoftwareApplicationFromOperatingSystem.
        
        private AppType.Type askUserToEnterSoftwareApplicationType()
        {
            Console.Write("Type: ( ");
            foreach (AppType.Type type in Enum.GetValues(typeof(AppType.Type)))
            {
                Console.Write((byte)type + "=" + type + " ");
            } // Foreach.
            Console.Write(" ): ");

            return (AppType.Type)Byte.Parse(Console.ReadLine() ?? string.Empty);
        } // printEnumValues.

        private void PrintAllOperatingSystemNames()
        {
            foreach (var operatingSystem in _iManager.GetAllOperatingSystems())
            {
                Console.WriteLine($"[{operatingSystem.OperatingSystemId}]: {operatingSystem.Name}");
            } // Foreach.
        } // printAllOperatingSystemNames.
        
        private void PrintAllSoftwareApplicationNamesOfOperatingSystem(long operatingSystemId)
        {
            foreach (var softwareApplication in _iManager.GetSoftwareApplicationsOfOperatingSystem(operatingSystemId))
            {
                Console.WriteLine($"[{softwareApplication.SoftwareApplicationId}]: {softwareApplication.Name}");
            } // Foreach.
        } // printAllOperatingSystemNames.
        
        

        // Returns the selected option from the player.
        private byte GetOption()
        {
            byte option;
            
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("==========================");
            Console.WriteLine("0. Quit");
            Console.WriteLine("1. Show all software applications");
            Console.WriteLine("2. Show all software applications by software application type.");
            Console.WriteLine("3. Show all operating systems.");
            Console.WriteLine("4. Show all operating systems with name and/or release date.");
            Console.WriteLine("5. Add a Software Application.");
            Console.WriteLine("6. Add an Operating System.");
            Console.WriteLine("7. Add a Software Application to an Operating System");
            Console.WriteLine("8. Remove a software Application from an Operating System.");
            Console.Write("Your choice: ");

            if (!Byte.TryParse(Console.ReadLine(), out option))
                return 0;

            return option;
        } // GetOption.
    }
}