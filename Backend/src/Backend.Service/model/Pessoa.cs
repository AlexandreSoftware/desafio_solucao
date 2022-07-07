using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service.model
{
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        [StringLength(300)]
        public string Nome { get; set; }
        [StringLength(11)]
        public string Cpf { get; set; }
        public int Idade { get; set; }
        public Cidade Cidade { get; set; }
    }
}
