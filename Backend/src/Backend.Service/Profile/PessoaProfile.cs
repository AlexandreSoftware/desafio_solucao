using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Infra.Data.model;
using Backend.Service.model;

namespace Backend.Service.Profile
{
    public class PessoaProfile :AutoMapper.Profile
    {
        public PessoaProfile()
        {
            CreateMap<Pessoa, PessoaDto>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.cpf))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.nome))
                .ForMember(dest => dest.Idade, opt => opt.MapFrom(src => src.idade));
        }

    }
}
