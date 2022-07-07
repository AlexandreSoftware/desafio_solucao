using Backend.Infra.Data.model;
using Backend.Service.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.EF.Interface
{
    public interface IPessoaService
    {
        public IEnumerable<Pessoa> GetAll(int page);
        public Pessoa GetId(int id);
        public bool Post(Pessoa p);
        public int Put(Pessoa p);
        public bool Delete(int id);
    }
}
