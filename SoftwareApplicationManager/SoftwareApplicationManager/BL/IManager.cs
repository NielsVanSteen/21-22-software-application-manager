using System;
using System.Collections;
using System.Collections.Generic;
using SoftwareApplicationManager.BL.Domain;
using OperatingSystem = SoftwareApplicationManager.BL.Domain.OperatingSystem;

namespace SoftwareApplicationManager.BL
{
    public interface IManager
    {
        // Operating System.
        Domain.OperatingSystem GetOperatingSystem(long id, bool loadSoftwareApplications = false, bool loadDeveloperOfSoftwareApplications = false);
        IEnumerable<Domain.OperatingSystem> GetAllOperatingSystems(bool loadSoftwareApplications = false);
        IEnumerable<Domain.OperatingSystem> GetOperatingSystemsByNameAndReleaseDate(string name, DateTime? releaseDate, bool loadSoftwareApplications = false);
        Domain.OperatingSystem AddOperatingSystem(string name, DateTime releaseDate, string description, string bannerImage);
        
        // Software Application.
        SoftwareApplication GetSoftwareApplication(long id, bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false, bool loadRatingDeveloper = false);
        IEnumerable<SoftwareApplication> GetAllSoftwareApplications(bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false);
        IEnumerable<SoftwareApplication> GetSoftwareApplicationsOfType(AppType.Type type, bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false);
        SoftwareApplication AddSoftwareApplication(string name, double? score, string description, string imagePath, DateTime releaseDate, AppType.Type applicationType, int downloads, Developer developer = null);
        SoftwareApplication AddSoftwareApplication(SoftwareApplication softwareApplication);
        
        // Developer.
        Developer GetDeveloper(long id, bool loadDevelopedApplications = false, bool loadRatedApplications = false);
        IEnumerable<Developer> GetAllDevelopers(bool loadDevelopedApplications = false, bool loadRatedApplications = false);
        Developer AddDeveloper(string name, string description, string profilePicture, string phoneNumber, string email, DateTime birthDate, Address address = null);
        Developer AddDeveloper(Developer developer);
        bool DoesDeveloperExist(long id);
        Developer ChangeDeveloper(string name, string description, DateTime birthDate, string profilePicture, string phoneNumber, string email);
        Developer ChangeDeveloper(Developer developer);
        bool RemoveDeveloper(long id);
        
        // Rating.
        Rating AddRating(string title, double score, DateTime dateTime, string description = null, Developer developer = null, SoftwareApplication softwareApplication = null);
        Rating AddRating(Rating rating);
        Rating GetRating(long developerId, long softwareApplicationId, bool loadDeveloper = false, bool loadSoftwareApplication = false);
        IEnumerable<Rating> GetAllRatings(bool loadDeveloper = false, bool loadSoftwareApplication = false);
        
        // Address.
        Address AddAddress(string street, int number, string postalCode, string city, string country, Developer developer = null);
        Address GetAddress(long id);
        IEnumerable<Address> GetAllAddresses();
        
        // Extra.
        void AddSoftwareApplicationToOperatingSystem(long softwareApplicationId, long operatingSystemId);
        void RemoveSoftwareApplicationToOperatingSystem(long softwareApplicationId, long operatingSystemId);
        IEnumerable<SoftwareApplication> GetSoftwareApplicationsOfOperatingSystem(long id);

    }
}