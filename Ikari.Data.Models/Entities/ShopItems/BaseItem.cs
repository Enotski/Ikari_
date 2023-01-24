using Ikari.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Models.Entities.ShopItems
{
    public abstract class BaseItem : IKey
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public double Price { get; set; }
    }
}
