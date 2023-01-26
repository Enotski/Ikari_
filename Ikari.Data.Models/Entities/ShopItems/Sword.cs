using Ikari.Data.Models.Entities.UserProfile;
using Ikari.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Models.Entities.ShopItems {
    /// <summary>
    /// Меч
    /// </summary>
    public class Sword : Weapon {
        public List<User> Users { get; set; }
        public SwordType Type { get; set; }
        public double Damage { get; set; }
    }
}
