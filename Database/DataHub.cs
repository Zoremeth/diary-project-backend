using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryBackend;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Database.Hubs {
    public interface IClient {
        Task ValidationDone(bool validationSuccessful);
        Task RegistrationDone(bool registrationSuccessful);
        Task EntriesRetrieved(List<Entry> entries);
        Task EntriesSaved(bool saveSuccesful);
    }
    public class DataHub : Hub<IClient> {
        public async Task Login(string username, string password) {
            bool validationStatus;
            using(var context = new DiaryContext()) {
                var user = await context.Users.FirstOrDefaultAsync(b => b.Username == username);
                var userPassword = await context.Users.FirstOrDefaultAsync(b => b.Password == password);
                validationStatus = user != null && userPassword != null;
                if (validationStatus) {
                    user.LoggedIn = true;
                }
            }
            await Clients.Caller.ValidationDone(validationStatus);
        }

        public async Task Register(string username, string password) {
            bool registrationSuccessful;
            using(var context = new DiaryContext()) {
                context.Users.Add(new User {
                    Username = username,
                        Password = password,
                        Entries = null,
                });
                await context.SaveChangesAsync();
                var user = await context.Users.FirstOrDefaultAsync(b => b.Username == username);
                registrationSuccessful = user != null;
            }
            await Clients.Caller.RegistrationDone(registrationSuccessful);
        }

        public async Task GetEntries(string username) {
            using(var context = new DiaryContext()) {
                var user = await context.Users
                    .Include(b => b.Entries)
                    .FirstOrDefaultAsync(b => b.Username == username);

                if (!user.LoggedIn)
                    await Clients.Caller.EntriesRetrieved(user.Entries);
            }
        }

        public async Task SaveEntries(int entryId, string content) {
            bool saveSuccesful;
            using(var context = new DiaryContext()) {
                var entry = await context.Entries.FirstOrDefaultAsync(b => b.Id == entryId);
                entry.Content = content;
                await context.SaveChangesAsync();
                saveSuccesful = entry != null;
            }
            await Clients.Caller.EntriesSaved(saveSuccesful);
        }
    }
}