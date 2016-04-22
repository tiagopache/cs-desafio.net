using Desafio.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class UnitOfWork : IDisposable
    {
        private DesafioDbContext _context;

        public UnitOfWork(DesafioDbContext context)
        {
            this._context = context;
        }


        #region Singleton Pattern - UsuarioRepository
        private GenericRepository<Usuario> _usuarioRepository;
        public GenericRepository<Usuario> UsuarioRepository
        {
            get
            {
                if (_usuarioRepository == null)
                    _usuarioRepository = new GenericRepository<Usuario>(this._context);

                return _usuarioRepository;
            }
            set
            {
                _usuarioRepository = value;
            }
        }
        #endregion


        #region Singleton Pattern - TelefoneRepository
        private GenericRepository<Telefone> _telefoneRepository;
        public GenericRepository<Telefone> TelefoneRepository
        {
            get
            {
                if (_telefoneRepository == null)
                    _telefoneRepository = new GenericRepository<Telefone>(this._context);

                return _telefoneRepository;
            }
            set
            {
                _telefoneRepository = value;
            }
        }
        #endregion

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbValidationError validationError in ex.EntityValidationErrors.SelectMany(ev => ev.ValidationErrors))
                {
                    Trace.TraceInformation("Property: {0} - Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }

                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


    }
}
