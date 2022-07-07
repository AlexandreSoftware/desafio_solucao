using Backend.Infra.Data.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.EF.Interface
{
    public interface IPessoaRepository
    {
        public List<Pessoa> GetAll(int Page);
        public Pessoa GetId(int id);
        public bool Post(Pessoa p);
        public int Put(Pessoa p);
        public bool Delete(int id);
    }
}
