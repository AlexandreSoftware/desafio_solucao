using Backend.Infra.Data.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infra.Data.Context
{
    public class PessoasContext:DbContext
    {
        public PessoasContext(DbContextOptions<PessoasContext> options)
        : base(options)
        {

        }
        public virtual DbSet<Pessoa> Pessoas { get; set; }
        public virtual DbSet<Cidade> Cidades { get; set; }
    }
}
