using AutoMapper;
using Entities.DTOs.AccountDTOs;
using Entities.DTOs.CustomerDTOs;
using Entities.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public class Map: Profile
    {
        public Map()
        {
            CreateMap<Account, AccountAddDTO>().ReverseMap();
            CreateMap<Account, AccountUpdateDTO>().ReverseMap();
            CreateMap<Account, AccountListDTO>().ReverseMap();
            CreateMap<Customer, CustomerAddDTO>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDTO>().ReverseMap();
            CreateMap<Customer, CustomerListDTO>()
            .ForMember(dest => dest.Accounts,
                opt => opt.MapFrom(src => src.Accounts)) 
            .ForMember(dest => dest.AccountCount,
                opt => opt.MapFrom(src => src.Accounts.Count));
            CreateMap<AccountListDTO, AccountUpdateDTO>().ReverseMap();
            CreateMap<CustomerListDTO, CustomerUpdateDTO>().ReverseMap();

        }

    }
}
