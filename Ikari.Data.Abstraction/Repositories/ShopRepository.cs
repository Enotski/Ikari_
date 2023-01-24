using Ikari.Data.Models.Entities.ShopItems;
using Ikari.Data.Models.ViewModels.DataGridModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Abstraction.Repositories {
    public class ShopRepository : BaseRepository {
        public ShopRepository(IkariDbContext context) : base(context) {
        }
        private Func<Sword, SwordDataGridModel> SwordToDataGridModel = (item) => {
            return new SwordDataGridModel() {
                Id = item.Id,
                Name = item.Name,
                Type = item.Type.ToString(),
                Damage = item.Damage
            };
        };
        private Func<Armour, ArmourDataGridModel> ArmourToDataGridModel = (item) => {
            return new ArmourDataGridModel() {
                Id = item.Id,
                Name = item.Name,
            };
        };
        public DataGridResponseModel<SwordDataGridModel> GetSwords(DataGridLoadOptions options) {
            try {
                var resp = new DataGridResponseModel<SwordDataGridModel>();

                var items = DbContext.Swords.AsQueryable();

                FilterDataGrid(options, ref items);
                var ordered = items.OrderBy(x => 0);
                SortData(options, ref ordered);

                resp.totalCount = ordered.Count();
                resp.data = ordered.Skip(options.Skip).Take(options.Take).Select(SwordToDataGridModel);
                return resp;
            } catch (Exception ex) {
                return new DataGridResponseModel<SwordDataGridModel>();
            }   
        }
        private void FilterDataGrid(DataGridLoadOptions options, ref IQueryable<Sword> items) {
            try {
                if (options.Filter != null) {
                    if (options.Filter[0].ToString() == "Name") {
                        var search = options.Filter[2].ToString().ToLower().Trim();
                        switch (options.Filter[1]) {
                            case "contains": {
                                    items = items.Where(x => x.Name.ToLower().Contains(search));
                                }
                                break;
                            case "notcontains": {
                                    items = items.Where(x => !x.Name.ToLower().Contains(search));
                                }
                                break;
                            case "startswith": {
                                    items = items.Where(x => x.Name.ToLower().StartsWith(search));
                                }
                                break;
                            case "endswith": {
                                    items = items.Where(x => x.Name.ToLower().EndsWith(search));
                                }
                                break;
                            case "=": {
                                    items = items.Where(x => x.Name == search);
                                }
                                break;
                            case "<>": {
                                    items = items.Where(x => x.Name != search);
                                }
                                break;
                        }
                    } else if (options.Filter[0].ToString() == "Damage") {
                        var search = Convert.ToDouble(options.Filter[2]);
                        switch (options.Filter[1]) {
                            case "=": {
                                    items = items.Where(x => x.Damage == search);
                                }
                                break;
                            case "<>": {
                                    items = items.Where(x => x.Damage != search);
                                }
                                break;
                            case "<": {
                                    items = items.Where(x => x.Damage < search);
                                }
                                break;
                            case ">": {
                                    items = items.Where(x => x.Damage > search);
                                }
                                break;
                            case "<=": {
                                    items = items.Where(x => x.Damage <= search);
                                }
                                break;
                            case ">=": {
                                    items = items.Where(x => x.Damage >= search);
                                }
                                break;
                            case "between": {
                                    var search_1 = Convert.ToDouble(options.Filter[3]);
                                    items = items.Where(x => x.Damage > search && x.Damage < search_1);
                                }
                                break;
                        }
                    }
                }
            } catch (Exception ex) {

            }
        }
        private void SortData(DataGridLoadOptions options, ref IOrderedQueryable<Sword> ordered) {
            try {
                if (options.Sort != null) {
                    foreach (var sort in options.Sort) {
                        if (sort.Selector == "Name") {
                            ordered = sort.Desc ? ordered.ThenByDescending(x => x.Name) : ordered.ThenBy(x => x.Name);
                        } else if (sort.Selector == "Damage") {
                            ordered = sort.Desc ? ordered.ThenByDescending(x => x.Damage) : ordered.ThenBy(x => x.Damage);
                        }
                    }
                }
            } catch(Exception ex) {

            }
        }

        public bool BuyItem(Guid itemId, Guid userId, string itemType) {
            try {
                var user = DbContext.Users.FirstOrDefault(x => x.Id == userId);
                if (user == null) { return false; }

                if (itemType == "sword") {
                    var item = DbContext.Swords.FirstOrDefault(x => x.Id == itemId);
                    if (item == null) return false;

                    user.Swords.Add(item);
                } else if (itemType == "armour") {
                    var item = DbContext.Armours.FirstOrDefault(x => x.Id == itemId);
                    if (item == null) return false;

                    user.Armours.Add(item);
                }
                DbContext.SaveChanges();

                return true;
            } catch (Exception ex) {
                return false;
            }
        }
    }
}
