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

            // Contact details mapping
            CreateMap<ContactDetailsModel, ContactDetailsViewModel>()
                .ForMember(dest => dest.employeeId, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.Phone_Number))
                .ForMember(dest => dest.alternateNumber, opt => opt.MapFrom(src => src.Alternate_Number))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.streetAddress, opt => opt.MapFrom(src => src.Street_Address))
                .ForMember(dest => dest.city, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.state, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.zipCode, opt => opt.MapFrom(src => src.Zip_Code))
                .ForMember(dest => dest.country, opt => opt.MapFrom(src => src.Country));

            // Team details mapping
            CreateMap<TeamDetailsModel, TeamDetailsViewModel>()
                .ForMember(dest => dest.employeeId, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.designation, opt => opt.MapFrom(src => src.Designation))
                .ForMember(dest => dest.teamManager, opt => opt.MapFrom(src => src.Team_HR_Head))
                .ForMember(dest => dest.projectManager, opt => opt.MapFrom(src => src.Project_Manager))
                .ForMember(dest => dest.teamLead, opt => opt.MapFrom(src => src.Team_Lead));

            // Job details mapping
            CreateMap<JobDetailsModel, JobDetailsViewModel>()
                .ForMember(dest => dest.empId, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.jobTitle, opt => opt.MapFrom(src => src.Job_Title))
                .ForMember(dest => dest.employmentStatus, opt => opt.MapFrom(src => src.Employment_Status))
                .ForMember(dest => dest.joinedDate, opt => opt.MapFrom(src => src.Joined_Date))
                .ForMember(dest => dest.skillset, opt => opt.MapFrom(src => src.Skillset));
            
            // Salary details mapping
            CreateMap<SalaryDetailsModel, SalaryDetailsViewModel>()
                .ForMember(dest => dest.empId, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.payGrade, opt => opt.MapFrom(src => src.Pay_Grade))
                .ForMember(dest => dest.currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.basicSalary, opt => opt.MapFrom(src => src.Basic_Salary))
                .ForMember(dest => dest.payFrequency, opt => opt.MapFrom(src => src.Pay_Frequency));
        }
    }
}
