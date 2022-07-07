using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Infra.Data.model
{
    public class PessoaDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? id { get; set; }
        [StringLength(300)]
        public string nome { get; set; }
        [StringLength(11)]
        public string cpf { get; set; }
        public int idade { get; set; }
        [ForeignKey("CidadeDTO")]
        public int id_cidade { get; set; }
    }
}
