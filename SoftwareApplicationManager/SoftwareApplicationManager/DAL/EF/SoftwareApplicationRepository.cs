using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SoftwareApplicationManager.BL.Domain;
using OperatingSystem = SoftwareApplicationManager.BL.Domain.OperatingSystem;

namespace SoftwareApplicationManager.DAL.EF
{
    public class SoftwareApplicationRepository : IRepository
    {
        // Fields.
        private readonly SoftwareApplicationManagerContext _context;
        
        // Constructor.
        public SoftwareApplicationRepository(SoftwareApplicationManagerContext context)
        {
            _context = context;
        } // Repository.
        
        // Methods.
        
        public OperatingSystem ReadOperatingSystem(long id, bool loadSoftwareApplications = false, bool loadDeveloperOfSoftwareApplications = false)
        {
            IQueryable<OperatingSystem> operatingSystems = _context.OperatingSystems;

            if (loadSoftwareApplications)
                operatingSystems = operatingSystems.Include(o => o.SoftwareApplications);

            if (loadDeveloperOfSoftwareApplications)
                operatingSystems = operatingSystems
                    .Include(o => o.SoftwareApplications)
                    .ThenInclude(s => s.Developer);

            return operatingSystems.SingleOrDefault(o => o.OperatingSystemId == id);
        } // ReadOperatingSystem.

        public IEnumerable<OperatingSystem> ReadAllOperatingSystems(bool loadSoftwareApplications = false)
        {
            IQueryable<OperatingSystem> operatingSystems = _context.OperatingSystems;

            if (loadSoftwareApplications)
                operatingSystems = operatingSystems.Include(o => o.SoftwareApplications);

            return operatingSystems.AsEnumerable();
        } // ReadAllOperatingSystems.

        public IEnumerable<OperatingSystem> ReadOperatingSystemsByNameAndReleaseDate(string name, DateTime? releaseDate, bool loadSoftwareApplications = false)
        {
            IQueryable<OperatingSystem> operatingSystems = _context.OperatingSystems;

            if (!String.IsNullOrWhiteSpace(name))
                operatingSystems = operatingSystems.Where(o => o.Name.ToLower().Contains(name.ToLower()));

            if (releaseDate != null)
                operatingSystems = operatingSystems.Where(o => o.ReleaseDate.Equals(releaseDate));
            
            if (loadSoftwareApplications)
                operatingSystems = operatingSystems.Include(o => o.SoftwareApplications);

            return operatingSystems.AsEnumerable();
        } // ReadOperatingSystemsByNameAndReleaseDate.

        public void CreateOperatingSystem(OperatingSystem operatingSystem)
        {
            _context.OperatingSystems.Add(operatingSystem);
            _context.SaveChanges();
        } // CreateOperatingSystem.

        public SoftwareApplication ReadSoftwareApplication(long id, bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false , bool loadRatingDeveloper = false)
        {
            IQueryable<SoftwareApplication> softwareApplications = _context.SoftwareApplications;

            if (loadOperatingSystems)
                softwareApplications = softwareApplications.Include(s => s.AvailableOnOperationSystems);

            if (loadDevelopers)
                softwareApplications = softwareApplications.Include(s => s.Developer);

            if (loadRatings)
                softwareApplications = softwareApplications.Include(s => s.Ratings);

            if (loadRatingDeveloper)
                softwareApplications = softwareApplications
                    .Include(s => s.Ratings)
                    .ThenInclude(r => r.Developer);

            return softwareApplications.SingleOrDefault(s => s.SoftwareApplicationId == id);
        } // ReadSoftwareApplication.

        public IEnumerable<SoftwareApplication> ReadAllSoftwareApplications(bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false)
        {
            IQueryable<SoftwareApplication> softwareApplications = _context.SoftwareApplications;

            if (loadOperatingSystems)
                softwareApplications = softwareApplications.Include(s => s.AvailableOnOperationSystems);

            if (loadDevelopers)
                softwareApplications = softwareApplications.Include(s => s.Developer);

            if (loadRatings)
                softwareApplications = softwareApplications.Include(s => s.Ratings);
            
            return softwareApplications.AsEnumerable();
        } // ReadAllSoftwareApplications.

        public IEnumerable<SoftwareApplication> ReadSoftwareApplicationsByOfType(AppType.Type type, bool loadOperatingSystems = false, bool loadDevelopers = false, bool loadRatings = false)
        {
            IQueryable<SoftwareApplication> softwareApplications = _context.SoftwareApplications;

            if (loadOperatingSystems)
                softwareApplications = softwareApplications.Include(s => s.AvailableOnOperationSystems);

            if (loadDevelopers)
                softwareApplications = softwareApplications.Include(s => s.Developer);

            if (loadRatings)
                softwareApplications = softwareApplications.Include(s => s.Ratings);
            
            return softwareApplications.Where<SoftwareApplication>(s => s.ApplicationType.Equals(type)).AsEnumerable();
        } // ReadSoftwareApplicationsByOfType.

