using System;
using System.Collections.Generic;
using System.Linq;
using SoftwareApplicationManager.BL.Domain;
using OperatingSystem = SoftwareApplicationManager.BL.Domain.OperatingSystem;

namespace SoftwareApplicationManager.DAL
{
    public class InMemoryRepository : IRepository
    {
        // Fields.
        private ICollection<Developer> Developers { get; set; } = new List<Developer>();
        private ICollection<SoftwareApplication> SoftwareApplications { get; set; } = new List<SoftwareApplication>();
        private ICollection<BL.Domain.OperatingSystem> OperatingSystems { get; set; } = new List<BL.Domain.OperatingSystem>();
        private ICollection<Rating> Ratings { get; set; }

        // Constructor.
        public InMemoryRepository()
        {
            Seed();
        } // InMemoryRepository.
        
        // Methods.
        public OperatingSystem ReadOperatingSystem(long id, bool loadSoftwareApplications = false, bool loadDeveloperOfSoftwareApplications = false)
        {
            return OperatingSystems.FirstOrDefault(o => o.OperatingSystemId == id);
        } // ReadOperatingSystem.

        public IEnumerable<BL.Domain.OperatingSystem> ReadAllOperatingSystems(bool loadSoftwareApplications = false)
        {
            return OperatingSystems;
        } // ReadAllOperatingSystems.

        public IEnumerable<OperatingSystem> ReadOperatingSystemsByNameAndReleaseDate(string name, DateTime? releaseDate, bool loadSoftwareApplications = false)
        {
            return OperatingSystems.Where(o => (name == null || o.Name.ToLower().Contains(name.ToLower())) 
                                                             && ( releaseDate == null || DateTime.Equals(o.ReleaseDate, DateTime.Parse(releaseDate.ToString())) ) );
        } // ReadOperatingSystemByNameAndReleaseDate.

        public void CreateOperatingSystem(OperatingSystem operatingSystem)
        {
            operatingSystem.OperatingSystemId = OperatingSystems.Count;
            OperatingSystems.Add(operatingSystem);
        } // CreateOperatingSystem.

        public SoftwareApplication ReadSoftwareApplication(long id, bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false, bool loadRatingDeveloper = false)
        {
            return SoftwareApplications.FirstOrDefault(s => s.SoftwareApplicationId == id);
        } // ReadSoftwareApplication.

        public IEnumerable<SoftwareApplication> ReadAllSoftwareApplications(bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false)
        {
            return SoftwareApplications;
        } // ReadAllSoftwareApplications.

        public IEnumerable<SoftwareApplication> ReadSoftwareApplicationsByOfType(AppType.Type type, bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false)
        {
            return SoftwareApplications.Where(s => s.ApplicationType == type);
        } // ReadSoftwareApplicationsByOfType.

        public void CreateSoftwareApplication(SoftwareApplication softwareApplication)
        {
            softwareApplication.SoftwareApplicationId = SoftwareApplications.Count;
            SoftwareApplications.Add(softwareApplication);
        } // CreateSoftwareApplication.
        
        public Developer CreateDeveloper(Developer developer)
        {
            developer.DeveloperId = Developers.Count;
            Developers.Add(developer);
            return developer;
        } // CreateSoftwareApplication.

        public Developer ReadDeveloper(long id, bool loadDevelopedApplications = false, bool loadRatedApplications = false)
        {
            return Developers.FirstOrDefault(d => d.DeveloperId == id);
        } // ReadDeveloper.

        public IEnumerable<Developer> ReadAllDevelopers(bool loadDevelopedApplications = false, bool loadRatedApplications = false)
        {
            return Developers;
        }  // ReadAllDevelopers

        public Developer UpdateDeveloper(Developer developer)
        {
            var developerRepo = ReadDeveloper(developer.DeveloperId);
            if (developerRepo != null)
            {
                developerRepo.Name = developer.Name;
                developerRepo.Description = developer.Description;
                developerRepo.BirthDate = developer.BirthDate;
                developerRepo.ProfilePicture = developer.ProfilePicture;
                developerRepo.PhoneNumber = developer.PhoneNumber;
                developer.Email = developer.Email;
            }

            return developerRepo;
        } // UpdateDeveloper.

