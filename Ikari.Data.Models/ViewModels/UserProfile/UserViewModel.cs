using Ikari.Data.Models.Entities.ShopItems;
using Ikari.Data.Models.Entities.UserProfile;
using Ikari.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Models.ViewModels.UserProfile {
    public class UserViewModel : IKey {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
