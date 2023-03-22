using Bogus;
using Bogus.DataSets;
using LexiconLMS.App.Server.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
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
        private static List<ActivityType> activityTypes = null!;

        public static async Task InitAsync(ApplicationDbContext context, IServiceProvider services, string password)
        {
            if (context is null) throw new NullReferenceException(nameof(ApplicationDbContext));
            db = context;

            if (string.IsNullOrEmpty(password))
                throw new Exception("Can't get password from config");

            if (db.Users.Any()) return;  // Seeda ej om det redan finns data på databasen

            ArgumentNullException.ThrowIfNull(nameof(services));
            
            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            if (roleManager == null) throw new NullReferenceException(nameof(RoleManager<IdentityRole>));

            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            if (userManager == null) throw new NullReferenceException(nameof(UserManager<ApplicationUser>));

           
            var roleNames = new[] { "Student", "Admin" };
            await AddRolesAsync(roleNames);

            var adminEmail = "admin@admin.se";
            var adminFirstName = "Admin";
            var adminLastName = "Admin";
            var adminAvatar = "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1227.jpg";
            var admin = await AddAdminAsync(adminEmail, adminFirstName, adminLastName, password, adminAvatar);


            var activityTypes = GenerateActivityTypes();
            var courses = GenerateCourses(5);  // Generates courses with associated modules and activities

            foreach (var course in courses)  // Adds students to each course
            {
                var students = GenerateStudents(20);
                await AddStudentsAsync(students, "Student", "password");
                course.Users = students;
            }

            await db.AddRangeAsync(courses);
            await db.SaveChangesAsync();
        }              

        private static List<Course> GenerateCourses(int numberOfCourses)
        {
            string[] courseTitles = { ".NET", "FrontEnd", "Java", "IT support"  };

            var faker = new Faker<Course>()
                .RuleFor(c => c.Title, f => f.PickRandom(courseTitles))
                .RuleFor(c => c.Description, f => f.Lorem.Sentence())
                //.RuleFor(c => c.StartTime, f => DateTime.Now.AddDays(f.Random.Int(-100,100)))
                .RuleFor(c => c.StartTime, f => f.Date.Recent())
                .RuleFor(c => c.EndTime, (f,c) => c.StartTime.AddMonths(11))
                .RuleFor(c => c.Modules, (f,c) => GenerateModules(8, c.StartTime))  
                ;

            return faker.Generate(numberOfCourses);
        }

        private static List<Module> GenerateModules(int numberOfModules, DateTime courseStart)
        {            
            string[] moduleTitles = { "C#", "Git", "API", "APL", "FrontEnd", "Blazor", "MVC", "Azure", "General"};
            int i = 0;
            int j = 0;
           
            var faker = new Faker<Module>()
                .RuleFor(m => m.Title, f => moduleTitles[i++])
                .RuleFor(m => m.Description, f => f.Lorem.Sentence(5))
                .RuleFor(m => m.StartTime, f => courseStart.AddMonths(j++))
                .RuleFor(m => m.EndTime, (f,m) => m.StartTime.AddMonths(1).AddDays(-1))
                .RuleFor(m => m.Activities, (f,m) => GenerateActivities(40, m.StartTime))                  
                ;
           
            return faker.Generate(numberOfModules);
        }

        private static List<Activity> GenerateActivities(int numberOfActivities, DateTime moduleStart)
        {
            int i = 0;
            int j = 0;

            var faker = new Faker<Activity>()

            .RuleFor(a => a.Title, f => f.Company.CompanyName())
            .RuleFor(a => a.Description, f => f.Lorem.Paragraphs(5))
            .RuleFor(a => a.StartTime, f => moduleStart.Date.AddHours(9 + (4 * (i++ % 2))).AddDays((i + (j++ % 2)) / 2)) // 2 activities/day 9-12 and 13-17
            .RuleFor(a => a.EndTime, (f, a) => a.StartTime.AddHours(3 + ((i - 1) % 2)))
            .RuleFor(a => a.ActivityType, f => f.PickRandom(activityTypes));

            return faker.Generate(numberOfActivities);
        }

        public static List<ActivityType> GenerateActivityTypes()
        {
            activityTypes = new List<ActivityType>();
            string[] types = { "Lecture", "E-learning", "Practice session", "Assignment", "Misc" };

            for (int i = 0; i < types.Length; i++)
            {
                ActivityType activityType = new() { Type = types[i] };    
                activityTypes.Add(activityType);
            }

            return activityTypes;
        }

        private static List<ApplicationUser> GenerateStudents(int numberOfStudents)
        {
            var faker = new Faker<ApplicationUser>("sv")
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName, "lexicon.se"))
                .RuleFor(u => u.UserName, (f, u) => u.Email)
                .RuleFor(u => u.EmailConfirmed, f => true)
                .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
                ;

            return faker.Generate(numberOfStudents);
        }


        private static async Task AddStudentsAsync(List<ApplicationUser> students, string role, string password)
        {
            foreach (var student in students)
            {
                var foundStudent = await userManager.FindByEmailAsync(student.Email);
                if (foundStudent != null) return;
                var result = await userManager.CreateAsync(student, password);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
                await userManager.AddToRoleAsync(student, role);
            }
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

        private static async Task<ApplicationUser> AddAdminAsync(string adminEmail, string adminFirstName, string adminLastName, string password, string adminAvatar)
        {
            var foundUser = await userManager.FindByEmailAsync(adminEmail);

            if (foundUser != null) return null!;

            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = adminFirstName,
                LastName = adminLastName,
                Avatar = adminAvatar
            };

            var result = await userManager.CreateAsync(admin, password);
            await userManager.AddToRoleAsync(admin, "Admin");

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
    }
}
