using System;
using SoftwareApplicationManager.BL.Domain;

namespace SoftwareApplicationManager.UI.MVC.Models
{
    public class SoftwareApplicationDetailModel
    {
        // Properties.
        public SoftwareApplication SoftwareApplication { get; set; }
        public bool IncludeDetailsLink { get; set; }
        
        // Constructor.
        public SoftwareApplicationDetailModel(SoftwareApplication softwareApplication, bool includeDetailsLink)
        {
            SoftwareApplication = softwareApplication;
            IncludeDetailsLink = includeDetailsLink;
        } // SoftwareApplicationDetailModel.
    }
}