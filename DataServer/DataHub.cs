using System;
using System.Linq;
using System.Threading.Tasks;
using DiaryBackend;
using Microsoft.AspNetCore.SignalR;

namespace DataServer.Hubs {
    public class DataHub : Hub {
        public Task Login(string username, string password) {
            var ValidationStatus = false;
            using(var context = new BackendContext()) {
                try {
                    var Query = context.Users
                        .Single(b => b.Username.Equals(username));
                    if (Query != null) {
                        ValidationStatus = true;
                    }
                } catch (InvalidOperationException) {
                    Console.WriteLine("Error occured.");
                    ValidationStatus = false;
                }
            }
            return Clients.Caller.SendAsync("ValidationRequest", ValidationStatus);
        }
    }
}