using AutoMapper;
using LMS.Models.DataModels;
using LMS.Models.ViewModels;

namespace LMS.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Employee details mapping
            CreateMap<EmployeeDetailsModel, EmployeeDetailsViewModel>()
                .ForMember(dest => dest.employeeId, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.First_Name))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.Last_Name))
                .ForMember(dest => dest.dob, opt => opt.MapFrom(src => src.Date_Of_Birth))
                .ForMember(dest => dest.gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.maritalStatus, opt => opt.MapFrom(src => src.Marital_Status))
                .ForMember(dest => dest.nationality, opt => opt.MapFrom(src => src.Nationality));

            // Code master mapping
            CreateMap<CommonModel, CommonViewModel>();
        }
    }
}
