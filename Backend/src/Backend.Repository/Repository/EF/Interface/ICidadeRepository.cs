using Backend.Infra.Data.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Repository.EF.Interface
{
    public interface ICidadeRepository
    {
        public List<CidadeDto> GetAll(int Page);
        public CidadeDto GetId(int id);
        public bool Post(CidadeDto c);
        public int Put(CidadeDto c);
        public bool Delete(int id);
    }
}
