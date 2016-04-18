using Desafio.Model;
using Desafio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.BusinessService
{
    public class TelefoneService : ServiceBase
    {
        public TelefoneService() : base()
        {

        }

        public TelefoneService(DesafioDbContext context) : base(context)
        {

        }

        public Telefone SaveTelefone(Telefone telefoneToSave)
        {
            var telefoneFound = this.UnitOfWork.TelefoneRepository.GetById(telefoneToSave.Id);

            if (telefoneFound != null)
            {
                telefoneFound.Ddd = telefoneToSave.Ddd;
                telefoneFound.Numero = telefoneToSave.Numero;
                telefoneFound.DataCriacao = telefoneToSave.DataCriacao;
                telefoneFound.DataAtualizacao = DateTime.Now;

                this.UnitOfWork.TelefoneRepository.Update(telefoneFound);
            }
            else
            {
                telefoneToSave.DataCriacao = DateTime.Now;
                telefoneToSave.DataAtualizacao = telefoneToSave.DataCriacao;

                this.UnitOfWork.TelefoneRepository.Insert(telefoneToSave);
            }

            this.UnitOfWork.Save();

            return telefoneToSave;
        }

        public void RemoveTelefone(int telefoneId)
        {
            this.UnitOfWork.TelefoneRepository.Delete(telefoneId);

            this.UnitOfWork.Save();
        }

        public IList<Telefone> Find(string telefone = "")
        {
            var result = default(List<Telefone>);

            if (!string.IsNullOrWhiteSpace(telefone))
                result = this.UnitOfWork.TelefoneRepository.Get(t => t.Numero.Contains(telefone)).ToList();
            else
                result = this.UnitOfWork.TelefoneRepository.Get().ToList();

            return result;
        }

        public Telefone GetById(int telefoneId)
        {
            return this.UnitOfWork.TelefoneRepository.GetById(telefoneId);
        }
    }
}
