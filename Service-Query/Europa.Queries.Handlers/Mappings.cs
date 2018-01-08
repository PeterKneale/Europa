using AutoMapper;
using Europa.Query.Data;
using Europa.Query.Messages.Models;
using System;
using System.Linq;

namespace Europa.Query.Handlers
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<CategoryData, Category>();
            CreateMap<PodcastData, Podcast>()
                .ForMember(dst => dst.Tags, opt => opt.MapFrom(src => GetTags(src)));
        }

        private static string[] GetTags(PodcastData src)
        {
            if (string.IsNullOrEmpty(src.Tags))
            {
                return Array.Empty<string>();
            }
            return src.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}