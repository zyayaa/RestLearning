namespace RestLearning.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    internal sealed class Configuration : DbMigrationsConfiguration<RestLearning.Data.UsersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UsersContext context)
        {
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'");
            context.Database.ExecuteSqlCommand("sp_MSForEachTable 'IF OBJECT_ID(''?'') NOT IN (ISNULL(OBJECT_ID(''[dbo].[__MigrationHistory]''),0)) DELETE FROM ?'");
            context.Database.ExecuteSqlCommand("EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'");

            context.Users.Add(new Users {
                UserID = Guid.Parse("750EFA66-87CD-468E-A02B-065BB0612A04"),
                Name = "Joao",
                Age = 21
            });

            context.Users.Add(new Users {
                UserID = Guid.Parse("CA0987CD-D8FF-4398-8662-1CE251DE3ED0"),
                Name = "Filipe",
                Age = 22
            });
        }
    }
}
