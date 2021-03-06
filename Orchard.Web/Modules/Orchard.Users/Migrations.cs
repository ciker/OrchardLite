﻿using Orchard.Data.Migration;

namespace Orchard.Users
{
    public class UsersDataMigration : DataMigrationImpl
    {

        public int Create()
        {
            SchemaBuilder.CreateTable("UserRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("UserName")
                    .Column<string>("Email")
                    .Column<string>("NormalizedUserName")
                    .Column<string>("Password")
                    .Column<string>("PasswordFormat")
                    .Column<string>("HashAlgorithm")
                    .Column<string>("PasswordSalt")
                    .Column<string>("RegistrationStatus", c => c.WithDefault("Approved"))
                    .Column<string>("EmailStatus", c => c.WithDefault("Approved"))
                    .Column<string>("EmailChallengeToken")
                );

            return 1;
        }


    }
}