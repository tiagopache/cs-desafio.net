using Desafio.Infrastructure.Data.Contexts;
using Desafio.Infrastructure.Logging;
using Desafio.Model.Entities;
using Desafio.Model.Initialization;
using System.Data.Entity;
using System.Diagnostics;

namespace Desafio.Model.Contexts
{
    public class DesafioDbContext : DbContext, IDbContext
    {
        public IDbSet<Usuario> Devices { get; set; }

        public IDbSet<Telefone> Partners { get; set; }

        private static int _instanceCounter = 0;
        private static int _instanceSequence = 0;

        private int instanceId { get; set; }

        public DesafioDbContext() : base("DesafioDbContext")
        {
            this.basicContextConfiguration();
        }

        public DesafioDbContext(string connStr = "DesafioDbContext") : base(connStr)
        {
            this.basicContextConfiguration();
        }

        public void Initialize(IDatabaseInitializer<DbContext> databaseInitializer = null)
        {
            this.log($"DbContext Instance {this.instanceId} - Initializing Database");
            Database.SetInitializer(databaseInitializer == null ? new DataSeedingInitializer() : databaseInitializer as IDatabaseInitializer<DesafioDbContext>);

            Migrator.RunMigrations();

            this.Database.Initialize(force: true);
            this.log($"DbContext Instance {this.instanceId} - Initialized Database");
        }

        private void basicContextConfiguration()
        {
            _instanceCounter++;
            this.instanceId = _instanceSequence++;

            UnityEventLogger.Log.CreateUnityMessage($"Creating DbContext {this.instanceId} - Instance count: {_instanceCounter}");

            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            this.logSql();
        }

        [Conditional("DEBUG")]
        private void logSql() => this.Database.Log = s => Debug.WriteLine($"DbContext Instance {this.instanceId} - Instance count: {_instanceCounter} \n {s}");

        private void log(string log)
        {
            UnityEventLogger.Log.LogUnityMessage(log);
        }

        protected override void Dispose(bool disposing)
        {
            this.log($"Entered Dispose DbContext {this.instanceId}");

            base.Dispose(disposing);

            _instanceCounter--;
            UnityEventLogger.Log.DisposeUnityMessage($"Disposed DbContext {this.instanceId} - Instance count: {_instanceCounter}");
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
