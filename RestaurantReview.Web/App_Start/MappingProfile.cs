using RestaurantReview.Core.Models;
using RestaurantReview.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantReview.Web.App_Start
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Restaurant, RestaurantListViewModels>()
                .ForMember(p => p.CountOfReviews, ex => ex.MapFrom(p => p.Reviews.Count))
                .ForMember(p => p.AverageScore, ex => 
                ex.MapFrom(p => p.Reviews.Any() ? Math.Round(p.Reviews.Average(rv => rv.Rating), 1) : 0));
        }

    }
}