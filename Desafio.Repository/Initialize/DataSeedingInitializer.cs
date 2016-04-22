using Desafio.Repository.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository.Initialize
{
    public class DataSeedingInitializer : MigrateDatabaseToLatestVersion<DesafioDbContext, Configuration>
    {
    }
}
