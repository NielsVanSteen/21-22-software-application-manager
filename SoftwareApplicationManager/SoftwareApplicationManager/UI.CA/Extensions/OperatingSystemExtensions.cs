using System;
using System.Linq;
using System.Text;
using OperatingSystem = SoftwareApplicationManager.BL.Domain.OperatingSystem;

namespace SoftwareApplicationManager.UI.CA.Extensions
{
    public static class OperatingSystemExtension
    {
        public static string GetInfo(this OperatingSystem operatingSystem)
        {
            return $"{operatingSystem.OperatingSystemId, -2}: {operatingSystem.Name,-20} {operatingSystem.ReleaseDate.Date,0:d/M/yyyy} {operatingSystem.Description}";
        } // GetInfo.

        public static string GetInfoWithSoftwareApplications(this OperatingSystem operatingSystem)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(operatingSystem.GetInfo());

            foreach (var softwareApplication in operatingSystem.SoftwareApplications)
            {
                sb.AppendLine($"\t Application: {softwareApplication.GetInfo()}");
            } // Foreach.

            return sb.ToString();

        } // GetInfoWithSoftwareApplications.
    }
}