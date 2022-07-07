using Backend.Infra.Data.model;
using Backend.Service.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.EF.Interface
{
    public interface ICidadeService
    {
        public IEnumerable<Cidade> GetAll(int page);
        public Cidade GetId(int id);
        public bool Post(Cidade c);
        public int Put(Cidade c);
        public bool Delete(int id);
    }
}
