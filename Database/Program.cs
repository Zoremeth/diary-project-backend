using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiaryBackend;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Database {
    public class Program {
        public static void Main(string[] args) {
            CreateWebHostBuilder(args).Build().Run();
            using(var context = new BackendContext()) {
                List<Entry> test = new List<Entry>();
                test.Add(new Entry { Id = 1, Title = "test", Date = "test", Content = "test", Deleted = false, Locked = "test" });
                var testuser = new User { Id = 1, Username = "123", Password = "123", test };
                context.Users.Add(testuser);
                context.SaveChanges();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}