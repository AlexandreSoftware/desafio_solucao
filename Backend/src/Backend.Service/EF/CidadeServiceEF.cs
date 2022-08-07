using AutoMapper;
using Backend.Infra.Data.model;
using Backend.Repository.EF;
using Backend.Repository.Redis;
using Backend.Service.model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.EF
{
    public interface ICidadeServiceEF
    {
        public IEnumerable<Cidade> GetAll(int page);
        public Cidade GetId(int id);
        public bool Post(Cidade c);
        public int Put(Cidade c);
        public bool Delete(int id);
    }
    public class CidadeServiceEF : ICidadeServiceEF
    {
        public readonly ICidadeRepositoryEF _CR;
        public readonly ICidadeRepositoryRedis _CRR;
        public readonly IMapper _mapper;

        public CidadeServiceEF(ICidadeRepositoryEF cr, IMapper mapper,ICidadeRepositoryRedis crr)
        {
            _CRR = crr;
            _CR = cr;
            _mapper = mapper;
        }
        public IEnumerable<Cidade> GetAll(int page)
        {
            string templateLog = "[Backend.Service] [EFCidadeServiceEF] [GetAll]";
            Log.Information($"{templateLog} Iniciando Servico GetAll, checando se a pagina e um numero maior que 0");
            if (page >= 0)
            {
                Log.Information($"{templateLog} Pagina e um numero maior que 0, buscando todos os registros da pagina");
                var paginaCidadeDto =_CR.GetAll(page);
                Log.Information($"{templateLog} achado todos os registros da pagina, convertendo de DTO para view");
                IEnumerable<Cidade> cidades = paginaCidadeDto.Select(x =>
                {
                    Cidade cidade = _mapper.Map<CidadeDto, Cidade>(x);
                    return cidade;

                });
                Log.Information($"{templateLog} todos os registros da pagina foram convertidos, retornando lista de Pessoa");
                return cidades;
            }
            else
            {
                Log.Error($"{templateLog} Pagina e um numero menor que 0, retornando lista vazia");
                return new List<Cidade>();
            }
        }
        public Cidade GetId(int id)
        {
            string templateLog = "[Backend.Service] [EFCidadeServiceEF] [GetId]";
            Log.Information($"{templateLog} Iniciando Servico GetId, checando se o ID e um numero maior que 0");
            if (id >= 0)
            {
                Log.Information($"{templateLog} ID e um numero maior que 0, buscando registro pelo ID e retornando");

                CidadeDto? mappedElement = _CRR.GetId(id);
                if(mappedElement is null)
                {
                    mappedElement = _CR.GetId(id);
                    _CRR.Set(mappedElement);
                }
                return _mapper.Map<CidadeDto, Cidade>(mappedElement);

            }
            else
            {
                Log.Error($"{templateLog} Id e um numero menor que 0, retornando lista vazia");
                return new Cidade();
            }    
        }
        public bool Post(Cidade c)
        {
            string templateLog = "[Backend.Service] [EFCidadeServiceEF] [Post]";
            Log.Information($"{templateLog} Iniciando Servico Post, checando se o ID e um numero maior que 0, se o tamanho do uf e igual a 2 e o tamanho do nome e maior que 1");
            if (c.Id >= 0 && c.UF.Length == 2 && c.Nome.Length >1)
            {
                Log.Information($"{templateLog} Validacoes passaram, Mapeando para DTO e retornando");

                var mappedCidade = _mapper.Map<Cidade, CidadeDto>(c);
                var passed = _CR.Post(mappedCidade);
                if(passed)
                {
                    _CRR.Set(mappedCidade);
                }
                return passed;

            }
            else
            {
                Log.Error($"{templateLog} Validacoes nao passaram, retornando false");
                return false;
            }
        }
        public int Put(Cidade c)
        {
            string templateLog = "[Backend.Service] [EFCidadeServiceEF] [Put]";
            Log.Information($"{templateLog} Iniciando Servico Put, checando se o tamanho do uf e igual a 2 e o tamanho do nome e maior que 1");
            c.Id = null;
            if (c.UF.Length == 2 && c.Nome.Length > 1)
            {
                Log.Information($"{templateLog} Validacoes passaram, Mapeando para DTO e retornando");
                var mappedCidade = _mapper.Map<Cidade, CidadeDto>(c);
                int idInserted = _CR.Put(mappedCidade);
                mappedCidade.id = idInserted;
                _CRR.Set(mappedCidade);
                return idInserted;
            }
            else
            {
                Log.Error($"{templateLog} Validacoes nao passaram, jogando erro");
                throw new Exception("Erro: nao foi possivel inserir o elemento");
            }    
        }
        public bool Delete(int id)
        {
            string templateLog = "[Backend.Service] [EFCidadeServiceEF] [Delete]";
            Log.Information($"{templateLog} Iniciando Servico Delete, checando se o ID e um numero maior que 0");
            if (id >= 0)
            {
                Log.Information($"{templateLog} ID e um numero maior que 0, deletando registro pelo ID e retornando");
                bool deleted =_CR.Delete(id);
                if (deleted)
                {
                    _CRR.Delete(id);
                }
                return deleted;
            }
            else
            {
                Log.Error($"{templateLog} Validacoes nao passaram, retornando false");
                return false;
            }     
        }
    }
}
