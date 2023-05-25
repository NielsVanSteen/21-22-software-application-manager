using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SoftwareApplicationManager.BL.Domain;
using SoftwareApplicationManager.DAL;
using OperatingSystem = SoftwareApplicationManager.BL.Domain.OperatingSystem;

namespace SoftwareApplicationManager.BL
{
    public class ApplicationManager : IManager
    {
        // Fields.
        private readonly IRepository _repository;

        // Constructor.
        public ApplicationManager(IRepository repository)
        {
            _repository = repository;
        } // Manager.

        // Methods.
        public Domain.OperatingSystem GetOperatingSystem(long id, bool loadSoftwareApplications = false, bool loadDeveloperOfSoftwareApplications = false)
        {
            return _repository.ReadOperatingSystem(id, loadSoftwareApplications, loadDeveloperOfSoftwareApplications);
        } // GetOperatingSystem.

        public IEnumerable<Domain.OperatingSystem> GetAllOperatingSystems(bool loadSoftwareApplications = false)
        {
            return _repository.ReadAllOperatingSystems(loadSoftwareApplications);
        } // GetAllOperatingSystems.

        public IEnumerable<Domain.OperatingSystem> GetOperatingSystemsByNameAndReleaseDate(string name, DateTime? releaseDate, bool loadSoftwareApplications = false)
        {
            return _repository.ReadOperatingSystemsByNameAndReleaseDate(name, releaseDate, loadSoftwareApplications);
        } // GetOperatingSystemsByNameAndReleaseDate.

        public OperatingSystem AddOperatingSystem(string name, DateTime releaseDate, string description, string bannerImage)
        {
            OperatingSystem operatingSystem = new OperatingSystem(name, releaseDate, description, bannerImage);
           
            Validator.ValidateObject(operatingSystem, new ValidationContext(operatingSystem), validateAllProperties: true);

            _repository.CreateOperatingSystem(operatingSystem);
            return operatingSystem;
        }  // AddOperatingSystem.

        public SoftwareApplication GetSoftwareApplication(long id, bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false, bool loadRatingDeveloper = false)
        {
            return _repository.ReadSoftwareApplication(id, loadOperatingSystems, loadDevelopers, loadRatings, loadRatingDeveloper);
        } // GetSoftwareApplication.

        public IEnumerable<SoftwareApplication> GetAllSoftwareApplications(bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false)
        {
            return _repository.ReadAllSoftwareApplications(loadOperatingSystems, loadDevelopers, loadRatings);
        } // GetAllSoftwareApplications.

        public IEnumerable<SoftwareApplication> GetSoftwareApplicationsOfType(AppType.Type type, bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false)
        {
            return _repository.ReadSoftwareApplicationsByOfType(type, loadOperatingSystems, loadDevelopers, loadRatings);
        } // GetSoftwareApplicationsByType.

        public SoftwareApplication AddSoftwareApplication(string name, double? score, string description, string imagePath, DateTime releaseDate, AppType.Type applicationType, int downloads, Developer developer = null)
        {
            SoftwareApplication softwareApplication = new SoftwareApplication(name, score, description, imagePath, releaseDate, applicationType, downloads, developer);
            AddSoftwareApplication(softwareApplication);
            return softwareApplication;
        } // AddSoftwareApplication.

        public SoftwareApplication AddSoftwareApplication(SoftwareApplication softwareApplication)
        {
            Validator.ValidateObject(softwareApplication, new ValidationContext(softwareApplication), validateAllProperties: true);
            _repository.CreateSoftwareApplication(softwareApplication);
            return softwareApplication;
        } // AddSoftwareApplication.

        public Developer AddDeveloper(string name, string description, string profilePicture, string phoneNumber, string email, DateTime birthDate, Address address = null)
        {
            Developer developer = new Developer(name, description, profilePicture, phoneNumber, email, birthDate);
            
            // If address (owned type) is given, link Address to developer and vice versa.
            if (address != null)
            {
                address.Addressee = developer;
                developer.Address = address;
            } // Address.
            
            Validator.ValidateObject(developer, new ValidationContext(developer), validateAllProperties: true);
            
            return _repository.CreateDeveloper(developer);
        } // AddDeveloper.

        public Developer AddDeveloper(Developer developer)
        {
            Validator.ValidateObject(developer, new ValidationContext(developer), validateAllProperties: true);
            return _repository.CreateDeveloper(developer);
        } // AddDeveloper.

        public bool DoesDeveloperExist(long id)
        {
            return _repository.DoesDeveloperExis(id);
        } // DoesDeveloperExist.
        
