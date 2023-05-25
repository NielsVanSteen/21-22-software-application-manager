using System;
using SoftwareApplicationManager.BL.Domain;
using OperatingSystem = SoftwareApplicationManager.BL.Domain.OperatingSystem;

namespace SoftwareApplicationManager.DAL.EF
{
    internal static class SoftwareApplicationInitializer
    {
        // Fields.
        private static bool _isInitialized;
        
        // Methods.
        
        public static void Initialize(SoftwareApplicationManagerContext context, bool rebuildEntireDb = false)
        {
            if (_isInitialized || rebuildEntireDb == false)
                return;
            
            DropDb(context);

            if (IsDbCreatedNow(context))
                InitializeDb(context);
            
            SaveChanges(context);
            
        } // initialize.

        private static bool DropDb(SoftwareApplicationManagerContext context)
        {
            return context.Database.EnsureDeleted();
        } // dropDb.

        private static bool IsDbCreatedNow(SoftwareApplicationManagerContext context)
        {
            return context.Database.EnsureCreated();
        } // IsDbCreatedNow.

        private static void InitializeDb(SoftwareApplicationManagerContext context)
        {
            _isInitialized = true;
            Seed(context);
        } // initializeDb.

        private static void SaveChanges(SoftwareApplicationManagerContext context)
        {
            context.SaveChanges();
            context.ChangeTracker.Clear();
        } // saveChanges.

        private static void Seed(SoftwareApplicationManagerContext context)
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
            
            //var softwareIntellij = new SoftwareApplication( "Intellij", 9.1, DateTime.ParseExact("01-01-2001", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), AppType.Type.Free, 560000, devJetbrains);
            //var softwareRider = new SoftwareApplication("Rider", 8.5, DateTime.ParseExact("05-07-2011", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), AppType.Type.Paid, 3264000, devJetbrains);
            //var softwarePycharm = new SoftwareApplication( "Pycharm", 7.5, DateTime.ParseExact("03-02-2007", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), AppType.Type.Both, 89200, devJetbrains);
            //var softwareResharper = new SoftwareApplication( "Resharper", 6.8, DateTime.ParseExact("15-09-2012", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), AppType.Type.Both, 25900, devJetbrains);
            
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
            //var osWindows10 = new OperatingSystem( "Windows 10", DateTime.ParseExact("29-07-2015", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture));
            //var osMacOs12 = new OperatingSystem( "MacOS 12", );
            //var osUbuntu20 = new OperatingSystem( "Ubuntu 20.04 LTS", DateTime.ParseExact("23-04-2020", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture));
            //var osAndroid11 = new OperatingSystem( "Android 11", DateTime.ParseExact("08-09-2020", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture));

            // Create Ratings.
            var ratingRiderByJetbrains = new Rating("Good", 8.5, DateTime.Now, devJetbrains, softwareRider, "A well developed IDE.");
            var ratingRiderByNiels = new Rating("Mediocre", 8.3, DateTime.Now, devNiels, softwareRider, "Although I like using Rider, I prefer Visual Studio");
            var ratingIntellijByJetbrains = new Rating("Excellent software", 9.2, DateTime.Now, devJetbrains, softwareIntellij, "Intellij has been a huge success for us, we have been developing Intellij for a long time, and will be doing so for the near future as well. Intellij is a good example of all that Jetbrains stands for, Simplistic but Excellent!");
            var ratingIntellijByNiels = new Rating("Superb", 7.1, DateTime.Now, devNiels, softwareIntellij, "Great Software!");
            
            // Create Addresses.
            var addressJetbrains = new Address("Na Hřebenech II ", 1718, "140 00", "Prague", "Czech Republic", devJetbrains);
            var addressNiels = new Address("SomeStreet", 1, "2390", "Oostmalle", "Belgium", devNiels);
            
            // Add the objects to the list and create an ID.
            context.OperatingSystems.Add(osWindows10);
            context.OperatingSystems.Add(osMacOs12);
            context.OperatingSystems.Add(osUbuntu20);
            context.OperatingSystems.Add(osAndroid11);

            context.SoftwareApplications.Add(softwareIntellij);
            context.SoftwareApplications.Add(softwareRider);
            context.SoftwareApplications.Add(softwarePycharm);
            context.SoftwareApplications.Add(softwareResharper);

            context.Developers.Add(devJetbrains);
            context.Developers.Add(devNiels);

            context.Ratings.Add(ratingRiderByJetbrains);
            context.Ratings.Add(ratingRiderByNiels);
            context.Ratings.Add(ratingIntellijByJetbrains);
            context.Ratings.Add(ratingIntellijByNiels);

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
            
            // Add the rating the developers rated to the Software instances.
            softwareRider.Ratings.Add(ratingRiderByJetbrains);
            softwareRider.Ratings.Add(ratingRiderByNiels);
            softwareIntellij.Ratings.Add(ratingIntellijByJetbrains);
            softwareIntellij.Ratings.Add(ratingIntellijByNiels);
            
            // Add the rating the developers rated to the Developer instances.
            devJetbrains.RatedApplications.Add(ratingIntellijByJetbrains);
            devJetbrains.RatedApplications.Add(ratingRiderByJetbrains);
            devNiels.RatedApplications.Add(ratingRiderByNiels);
            devNiels.RatedApplications.Add(ratingIntellijByNiels);
            
            // Add addressees to addresses.
            devJetbrains.Address = addressJetbrains;
            devNiels.Address = addressNiels;
        } // seed.

    }
}