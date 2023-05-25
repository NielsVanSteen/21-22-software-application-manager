using System.Text;
using SoftwareApplicationManager.BL.Domain;

namespace SoftwareApplicationManager.UI.CA.Extensions
{
    public static class SoftwareApplicationExtensions
    {
        public static string GetInfo(this SoftwareApplication softwareApplication)
        {
            return $"{softwareApplication.SoftwareApplicationId, -2}: {softwareApplication.Name,-20} {softwareApplication.ReleaseDate,0:dd/M/yyyy} {softwareApplication.ApplicationType} " +
                   $"Score: {softwareApplication.Score,-2} {softwareApplication.Downloads,10} Downloads {(softwareApplication.Developer == null ? "" : ("[Developed by: " + softwareApplication.Developer.Name + "]"))}";
        } // GetInfo.
        
        public static string GetInfoWithOperatingSystems(this SoftwareApplication softwareApplication)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(softwareApplication.GetInfo());

            foreach (var operatingSystem in softwareApplication.AvailableOnOperationSystems)
            {
                sb.AppendLine($"\t Operating System: {operatingSystem.GetInfo()}");
            } // Foreach.

            return sb.ToString();

        } // GetInfoWithSoftwareApplications.
    }
}