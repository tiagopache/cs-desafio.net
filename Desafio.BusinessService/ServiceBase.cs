using Desafio.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.BusinessService
{
    public abstract class ServiceBase
    {
        internal DesafioDbContext _context;

        public ServiceBase()
        {
            this._context = new DesafioDbContext();
        }

        public ServiceBase(DesafioDbContext context)
        {
            this._context = context;
        }
        
        #region Singleton Pattern - UnitOfWork
        private UnitOfWork _unitOfWork;
        public UnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = new UnitOfWork(this._context);

                return _unitOfWork;
            }
            set
            {
                _unitOfWork = value;
            }
        }
        #endregion

        public void ServiceInitialize()
        {
            //Database.SetInitializer(new DataSeedingInitializer());
            //Migrator.RunMigrations();

            this._context.Database.Initialize(force: true);
        }
    }
}
