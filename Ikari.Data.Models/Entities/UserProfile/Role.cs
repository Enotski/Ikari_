using Ikari.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Models.Entities.UserProfile
{
    public class Role: IKey
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<User> Users { get; set; }
    }
}
