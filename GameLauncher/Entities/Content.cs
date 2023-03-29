using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Entities
{
    public class Content
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float CostReal { get; set; }
        public int CostCoins { get; set; }
        public string InstallationLink { get; set; }
        public string InstallationFolder { get; set; }
        public string Type { get; set; }
    }
}
