using System;

namespace SoftwareApplicationManager.UI.MVC.Models
{
    public class MainDetailModel
    {
        // Properties.
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        
        // Constructor.
        public MainDetailModel(string name, string description, DateTime date, string imageUrl)
        {
            Name = name;
            Description = description;
            Date = date;
            ImageUrl = imageUrl;
        } // MainDetailModel.
    }
}