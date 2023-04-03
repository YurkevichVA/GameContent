using System;
using System.Collections.Generic;

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
