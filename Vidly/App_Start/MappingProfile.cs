using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {

        public MappingProfile() 
        {
            // API <- Inbound, to make sure ignore customer id, so wont get exception saying cant update id
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore()); ;
            // API -> Outbound
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            Mapper.CreateMap<Genre, GenreDto>();





            // API <- Inbound, to make sure ignore movie id, so wont get exception saying cant update id
            Mapper.CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            // API -> Outbound
            Mapper.CreateMap<Movie, MovieDto>();

        }
    }
}

