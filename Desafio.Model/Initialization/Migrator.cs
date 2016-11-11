using Desafio.Model.Migrations;
using System.Data.Entity.Migrations;

namespace Desafio.Model.Initialization
{
    public class Migrator
    {
        public static void RunMigrations()
        {
            var migrationConfigurations = new DesafioConfiguration();
            var dbMigrator = new DbMigrator(migrationConfigurations);

            dbMigrator.Update();
        }
    }
}
