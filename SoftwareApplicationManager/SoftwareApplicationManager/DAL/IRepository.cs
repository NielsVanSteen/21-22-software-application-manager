using System;
using System.Collections.Generic;
using SoftwareApplicationManager.BL.Domain;
using OperatingSystem = SoftwareApplicationManager.BL.Domain.OperatingSystem;

namespace SoftwareApplicationManager.DAL
{
    public interface IRepository
    {
        // Operating System.
        OperatingSystem ReadOperatingSystem(long id, bool loadSoftwareApplications = false, bool loadDeveloperOfSoftwareApplications = false);
        IEnumerable<OperatingSystem> ReadAllOperatingSystems(bool loadSoftwareApplications = false);
        IEnumerable<OperatingSystem> ReadOperatingSystemsByNameAndReleaseDate(string name, DateTime? releaseDate, bool loadSoftwareApplications = false);
        void CreateOperatingSystem(OperatingSystem operatingSystem);
        
        // Software application
        SoftwareApplication ReadSoftwareApplication(long id, bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false, bool loadRatingDeveloper = false);
        IEnumerable<SoftwareApplication> ReadAllSoftwareApplications(bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false);
        IEnumerable<SoftwareApplication> ReadSoftwareApplicationsByOfType(AppType.Type type, bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false);
        void CreateSoftwareApplication(SoftwareApplication softwareApplication);
        
        // Developer.
        Developer CreateDeveloper(Developer developer);
        Developer ReadDeveloper(long id, bool loadDevelopedApplications = false, bool loadRatedApplications = false);
        IEnumerable<Developer> ReadAllDevelopers(bool loadDevelopedApplications = false, bool loadRatedApplications = false);
        Developer UpdateDeveloper(Developer developer);
        bool DeleteDeveloper(long id);
        bool DoesDeveloperExis(long id);
        
        // Rating.
        void CreateRating(Rating rating);
        Rating ReadRating(long developerId, long softwareApplicationId, bool loadDeveloper = false, bool loadSoftwareApplication = false);
        IEnumerable<Rating> ReadAllRatings(bool loadDeveloper = false, bool loadSoftwareApplication = false);
        
        // Address.
        Address ReadAddress(long id);
        IEnumerable<Address> ReadAllAddresses();
        
        // Update. 
        void UpdateSoftwareApplication(SoftwareApplication softwareApplication);
        void UpdateOperatingSystem(OperatingSystem operatingSystem);
        
        // Extra.
        IEnumerable<SoftwareApplication> ReadSoftwareApplicationsOfOperatingSystem(long id);
    }
}