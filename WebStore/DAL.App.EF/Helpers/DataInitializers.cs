using System;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public static class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }
        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }
        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roleNames = new String[] {"Admin", "Customer", "Publisher"};
            foreach (var roleName in roleNames)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole();
                    role.Name = roleName;
                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role not created!");
                    }
                }
            }
            
            
            var admin = new AppUser
            {
                Email = "admin@mail.ru", 
                UserName = "admin@mail.ru", 
                FirstName = "Nikita", 
                LastName = "Admin"
            };
            
            var publisher = new AppUser {
                Email = "publisher@mail.ru", 
                UserName = "publisher@mail.ru",
                FirstName = "Nikita",
                LastName = "Publisher"
            };
            
            var customer = new AppUser {
                Email = "customer@mail.com", 
                UserName = "customer@mail.com",
                FirstName = "Nikita",
                LastName = "Customer"
            };

            var users = new AppUser[]
            {
                admin,
                publisher,
                customer
            };
            
            var passwords = new string[]
            {
                "Qwertyui123!", "Qwertyui123!", "Qwertyui123!"
            };
            
            for (int i = 0; i < users.Length; i++)
            {
                var user = userManager.FindByNameAsync(users[i].UserName).Result;
                if (user == null)
                {
                    var result = userManager.CreateAsync(users[i], passwords[i]).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }
            }
            admin = userManager.FindByNameAsync(admin.UserName).Result;
            publisher = userManager.FindByNameAsync(publisher.UserName).Result;
            customer = userManager.FindByNameAsync(customer.UserName).Result;


            var roleResult = userManager.AddToRoleAsync(admin, "Admin").Result;
            roleResult = userManager.AddToRoleAsync(admin, "Publisher").Result;
            roleResult = userManager.AddToRoleAsync(admin, "Customer").Result;
            roleResult = userManager.AddToRoleAsync(publisher, "Publisher").Result;
            roleResult = userManager.AddToRoleAsync(publisher, "Customer").Result;
            roleResult = userManager.AddToRoleAsync(customer, "Customer").Result;

            /*
            var user = userManager.FindByNameAsync(userName).Result;
            if (user == null)
            {
                user = new AppUser
                {
                    Email = userName, 
                    UserName = userName, 
                    FirstName = firstName, 
                    LastName = lastName
                };

                var result = userManager.CreateAsync(user, passWord).Result;
                if(!result.Succeeded) throw new ApplicationException("User not created!");
            }
        

            var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
            roleResult = userManager.AddToRoleAsync(user, "Customer").Result;
        */
        }
        public static void SeedData(AppDbContext context)
        {
        }
        
    }

}

