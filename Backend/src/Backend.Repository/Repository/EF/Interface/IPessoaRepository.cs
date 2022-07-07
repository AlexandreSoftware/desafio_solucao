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
        public List<PessoaDto> GetAll(int Page);
        public PessoaDto GetId(int id);
        public bool Post(PessoaDto p);
        public int Put(PessoaDto p);
        public bool Delete(int id);
    }
}