        public bool DeleteDeveloper(long id)
        {
            var developerToDelete = ReadDeveloper(id);
            if (developerToDelete != null)
                Developers.Remove(developerToDelete);

            return developerToDelete != null;
        } // DeleteDeveloper.
        
        public bool DoesDeveloperExis(long id)
        {
            return Developers.Any(d => d.DeveloperId == id);
        } // DoesDeveloperExis.

        public void CreateRating(Rating rating)
        {
            Ratings.Add(rating);
        } // CreateRating.

        public Rating ReadRating(long developerId, long softwareApplicationId, bool loadDeveloper = false, bool loadSoftwareApplication = false)
        {
            return Ratings.FirstOrDefault(r => r.Developer.DeveloperId == developerId && r.SoftwareApplication.SoftwareApplicationId == softwareApplicationId);
        } // ReadRating.

        public IEnumerable<Rating> ReadAllRatings(bool loadDeveloper = false, bool loadSoftwareApplication = false)
        {
            return Ratings;
        } // ReadAllRatings.

        public Address ReadAddress(long id)
        {
            return Developers.First(d => d.DeveloperId == id).Address;
        } // ReadAddress.

        public IEnumerable<Address> ReadAllAddresses()
        {
            ICollection<Address> addresses = new List<Address>();

            foreach (var developer in Developers)
            {
                addresses.Add(developer.Address);
            } // Foreach.

            return addresses.AsEnumerable();
        } // ReadAllAddresses.

        public void UpdateSoftwareApplication(SoftwareApplication softwareApplication)
        {
            throw new NotImplementedException();
            foreach (var application in SoftwareApplications)
            {
                if (application.SoftwareApplicationId == softwareApplication.SoftwareApplicationId)
                {
                    SoftwareApplications.Remove(application);
                    SoftwareApplications.Add(softwareApplication);
                } // If.
            } // Foreach.
        } // UpdateSoftwareApplication.

        public void UpdateOperatingSystem(OperatingSystem operatingSystem)
        {
            throw new NotImplementedException();
            foreach (var os in OperatingSystems)
            {
                if (operatingSystem.OperatingSystemId == os.OperatingSystemId)
                {
                    OperatingSystems.Remove(os);
                    OperatingSystems.Add(operatingSystem);
                } // If.
            } // Foreach.
        } // UpdateOperatingSystem.

