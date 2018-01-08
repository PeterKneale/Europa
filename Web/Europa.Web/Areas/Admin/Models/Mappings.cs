using System;
using AutoMapper;
using Europa.Query.Messages.Models;
using Europa.Write.Messages;
using System.Linq;

namespace Europa.Web.Areas.Admin.Models
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Category, CategoryEditModel>();
            CreateMap<CategoryEditModel, CreateCategoryCommand>();
            CreateMap<Podcast, PodcastViewModel>();
            CreateMap<Podcast, PodcastEditModel>();
        }
    }
}