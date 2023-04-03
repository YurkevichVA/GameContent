using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace GameLauncher.Entities
{
    public class Content
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double CostReal { get; set; }
        public int CostCoins { get; set; }
        public string InstallationLink { get; set; }
        public string InstallationFolder { get; set; }
        public ContentType Type { get; set; }
        public DateTime? DeleteDt { get; set; }
    }
    public enum ContentType
    {
        Item,
        Character
    }
}
