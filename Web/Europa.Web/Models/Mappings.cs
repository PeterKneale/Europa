using System;
using AutoMapper;
using Europa.Query.Messages.Models;

namespace Europa.Web.Models
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Podcast, PodcastViewModel>();
        }
    }
}