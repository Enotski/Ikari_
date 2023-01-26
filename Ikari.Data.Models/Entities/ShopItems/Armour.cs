using Ikari.Data.Models.Entities.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Models.Entities.ShopItems {

    /// <summary>
    /// Броня
    /// </summary>
    public class Armour : Clothes {
        public List<User> Users { get; set; }
    }
}