        public Developer GetDeveloper(long id, bool loadDevelopedApplications = false, bool loadRatedApplications = false)
        {
            return _repository.ReadDeveloper(id, loadDevelopedApplications, loadRatedApplications);
        } // GetDeveloper.

        public IEnumerable<Developer> GetAllDevelopers(bool loadDevelopedApplications = false, bool loadRatedApplications = false)
        {
            return _repository.ReadAllDevelopers(loadDevelopedApplications, loadRatedApplications);
        }  // GetAllDevelopers.
        
        public Developer ChangeDeveloper(string name, string description, DateTime birthDate, string profilePicture, string phoneNumber, string email)
        {
            return null;
        }  // ChangeDeveloper.

        public Developer ChangeDeveloper(Developer developer)
        {
            return _repository.UpdateDeveloper(developer);
        } // ChangeDeveloper.
       

        public bool RemoveDeveloper(long id)
        {
            return _repository.DeleteDeveloper(id);
        } // DeleteDeveloper.

        public Rating AddRating(string title, double score, DateTime dateTime, string description = null, Developer developer = null, SoftwareApplication softwareApplication = null)
        {
            Rating rating = new Rating(title, score, dateTime,description);
            rating.SoftwareApplication = softwareApplication;
            rating.Developer = developer;
            
            Validator.ValidateObject(rating, new ValidationContext(rating), true);
            
            _repository.CreateRating(rating);

            return rating;
        } // AddRating.

        public Rating AddRating(Rating rating)
        {
            _repository.CreateRating(rating);
            return rating;
        } // AddRating.
        
        public Rating GetRating(long developerId, long softwareApplicationId, bool loadDeveloper = false, bool loadSoftwareApplication = false)
        {
            return _repository.ReadRating(developerId, softwareApplicationId, loadDeveloper, loadSoftwareApplication);
        } // GetRating.

        public IEnumerable<Rating> GetAllRatings(bool loadDeveloper = false, bool loadSoftwareApplication = false)
        {
            return _repository.ReadAllRatings(loadDeveloper, loadSoftwareApplication);
        }  // GetAllRatings.

        public Address AddAddress(string street, int number, string postalCode, string city, string country, Developer developer = null)
        {
            // Address is an owned type, it does not have a DbSet. It can only be added by giving the address to the method 'AddDeveloper'.
            return new Address(street, number, postalCode, city, country, developer);
        } // AddAddress.
        
        public Address GetAddress(long id)
        {
            return _repository.ReadAddress(id);
        } // GetAddress.

        public IEnumerable<Address> GetAllAddresses()
        {
            return _repository.ReadAllAddresses();
        } // GetAllAddresses.

        public void AddSoftwareApplicationToOperatingSystem(long softwareApplicationId, long operatingSystemId)
        {
            ChangeLinkSoftwareApplicationOperatingSystem(softwareApplicationId, operatingSystemId, true);
        } // AddSoftwareApplicationToOperatingSystem.

        public void RemoveSoftwareApplicationToOperatingSystem(long softwareApplicationId, long operatingSystemId)
        {
           ChangeLinkSoftwareApplicationOperatingSystem(softwareApplicationId, operatingSystemId, false);
        } // RemoveSoftwareApplicationToOperatingSystem..
        
        public IEnumerable<SoftwareApplication> GetSoftwareApplicationsOfOperatingSystem(long id)
        {
            return _repository.ReadSoftwareApplicationsOfOperatingSystem(id);
        } // GetSoftwareApplicationsOfOperatingSystem.
        
        
        private void ChangeLinkSoftwareApplicationOperatingSystem(long softwareApplicationId, long operatingSystemId, bool add = false)
        {
            // Get the corresponding SoftwareApplication and OperatingSystem.
            SoftwareApplication softwareApplication = _repository.ReadSoftwareApplication(softwareApplicationId);
            OperatingSystem operatingSystem = _repository.ReadOperatingSystem(operatingSystemId);
            
            // Update both instances.
            if (add)
            {
                softwareApplication.AvailableOnOperationSystems.Add(operatingSystem);
                operatingSystem.SoftwareApplications.Add(softwareApplication);
            }
            else
            {
                softwareApplication.AvailableOnOperationSystems.Remove(operatingSystem);
                operatingSystem.SoftwareApplications.Remove(softwareApplication);
            }
            
            // Execute changes.
            _repository.UpdateSoftwareApplication(softwareApplication);
            _repository.UpdateOperatingSystem(operatingSystem);
        } // ChangeLinkSoftwareApplicationOperatingSystem.
        
       
        
    }
}