using Ikari.Data.Models.Entities.ShopItems;
using Ikari.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Models.Entities.UserProfile {
    /// <summary>
    /// Его превосходительство юзер
    /// </summary>
    public class User : IKey {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }
        //public decimal Balance { get; set; }
        public List<Armour> Armours { get; set; }
        public List<Sword> Swords { get; set; }
    }
}
