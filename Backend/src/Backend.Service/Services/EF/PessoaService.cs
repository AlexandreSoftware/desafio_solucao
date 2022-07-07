using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Infra.Data.model;
using Backend.Repository.EF;
using Backend.Repository.EF.Interface;
using Backend.Service.EF.Interface;
using Backend.Service.model;
using Serilog;

namespace Backend.Service.EF
{
    public class PessoaService : IPessoaService
    {
        public readonly IPessoaRepository _PR;
        public readonly ICidadeRepository _CR;
        public readonly IMapper _mapper;
        public PessoaService(IPessoaRepository pr,ICidadeRepository cr, IMapper mapper)
        {
            _CR = cr;
            _mapper = mapper;
            _PR = pr;
        }
        public IEnumerable<Pessoa> GetAll(int page)
        {
            string templateLog = "[Backend.Service] [EFPessoaService] [GetAll]";
            Log.Information($"{templateLog} Iniciando Servico GetAll, checando se a pagina e um numero maior que 0");
            if (page >= 0)
            {
                Log.Information($"{templateLog} Pagina e um numero maior que 0, buscando todos os registros da pagina");
                var paginaPessoasDto = _PR.GetAll(page);
                Log.Information($"{templateLog} achado todos os registros da pagina, convertendo de DTO para view");
                IEnumerable<Pessoa> pessoas = paginaPessoasDto.Select(x =>
                {
                    Pessoa pessoa = _mapper.Map<PessoaDto, Pessoa>(x);
                    pessoa.Cidade = _mapper.Map <CidadeDto,Cidade>(_CR.GetId(x.id_cidade));
                    return pessoa;
                });
                Log.Information($"{templateLog} todos os registros da pagina foram convertidos, retornando lista de Pessoa");
                return pessoas;
            }
            else
            {
                Log.Error($"{templateLog} Pagina e um numero menor que 0, retornando lista vazia");
                return new List<Pessoa>();
            }
        }
        public Pessoa GetId(int id)
        {
            string templateLog = "[Backend.Service] [EFPessoaService] [GetId]";
            Log.Information($"{templateLog} Iniciando Servico GetId, checando se o ID e um numero maior que 0");
            if (id >= 0)
            {
                Log.Information($"{templateLog} ID e um numero maior que 0, buscando registro pelo ID");
                var pessoaDto = _PR.GetId(id);
                Log.Information($"{templateLog} achado registro pelo ID, convertendo de DTO para view");
                var pessoa = _mapper.Map<PessoaDto, Pessoa>(pessoaDto);
                pessoa.Cidade = _mapper.Map<CidadeDto, Cidade>(_CR.GetId(pessoaDto.id_cidade));
                Log.Information($"{templateLog} registro convertido, retornando Pessoa");
                return pessoa;
            }
            else
            {
                Log.Error($"{templateLog} Id e um numero menor que 0, retornando lista vazia");
                return new Pessoa();
            }

        }
        public bool Post(Pessoa p)
        {
            string templateLog = "[Backend.Service] [EFPessoaService] [Post]";
            Log.Information($"{templateLog} Iniciando Servico Post, checando se o ID e um numero maior que 0, se a idade e menor que 150, se o nome tem menos que 350 caracteres, e o cpf tem o tamanho correto");
            if (p.Id >= 0 && p.Idade < 150 && p.Nome.Length < 300 && p.Cpf.Length == 11)
            {
                Log.Information($"{templateLog} Validacoes passaram, Mapeando para DTO");
                var pessoaDto = _mapper.Map<Pessoa, PessoaDto>(p);
                pessoaDto.id_cidade = (int)p.Cidade.Id;
                Log.Information($"{templateLog} Mapeado para DTO, Editando registro");
                return _PR.Post(pessoaDto);
            }
            else
            {
                Log.Error($"{templateLog} Validacoes nao passaram, retornando false");
                return false;
            }

        }
        public int Put(Pessoa p)
        {
            string templateLog = "[Backend.Service] [EFPessoaService] [Put]";
            Log.Information($"{templateLog} Iniciando Servico Put, checando se a idade e menor que 150, se o nome tem menos que 350 caracteres, e o cpf tem o tamanho correto");
            p.Id = null;
            if (p.Idade < 150 && p.Nome.Length < 300 && p.Cpf.Length == 11)
            {
                Log.Information($"{templateLog} Validacoes passaram, Mapeando para DTO");
                var pessoaDto = _mapper.Map<Pessoa, PessoaDto>(p);
                pessoaDto.id_cidade = (int)p.Cidade.Id;
                Log.Information($"{templateLog} Mapeado para DTO, inserindo registro");
                return _PR.Put(pessoaDto);
            }
            else
            {
                Log.Error($"{templateLog} Validacoes nao passaram, Jogando erro");
                throw new Exception("Erro: nao foi possivel inserir o elemento");
            }

        }
        public bool Delete(int id)
        {
            string templateLog = "[Backend.Service] [EFPessoaService] [Delete]";
            Log.Information($"{templateLog} Iniciando Servico Delete, checando se o ID e um numero maior que 0");
            if (id >= 0)
            {
                Log.Information($"{templateLog} ID e um numero maior que 0, deletando registro ");
                return _PR.Delete(id);
            }
            else
            {
                Log.Error($"{templateLog} Validacoes nao passaram, retornando false");
                return false;
            }

        }
    }
}