        public void CreateSoftwareApplication(SoftwareApplication softwareApplication)
        {
            _context.SoftwareApplications.Add(softwareApplication);
            _context.SaveChanges();
        } // CreateSoftwareApplication.

        public Developer CreateDeveloper(Developer developer)
        {
            _context.Developers.Add(developer);
            _context.SaveChanges();
            return developer;
        } // CreateDeveloper.

        public Developer ReadDeveloper(long id, bool loadDevelopedApplications = false, bool loadRatedApplications = false)
        {
            IQueryable<Developer> developers = _context.Developers;

            if (loadDevelopedApplications)
                developers = developers.Include(d => d.DevelopedApplications);

            if (loadRatedApplications)
                developers = developers.Include(d => d.RatedApplications);

            return developers.SingleOrDefault(d => d.DeveloperId == id);
        } // ReadDeveloper.

        public IEnumerable<Developer> ReadAllDevelopers(bool loadDevelopedApplications = false, bool loadRatedApplications = false)
        {
            IQueryable<Developer> developers = _context.Developers;

            if (loadDevelopedApplications)
                developers = developers.Include(d => d.DevelopedApplications);

            if (loadRatedApplications)
                developers = developers.Include(d => d.RatedApplications);

            return developers.AsEnumerable();
        } // ReadAllDevelopers.

        public Developer UpdateDeveloper(Developer developer)
        {
            //_context.Developers.Update(developer);
            var dev = _context.Developers.SingleOrDefault(d => d.DeveloperId == developer.DeveloperId);
            if (dev == null) return null;
            _context.Entry(dev).CurrentValues.SetValues(developer);
            _context.SaveChanges();
            return developer;
        } // UpdateDeveloper.

        public bool DeleteDeveloper(long id)
        {
            Developer developer = _context.Developers.SingleOrDefault(d => d.DeveloperId == id);
            if (developer == null)
                return false;
            _context.Developers.Remove(developer);
            _context.SaveChanges();
            return true;
        } // DeleteDeveloper.

        public bool DoesDeveloperExis(long id)
        {
            return _context.Developers.SingleOrDefault(d => d.DeveloperId == id) != null;
        } // DoesDeveloperExis.
        
        public void CreateRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            _context.Entry(rating.Developer).State = EntityState.Unchanged;
            _context.Entry(rating.SoftwareApplication).State = EntityState.Unchanged;
            _context.SaveChanges();
        } // CreateRating.

        public Rating ReadRating(long developerId, long softwareApplicationId, bool loadDeveloper = false, bool loadSoftwareApplication = false)
        {
            IQueryable<Rating> ratings = _context.Ratings;

            if (loadDeveloper)
                ratings = ratings.Include(r => r.Developer);

            if (loadSoftwareApplication)
                ratings = ratings.Include(r => r.SoftwareApplication);

            return ratings.SingleOrDefault(r =>
                r.SoftwareApplication.SoftwareApplicationId == softwareApplicationId &&
                r.Developer.DeveloperId == developerId);
        } // ReadRating.

        public IEnumerable<Rating> ReadAllRatings(bool loadDeveloper = false, bool loadSoftwareApplication = false)
        {
            IQueryable<Rating> ratings = _context.Ratings;

            if (loadDeveloper)
                ratings = ratings.Include(r => r.Developer);

            if (loadSoftwareApplication)
                ratings = ratings.Include(r => r.SoftwareApplication);

            return ratings.AsEnumerable();
        } // ReadAllRatings.
        

        public Address ReadAddress(long id)
        {
            Developer developer = _context.Developers.Find(id);
            return developer.Address;
        } // ReadAddress.

        public IEnumerable<Address> ReadAllAddresses()
        {
            ICollection<Address> addresses = new List<Address>();
            
            foreach (var developer in _context.Developers)
            {
                addresses.Add(developer.Address);
            } // Foreach.
            
            return addresses;
        } // ReadAddress.

        public void UpdateSoftwareApplication(SoftwareApplication softwareApplication)
        {
            _context.SoftwareApplications.Update(softwareApplication);
            _context.SaveChanges();
        } // UpdateSoftwareApplication.

        public void UpdateOperatingSystem(OperatingSystem operatingSystem)
        {
            _context.OperatingSystems.Update(operatingSystem);
            _context.SaveChanges();
        } // UpdateOperatingSystem.

        public IEnumerable<SoftwareApplication> ReadSoftwareApplicationsOfOperatingSystem(long id)
        {
            return _context.OperatingSystems
                .Where(c => c.OperatingSystemId == id)
                .SelectMany(c => c.SoftwareApplications).AsEnumerable();
        } // ReadSoftwareApplicationsOfOperatingSystem.
        
       
        
        
        
    }
}