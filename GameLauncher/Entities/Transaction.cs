using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        
        /// Navigation properties ///
        public Player Payer { get; set; }
        public List<Content> Contents { get; set; }
    }
}
