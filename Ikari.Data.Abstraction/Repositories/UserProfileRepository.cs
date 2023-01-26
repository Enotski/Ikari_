using Ikari.Data.Models.Entities.ShopItems;
using Ikari.Data.Models.Entities.UserProfile;
using Ikari.Data.Models.Enums;
using Ikari.Data.Models.ViewModels.DataGridModels;
using Ikari.Data.Models.ViewModels.UserProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ikari.Data.Abstraction.Repositories {
    /// <summary>
    /// Репозиторий провиля пользователя
    /// </summary>
    public class UserProfileRepository : BaseRepository {
        public UserProfileRepository(IkariDbContext context) : base(context) {
        }
        private Func<User, UserViewModel> UserToViewModel = (item) => {
            return new UserViewModel() {
                Id = item.Id,
                Login = item.Login,
                Email = item.Email,
                //Balance = item.Balance,
                Role = new RoleViewModel { Id = item.RoleId.GetValueOrDefault(), Name = item.Role.Name, Type = RoleType.Admin}
            };
        };
        private Func<User, UserDataGridModel> UserToDataGridModel = (item) => {
            return new UserDataGridModel() {
                Id = item.Id,
                Login = item.Login,
                Email = item.Email,
            };
        };
        public UserViewModel GetUserViewModel(Guid userId) {
            try {
                var user = DbContext.Users.Include(u => u.Role).FirstOrDefault(x => x.Id == userId);
                if (user == null)
                    return new UserViewModel();
                return UserToViewModel(user); 
            } catch (Exception ex) {
                return new UserViewModel();
            }
        }
        public UserViewModel GetUserViewModel(string login, string pass) {
            try {
                var users = DbContext.Users.ToList();
                var user = DbContext.Users.Include(u => u.Role).FirstOrDefault(x => x.Login == login && x.Password == pass);
                if (user == null)
                    return new UserViewModel();
                return UserToViewModel(user);
            } catch (Exception ex) {
                return new UserViewModel();
            }
        }
        public User? GetUserEntity(Guid userId) {
            try {
                return DbContext.Users.FirstOrDefault(x => x.Id == userId);
            } catch (Exception ex) {
                return null;
            }
        }
        public DataGridResponseModel<UserDataGridModel> GetUsersDataGrid(DataGridLoadOptions options) {
            try {
                var resp = new DataGridResponseModel<UserDataGridModel>();

                var items = DbContext.Users.AsQueryable();

                FilterDataGrid(options, ref items);
                var ordered = items.OrderBy(x => 0);
                SortData(options, ref ordered);

                resp.totalCount = ordered.Count();
                resp.data = ordered.Skip(options.Skip).Take(options.Take).Select(UserToDataGridModel);
                return resp;
            } catch (Exception ex) {
                return new DataGridResponseModel<UserDataGridModel>();
            }
        }
        private void FilterDataGrid(DataGridLoadOptions options, ref IQueryable<User> items) {
            try {
                if (options.Filter != null) {
                    if (options.Filter[0].ToString() == "Login") {
                        var search = options.Filter[2].ToString().ToLower().Trim();
                        switch (options.Filter[1]) {
                            case "contains": {
                                    items = items.Where(x => x.Login.ToLower().Contains(search));
                                }
                                break;
                            case "notcontains": {
                                    items = items.Where(x => !x.Login.ToLower().Contains(search));
                                }
                                break;
                            case "startswith": {
                                    items = items.Where(x => x.Login.ToLower().StartsWith(search));
                                }
                                break;
                            case "endswith": {
                                    items = items.Where(x => x.Login.ToLower().EndsWith(search));
                                }
                                break;
                            case "=": {
                                    items = items.Where(x => x.Login == search);
                                }
                                break;
                            case "<>": {
                                    items = items.Where(x => x.Login != search);
                                }
                                break;
                        }
                    } else if (options.Filter[0].ToString() == "Email") {
                        var search = options.Filter[2].ToString().ToLower().Trim();
                        switch (options.Filter[1]) {
                            case "contains": {
                                    items = items.Where(x => x.Email.ToLower().Contains(search));
                                }
                                break;
                            case "notcontains": {
                                    items = items.Where(x => !x.Email.ToLower().Contains(search));
                                }
                                break;
                            case "startswith": {
                                    items = items.Where(x => x.Email.ToLower().StartsWith(search));
                                }
                                break;
                            case "endswith": {
                                    items = items.Where(x => x.Email.ToLower().EndsWith(search));
                                }
                                break;
                            case "=": {
                                    items = items.Where(x => x.Email == search);
                                }
                                break;
                            case "<>": {
                                    items = items.Where(x => x.Email != search);
                                }
                                break;
                        }
                    }
                }
            } catch (Exception ex) {

            }
        }
        private void SortData(DataGridLoadOptions options, ref IOrderedQueryable<User> ordered) {
            try {
                if (options.Sort != null) {
                    foreach (var sort in options.Sort) {
                        if (sort.Selector == "Login") {
                            ordered = sort.Desc ? ordered.ThenByDescending(x => x.Login) : ordered.ThenBy(x => x.Login);
                        } else if (sort.Selector == "Email") {
                            ordered = sort.Desc ? ordered.ThenByDescending(x => x.Email) : ordered.ThenBy(x => x.Email);
                        }
                    }
                }
            } catch (Exception ex) {

            }
        }
        public UserViewModel RegisterNewUser(string name, string email, string password) {
            try {
                var userRole = DbContext.Roles.FirstOrDefault(c => c.Name == "User");
                var newUser = new User() {
                    Id = Guid.NewGuid(),
                    Login = name,
                    Email = email,
                    Password = password,
                    RoleId = userRole.Id
                };
                DbContext.Users.Add(newUser);
                DbContext.SaveChanges();

                return UserToViewModel(newUser);
            } catch(Exception ex) {
                return null;
            }
        }
        public Role GetUserRole(Guid userId) {
            try {
                var currUser = DbContext.Users.Include(x => x.Role).FirstOrDefault(x => x.Id == userId);
                if (currUser == null)
                    return null;

                return currUser.Role;
                //return currUser.Role.RoleType;

            } catch(Exception ex) {
                return null;
            }
        }
    }
}
