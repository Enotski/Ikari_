using Ikari.Data.Models.Enums;
using Ikari.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Models.ViewModels.UserProfile {
    public class RoleViewModel : IKey {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RoleType Type { get; set; }
    }
}
