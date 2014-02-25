namespace Altairis.Rap.Migrations {
    using Altairis.Rap.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Altairis.Rap.Data.RapDbContext> {
        private const string INITIAL_PASSWORD = "password";

        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Altairis.Rap.Data.RapDbContext context) {
            if (!context.Users.Any()) {
                byte[] passwordHash, passwordSalt;
                using (var hmac = new System.Security.Cryptography.HMACSHA512()) {
                    passwordSalt = hmac.Key;
                    passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(INITIAL_PASSWORD));
                }
                context.Users.Add(new User {
                    UserName = "administrator",
                    Email = "info@altairis.cz",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    IsApproved = true,
                    DateCreated = DateTime.Now,
                    DateLastPasswordChange = DateTime.Now,
                    Comment = "Správce systému",
                });
                context.SaveChanges();
            }
        }
    }
}
