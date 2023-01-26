using Ikari.Data.Models.Entities.ShopItems;
using Ikari.Data.Models.Entities.UserProfile;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Abstraction.Repositories
{
    /// <summary>
    /// Базовый репозиторий
    /// </summary>
    public class BaseRepository {
        protected IkariDbContext DbContext { get; set; }

        public BaseRepository(IkariDbContext context) {
            DbContext = context;
        }
    }
}
