using Desafio.Repository.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository.Initialize
{
    public class Migrator
    {
        public static void RunMigrations()
        {
            var migrationConfigurations = new Configuration();
            var dbMigrator = new DbMigrator(migrationConfigurations);

            dbMigrator.Update();
        }
    }
}
