using Backend.Infra.Data.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.EF.Interface
{
    public interface ICidadeService
    {
        public List<Cidade> GetAll(int page);
        public Cidade GetId(int id);
        public bool Post(Cidade c);
        public int Put(Cidade c);
        public bool Delete(int id);
    }
}
