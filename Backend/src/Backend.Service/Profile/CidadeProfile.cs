using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Backend.Infra.Data.model;
using Backend.Service.model;

namespace Backend.Service.Profile
{
    public class CidadeProfile : AutoMapper.Profile
    {
        public CidadeProfile()
        {
            CreateMap<Cidade, CidadeDto>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.nome))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.UF, opt => opt.MapFrom(src => src.uF));
        }
    }
}
