using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Infra.Data.model;
using Backend.Repository.EF;
using Backend.Repository.EF.Interface;
using Backend.Service.EF.Interface;

namespace Backend.Service.EF
{
    public class PessoaService : IPessoaService
    {
        public IPessoaRepository pR;
        public PessoaService(IPessoaRepository pr)
        {
            this.pR = pr;
        }
        public List<Pessoa> GetAll(int page)
        {
            if (page >= 0)
            {
                return pR.GetAll(page);
            }
            else
            {
                return new List<Pessoa>();
            }
        }
        public Pessoa GetId(int id)
        {
            if (id >= 0)
            {
                return pR.GetId(id);
            }
            else
            {
                return new Pessoa();
            }

        }
        public bool Post(Pessoa p)
        {
            if (p.id >= 0 && p.idade < 150 && p.nome.Length < 300 && p.cpf.Length == 11)
            {
                return pR.Post(p);
            }
            //on a later version ill do proper error returning, for now just returning false will do 
            else
            {
                return false;
            }

        }
        public int Put(Pessoa p)
        {
            p.id = 0;
            if (p.idade < 150 && p.nome.Length < 300 && p.cpf.Length == 11)
            {
                return pR.Put(p);
            }
            //on a later version ill do proper error returning, for now just returning false will do 
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