        public IEnumerable<SoftwareApplication> ReadSoftwareApplicationsOfOperatingSystem(long id)
        {
            throw new NotImplementedException();
        } // ReadSoftwareApplicationsOfOperatingSystem.
      
        
        // Fill the lists.
        private void Seed()
        {
            
            // Create developers.
            var devJetbrains = new Developer()
            {
                Name = "Jetbrains S.R.0",
                Description = "JetBrains s.r.o. is a Czech software development company which makes tools for software" +
                              " developers and project managers. As of 2019, the company has offices in Prague, Saint Petersburg, " +
                              " Moscow, Munich, Boston, Novosibirsk, Amsterdam, Foster City and Marlton, New Jersey.",
                ProfilePicture = "jetbrains.jpg",
                PhoneNumber = "+420 2 4172 2501",
                Email = "sales@jetbrains.com",
                BirthDate = DateTime.ParseExact("14-08-2000", "dd-MM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture)
            };
            var devNiels = new Developer()
            {
                Name = "Niels Van Steen",
                Description = "Currently Studying Applied Information Technology at KdG (Karel de Grote University College. & Proud developer of the Software Application Management website.",
                ProfilePicture = "niels.png",
                PhoneNumber = "(+32)468 25 56 91",
                Email = "niels.vansteen@student.kdg.be",
                BirthDate = DateTime.ParseExact("02-01-2002", "dd-MM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture)
            };

            // Create Software applications.
             var softwareIntellij = new SoftwareApplication()
            {
                Name = "Intellij",
                Score = 9.1,
                Description = "Intellij is and IDE developed by Jetbrains, It has a Standard and Ultimate version. Intellij is mainly used to Develop Java applications.",
                ImagePath = "intellij.png",
                ReleaseDate = DateTime.ParseExact("01-01-2001", "dd-MM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture),
                ApplicationType = AppType.Type.Free,
                Downloads = 560000,
                Developer = devJetbrains
            };
            var softwareRider = new SoftwareApplication()
            {
                Name = "Rider",
                Score = 8.5,
                Description = "JetBrains Rider is a cross-platform .NET IDE based on the IntelliJ platform and ReSharper.",
                ImagePath = "rider.png",
                ReleaseDate = DateTime.ParseExact("05-07-2011", "dd-MM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture),
                ApplicationType = AppType.Type.Paid,
                Downloads = 3264000,
                Developer = devJetbrains
            };
            SoftwareApplication softwarePycharm = new SoftwareApplication()
            {
                Name = "Pycharm",
                Score = 7.5,
                Description = "PyCharm knows everything about your code. Rely on it for intelligent code completion, on-the-fly error checking and quick-fixes, easy project navigation, and much more.",
                ImagePath = "pycharm.png",
                ReleaseDate = DateTime.ParseExact("03-02-2007", "dd-MM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture),
                ApplicationType = AppType.Type.Both,
                Downloads = 89200,
                Developer = devJetbrains
            };
            var softwareResharper = new SoftwareApplication()
            {
                Name = "Resharper",
                Score = 6.8,
                Description = "ReSharper extends Visual Studio with over 2200 on-the-fly code inspections for C#, VB.NET, ASP.NET, JavaScript, TypeScript and other technologies. For most inspections, ReSharper provides quick-fixes (light bulbs) to improve the code." +
                    "Find and remove unused code? Migrate your code to the latest C# version? Convert loops to LINQ at will? Find and prevent possible exceptions? Use a common naming standard? All that and a lot more code improvements are made possible with ReSharper's code analysis.",
                ImagePath = "resharper.png",
                ReleaseDate = DateTime.ParseExact("15-09-2012", "dd-MM-yyyy",
                    System.Globalization.CultureInfo.InvariantCulture),
                ApplicationType = AppType.Type.Both,
                Downloads = 25961,
                Developer = devJetbrains
            };
            
            //var softwareIntellij = new SoftwareApplication( "Intellij", 9.1, DateTime.ParseExact("01-01-2001", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), AppType.Type.Both, 560000, devJetbrains);
            //var softwareRider = new SoftwareApplication("Rider", 8.5, DateTime.ParseExact("05-07-2011", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), AppType.Type.Both, 32000, devJetbrains);
            //var softwarePycharm = new SoftwareApplication( "Pycharm", 7.5, DateTime.ParseExact("03-02-2007", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), AppType.Type.Both, 89000, devJetbrains);
            //var softwareResharper = new SoftwareApplication( "Resharper", 6.8, DateTime.ParseExact("15-09-2012", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), AppType.Type.Both, 25000, devJetbrains);
            
            // Create operatings systems.
             var osWindows10 = new OperatingSystem()
            {
                Name = "Windows 10",
                ReleaseDate = DateTime.ParseExact("29-07-2015", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                Description =
                    "Windows 10 is a major release of the Windows NT operating system developed by Microsoft. It is the successor" +
                    " to Windows 8.1, which was released nearly two years earlier, and itself was released to manufacturing on July 15, " +
                    " 2015, and broadly released for the general public on July 29, 2015.",
                BannerImage = "windows10.png"
            };
            var osMacOs12 = new OperatingSystem()
            {
                Name = "MacOS 12",
                ReleaseDate = DateTime.ParseExact("07-06-2021", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                Description = "macOS Monterey is the 18th and current major release of macOS, Apple's desktop operating system for Macintosh " +
                              "computers. The successor to macOS Big Sur, it was announced at WWDC 2021 on June 7, 2021, and released on October 25, 2021.",
                    BannerImage = "macOS12.png"
            };
            var osUbuntu20 = new OperatingSystem()
            {
                Name = "Ubuntu 20.04 LTS",
                ReleaseDate = DateTime.ParseExact("23-04-2020", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                Description = "Ubuntu is a Linux distribution based on Debian and composed mostly of free and open-source software. Ubuntu " +
                              "is officially released in three editions: Desktop, Server, and Core for Internet of things devices and robots. " +
                              " All the editions can run on the computer alone, or in a virtual machine.",
                BannerImage = "ubuntu20.04.png"
            };
            var osAndroid11 = new OperatingSystem()
            {
                Name = "Ubuntu 20.04 LTS",
                ReleaseDate = DateTime.ParseExact("08-09-2020", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture),
                Description = "Android 11 is the eleventh major release and 18th version of Android, the mobile operating system developed by the " +
                              "Open Handset Alliance led by Google. It was released on September 8, 2020.",
                BannerImage = "android11.png"
            };
            //var osWindows10 = new BL.Domain.OperatingSystem( "Windows 10", DateTime.ParseExact("29-07-2015", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture));
            //var osMacOs12 = new BL.Domain.OperatingSystem( "MacOS 12", DateTime.ParseExact("07-06-2021", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture));
            //var osUbuntu20 = new BL.Domain.OperatingSystem( "Ubuntu 20.04 LTS", DateTime.ParseExact("23-04-2020", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture));
            //var osAndroid11 = new BL.Domain.OperatingSystem( "Android 11", DateTime.ParseExact("08-09-2020", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture));
            
            // Add the objects to the list and create an ID.
            CreateOperatingSystem(osWindows10);
            CreateOperatingSystem(osMacOs12);
            CreateOperatingSystem(osUbuntu20);
            CreateOperatingSystem(osAndroid11);
            
            CreateSoftwareApplication(softwareIntellij);
            CreateSoftwareApplication(softwareRider);
            CreateSoftwareApplication(softwarePycharm);
            CreateSoftwareApplication(softwareResharper);
            
            CreateDeveloper(devJetbrains);
            
            // Add software applications to the list in each Operating System class.
            osWindows10.SoftwareApplications.Add(softwareIntellij);
            osWindows10.SoftwareApplications.Add(softwareRider);
            osWindows10.SoftwareApplications.Add(softwarePycharm);
            osWindows10.SoftwareApplications.Add(softwareResharper);
            
            osMacOs12.SoftwareApplications.Add(softwareIntellij);
            osMacOs12.SoftwareApplications.Add(softwareRider);
            osMacOs12.SoftwareApplications.Add(softwarePycharm);
            osMacOs12.SoftwareApplications.Add(softwareResharper);
            
            osUbuntu20.SoftwareApplications.Add(softwareIntellij);
            osUbuntu20.SoftwareApplications.Add(softwareRider);
            osUbuntu20.SoftwareApplications.Add(softwarePycharm);
            osUbuntu20.SoftwareApplications.Add(softwareResharper);
            
            // Add the operating systems to the list in each software application class.
            softwareIntellij.AvailableOnOperationSystems.Add(osWindows10);
            softwareIntellij.AvailableOnOperationSystems.Add(osMacOs12);
            softwareIntellij.AvailableOnOperationSystems.Add(osUbuntu20);
            
            softwareRider.AvailableOnOperationSystems.Add(osWindows10);
            softwareRider.AvailableOnOperationSystems.Add(osMacOs12);
            softwareRider.AvailableOnOperationSystems.Add(osUbuntu20);
            
            softwarePycharm.AvailableOnOperationSystems.Add(osWindows10);
            softwarePycharm.AvailableOnOperationSystems.Add(osMacOs12);
            softwarePycharm.AvailableOnOperationSystems.Add(osUbuntu20);
            
            // Add the application the developers developed.
            devJetbrains.DevelopedApplications.Add(softwareIntellij);
            devJetbrains.DevelopedApplications.Add(softwareRider);
            devJetbrains.DevelopedApplications.Add(softwarePycharm);
            devJetbrains.DevelopedApplications.Add(softwareResharper);
            
        } // Seed.

        
    }
}