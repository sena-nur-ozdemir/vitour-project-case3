using AutoMapper;
using Case3Vitour.Dtos.CategoryDtos;
using Case3Vitour.Dtos.GalleryDtos;
using Case3Vitour.Dtos.ReservationDtos;
using Case3Vitour.Dtos.ReviewDtos;
using Case3Vitour.Dtos.TourDtos;
using Case3Vitour.Dtos.TourPlanDtos;
using Case3Vitour.Entities;

namespace Case3Vitour.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Tour, ResultTourDto>().ReverseMap();
            CreateMap<Tour, TourCardDto>().ReverseMap();
            CreateMap<Tour, TourDetailDto>().ReverseMap();
            CreateMap<Tour, CreateTourDto>().ReverseMap();
            CreateMap<Tour, GetTourByIdDto>().ReverseMap();
            CreateMap<Tour, UpdateTourDto>().ReverseMap();

            CreateMap<TourPlan, ResultTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, ResultTourPlanByTourIdDto>().ReverseMap();
            CreateMap<TourPlan, CreateTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, UpdateTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, GetTourPlanByIdDto>().ReverseMap();

            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();

            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, ResultReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            CreateMap<Review, GetReviewByIdDto>().ReverseMap();
            CreateMap<Review, ResultReviewByTourIdDto>().ReverseMap();

            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, ResultReservationDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
            CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();
            CreateMap<Reservation, ResultReservationByTourIdDto>().ReverseMap();

            CreateMap<Gallery, CreateGalleryDto>().ReverseMap();
            CreateMap<Gallery, GetGalleryByIdDto>().ReverseMap();
            CreateMap<Gallery, ResultGalleryByTourIdDto>().ReverseMap();
            CreateMap<Gallery, ResultGalleryDto>().ReverseMap();
            CreateMap<Gallery, UpdateGalleryDto>().ReverseMap();
        }
    }
}