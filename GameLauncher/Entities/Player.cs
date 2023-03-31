using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameLauncher.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string HashPassword { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDt { get; set; }
        public int CoinsCount { get; set; }
        public DateTime? DeleteDt { get; set; }

        /// Navigation properties ///
        public List<Content> UnlockedContent { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}
