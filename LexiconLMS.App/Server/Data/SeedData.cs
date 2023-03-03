using Bogus;
using Bogus.DataSets;
using LexiconLMS.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using System.Security.Claims;

namespace LexiconLMS.App.Server.Data
{
    public class SeedData
    {
        private static ApplicationDbContext db = default!;
        private static UserManager<ApplicationUser> userManager = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static Faker faker = null!;

        public static async Task InitAsync(ApplicationDbContext context, IServiceProvider services, string adminPW)
        {
            if (context is null) throw new NullReferenceException(nameof(ApplicationDbContext));
            db = context;

            if (string.IsNullOrEmpty(adminPW))
                throw new Exception("Can't get password from config");

            if (db.Users.Any()) return;  // Seeda ej om det redan finns data på databasen

            ArgumentNullException.ThrowIfNull(nameof(services));
            
            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (roleManager == null) throw new NullReferenceException(nameof(RoleManager<IdentityRole>));

            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            if (userManager == null) throw new NullReferenceException(nameof(UserManager<ApplicationUser>));

            var roleNames = new[] { "Student", "Admin" };
            var adminEmail = "admin@admin.se";
            var adminFirstName = "Admin";
            var adminLastName = "Admin";

            await AddRolesAsync(roleNames);

            var admin = await AddAdminAsync(adminEmail, adminFirstName, adminLastName, adminPW);

            await AddToRolesAsync(admin, roleNames);
          
            faker = new Faker("sv");
            var courses = GenerateCourses(5);
            await db.AddRangeAsync(courses);
            await db.SaveChangesAsync();
        }

        private static async Task AddToRolesAsync(ApplicationUser admin, string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await userManager.IsInRoleAsync(admin, roleName)) continue;
                var result = await userManager.AddToRoleAsync(admin, roleName);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }

        private static async Task<ApplicationUser> AddAdminAsync(string adminEmail, string adminFirstName, string adminLastName, string adminPW)
        {
            var foundUser = await userManager.FindByEmailAsync(adminEmail);

            if (foundUser != null) return null!;

            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = adminFirstName,
                LastName = adminLastName
            };

            var result = await userManager.CreateAsync(admin, adminPW);            

            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return admin;
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;
                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }


        private static IEnumerable<Course> GenerateCourses(int numberOfCourses)
        {            
            var courses = new List<Course>();
            Random random = new Random();

            string[] courseTitles = {".NET", "FrontEnd"};
           
            for (int i = 0; i < numberOfCourses; i++)
            {
                Course course = new Course
                {
                    Title = faker.PickRandom(courseTitles),
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMonths(5),
                    Descritpion = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Lorem.Text()),
                    Modules = GenerateModules(8),
                };

                courses.Add(course);
            }
            return courses;
        }


private static List<Module> GenerateModules(int numberOfModules)
        {
            var modules = new List<Module>();

            string[] moduleTitles = { "C#", "Git", "API", "APL", "FrontEnd", "LMS", "MVC", "Azure" };

            for (int i = 0; i < numberOfModules; i++)
            {
                Module module = new Module
                {
                    Title = moduleTitles[i],
                    StartTime = DateTime.Now.AddMonths(i),
                    EndTime = DateTime.Now.AddMonths(i+1),
                    Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Lorem.Text()),
                    Activities = GenerateActivities(8),
                };

                modules.Add(module);
            }
            return modules;
        }


        private static List<Activity> GenerateActivities(int numberOfActivities)
        {
            var activities = new List<Activity>();
            string[] activityTypes = {"Lecture", "E-learning", "Practice session", "Assignment"};
              
            for (int i = 0; i < numberOfActivities; i++)
            {
                Activity activity = new Activity
                {
                    //Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Lorem.Word())
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(9),
                    Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(faker.Lorem.Text()),
                    ActivityType = new ActivityType
                    {
                        Type = faker.PickRandom(activityTypes)
                    }
                };

                activities.Add(activity);
            }
            return activities;
        }
    }
}
