using Desafio.Model.Contexts;
using Desafio.Model.Migrations;
using System.Data.Entity;

namespace Desafio.Model.Initialization
{
    public class DataSeedingInitializer : MigrateDatabaseToLatestVersion<DesafioDbContext, DesafioConfiguration> { }
}
