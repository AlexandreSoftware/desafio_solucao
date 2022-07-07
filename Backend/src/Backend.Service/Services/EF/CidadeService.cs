using Backend.Infra.Data.model;
using Backend.Repository.EF.Interface;
using Backend.Service.EF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.EF
{
    public class CidadeService : ICidadeService
    {
        public ICidadeRepository pR;
        public CidadeService(ICidadeRepository pr)
        {
            this.pR = pr;
        }
        public List<Cidade> GetAll(int page)
        {
            if (page >= 0)
            {
                return pR.GetAll(page);
            }
            else
            {
                return new List<Cidade>();
            }
        }
        public Cidade GetId(int id)
        { 
            if (id >= 0)
            {
                return pR.GetId(id);
            }
            else
            {
                return new Cidade();
            }    
        }
        public bool Post(Cidade p)
        {
            if (p.id >= 0 && p.UF.Length == 2 && p.nome.Length >1)
            {
                return pR.Post(p);
            }
            //on a later version ill do proper error returning, for now just returning false will do 
            else
            {
                return false;
            }
        }
        public int Put(Cidade p)
        {
            p.id = 0;
            if ( p.id >= 0 && p.UF.Length == 2 && p.nome.Length > 1)
            {
                return pR.Put(p);
            }
            else
            {
                throw new Exception("Erro: nao foi possivel inserir o elemento");
            }    
        }
        public bool Delete(int id)
        {
            if (id >= 0)
            {
                return pR.Delete(id);
            }
            else
            {
                return false;
            }     
        }
    }
}
