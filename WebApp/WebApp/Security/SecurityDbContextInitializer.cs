using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region Additional Namespaces
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;
using System.Data.Entity;
using WebApp.Models;
using ChinookSystem.BLL;
#endregion

namespace WebApp.Security
{
    public class SecurityDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            #region Seed the roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');
            foreach (var role in startupRoles)
                roleManager.Create(new IdentityRole { Name = role });

            //take roles from your database suck as a positions table or off some other data record
            //we have a Title colum on the Employees table which hold the rules
            EmployeeController sysmgr = new EmployeeController();
            List<string> employeeroles = sysmgr.Employees_GetTitles();
            foreach (var role in employeeroles)
                roleManager.Create(new IdentityRole { Name = role });


            #endregion

            #region Seed the users
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];
            string adminRole = ConfigurationManager.AppSettings["adminRole"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var result = userManager.Create(new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail
            }, adminPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(adminUser).Id, adminRole);

            //example of hard coding a new user

            string userPassword = ConfigurationManager.AppSettings["newUserPassword"];
          
            result = userManager.Create(new ApplicationUser
            {
                UserName="HansendB",
                Email = "HansenB@hotmail.somewhere.ca",
                CustomerId = 4
            }, userPassword);
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName("HansendB").Id,"Customers");

            

            //seeding employees from the employee table
            //todo:
            //retrieve a list of Employee from the database
            //foreach employee
            //UserName such as LastName and FirstInitial possible add a number
            //Email of employee or null or add @Chinook.somewhere.ca to userName
            //Employee ID is the pKey of he Employee record
            //use the appSetting newUserPassword for the password
            //Succeeded, role can come from the Employee record
            
            #endregion

            // ... etc. ...

            base.Seed(context);
        }
    }
}