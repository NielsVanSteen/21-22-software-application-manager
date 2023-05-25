using SoftwareApplicationManager.BL.Domain;

namespace SoftwareApplicationManager.UI.CA.Extensions
{
    public static class DeveloperExtensions
    {
        public static string GetInfo(this Developer developer)
        {
            return $"{developer.DeveloperId, -5}: {developer.Name,20} {developer.Description} {developer.BirthDate}";
        }
    }
}