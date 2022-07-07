using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Bogus;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository.EF;
using Backend.Infra.Data.model;
using Bogus.Extensions.Brazil;
using Backend.Infra.Data.Context;

namespace Backend.Repository.Context
{
    public class PessoaSeeder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static void Seed(PessoasContext pc)
        {
            PessoasContext PC = pc;
            string templatelog = "[Backend.Repository] [Seeder] [Seed]";
            Log.Information($"{templatelog} Iniciando Seeding, instanciando o Repositorio");
            PessoaRepository PR = new PessoaRepository(PC);
            CidadeRepository CR = new CidadeRepository(PC);
            Log.Information($"{templatelog} Gerando Pessoas");
            var pessoas = GerarPessoas();
            Log.Information($"{templatelog} Gerando Cidades");
            var cidades = GerarCidades();
            Log.Information($"{templatelog} Connectando Cidades e Pessoas");
            int[] RandomCidadeIds = Shuffle40Random();
            Log.Information($"{templatelog} Inserindo Cidades e Pessoas");
            for (int i = 0; i < pessoas.Length; i++)
            {
                CR.Put(cidades[i]);
            }
            for (int i = 0; i < 40; i++)
            {
                pessoas[i].id_cidade = RandomCidadeIds[i];
            }
            //isso garante que sempre vai ter uma cidade para uma pessoa
            for (int i = 0; i < pessoas.Length; i++)
            {
                pessoas[i].cpf =pessoas[i].cpf.Replace(".", string.Empty).Replace("-", string.Empty);
                PR.Put(pessoas[i]);
            }
            Log.Information($"{templatelog} Terminado de inserir cidades e pessoas");
        }
        public static int[] Shuffle40Random()
        {
            int[] result = new int[40];
            Random r = new Random();
            for (int i = 0; i < 40; i++)
            {
                result[i] = r.Next(1, 40);
            }
            return result;
        }
        public static PessoaDto[] GerarPessoas()
        {
            string templatelog = "[Backend.Repository] [Seeder] [GerarPessoas]";
            Log.Information($"{templatelog} Criando Gerador de Pessoas com o faker");
            var fakerjob = new Faker<PessoaDto>("pt_BR")
                .RuleFor(property: x => x.nome, setter: c => c.Name.FirstName())
                .RuleFor(property: x => x.cpf, setter: c => c.Person.Cpf())
                .RuleFor(property: x => x.idade, setter: c => c.Random.Int(0, 100));
            Log.Information($"{templatelog} Gerando Pessoas");
            var res = fakerjob.Generate(40).ToArray();
            Log.Information($"{templatelog} Pessoas Geradas, retornando");
            return res;
        }

        public static CidadeDto[] GerarCidades()
        {
            string templatelog = "[Backend.Repository] [Seeder] [GerarCidades]";
            Log.Information($"{templatelog} Criando Gerador de Cidades com o faker");
            var fakerCidade = new Faker<CidadeDto>()
                .RuleFor(property: x => x.uF, setter: y => y.Random.String2(length:2,chars : "abcdefghijklmnopqrstuvwxyz"))
                .RuleFor(property: x => x.nome, setter: y => y.Address.City());
            Log.Information($"{templatelog} Gerando Cidades");
            var res = fakerCidade.Generate(40).ToArray();
            Log.Information($"{templatelog} Cidades Geradas, retornando");
            return res;
        }
    }
}
