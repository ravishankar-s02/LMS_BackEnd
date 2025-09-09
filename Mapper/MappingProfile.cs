using AutoMapper;
using LMS.Models.DataModels;
using LMS.Models.ViewModels;

namespace LMS.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 1. Employee details mapping
            CreateMap<EmployeeDetailsModel, EmployeeDetailsViewModel>()
                .ForMember(dest => dest.empCode, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.First_Name))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.Last_Name))
                .ForMember(dest => dest.dob, opt => opt.MapFrom(src => src.Date_Of_Birth))
                .ForMember(dest => dest.gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.maritalStatus, opt => opt.MapFrom(src => src.Marital_Status))
                .ForMember(dest => dest.nationality, opt => opt.MapFrom(src => src.Nationality));

            // 2. Code master mapping
            CreateMap<CommonModel, CommonViewModel>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.SS_Code_PK))
                .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.SS_Code_Type))
                .ForMember(dest => dest.code, opt => opt.MapFrom(src => src.SS_Code_Code))
                .ForMember(dest => dest.screenName, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.defaultFlag, opt => opt.MapFrom(src => src.Default_Flag));

            CreateMap<TeamModel, TeamViewModel>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.SS_Team_PK))
                .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.SS_Code_Type))
                .ForMember(dest => dest.code, opt => opt.MapFrom(src => src.SS_Code_Code))
                .ForMember(dest => dest.screenName, opt => opt.MapFrom(src => src.Screen_Name))
                .ForMember(dest => dest.defaultFlag, opt => opt.MapFrom(src => src.Default_Flag));

            // 3. Contact details mapping
            CreateMap<ContactDetailsModel, ContactDetailsViewModel>()
                .ForMember(dest => dest.empCode, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.Phone_Number))
                .ForMember(dest => dest.alternateNumber, opt => opt.MapFrom(src => src.Alternate_Number))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.streetAddress, opt => opt.MapFrom(src => src.Street_Address))
                .ForMember(dest => dest.city, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.state, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.zipCode, opt => opt.MapFrom(src => src.Zip_Code))
                .ForMember(dest => dest.country, opt => opt.MapFrom(src => src.Country));

            // 4. Team details mapping
            CreateMap<TeamDetailsModel, TeamDetailsViewModel>()
                .ForMember(dest => dest.empCode, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.designation, opt => opt.MapFrom(src => src.Designation))
                .ForMember(dest => dest.teamAndHrHead, opt => opt.MapFrom(src => src.Team_HR_Head))
                .ForMember(dest => dest.projectManager, opt => opt.MapFrom(src => src.Project_Manager))
                .ForMember(dest => dest.teamLead, opt => opt.MapFrom(src => src.Team_Lead));

            // 5. Job details mapping
            CreateMap<JobDetailsModel, JobDetailsViewModel>()
                .ForMember(dest => dest.empCode, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.jobTitle, opt => opt.MapFrom(src => src.Job_Title))
                .ForMember(dest => dest.employmentStatus, opt => opt.MapFrom(src => src.Employment_Status))
                .ForMember(dest => dest.joinedDate, opt => opt.MapFrom(src => src.Joined_Date))
                .ForMember(dest => dest.skillset, opt => opt.MapFrom(src => src.Skillset));

            // 6. Salary details mapping
            CreateMap<SalaryDetailsModel, SalaryDetailsViewModel>()
                .ForMember(dest => dest.empCode, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.payGrade, opt => opt.MapFrom(src => src.Pay_Grade))
                .ForMember(dest => dest.currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.basicSalary, opt => opt.MapFrom(src => src.Basic_Salary))
                .ForMember(dest => dest.payFrequency, opt => opt.MapFrom(src => src.Pay_Frequency));

            //7. Get Employee Full Details
            CreateMap<EmployeeFullProfileModel, EmployeeFullProfileViewModel>()
                .ForMember(dest => dest.empPk, opt => opt.MapFrom(src => src.SS_Emp_PK))
                .ForMember(dest => dest.empCode, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.empStatus, opt => opt.MapFrom(src => src.Emp_Status))
                .ForMember(dest => dest.firstName, opt => opt.MapFrom(src => src.First_Name))
                .ForMember(dest => dest.lastName, opt => opt.MapFrom(src => src.Last_Name))
                .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.Full_Name))
                .ForMember(dest => dest.dateOfBirth, opt => opt.MapFrom(src => src.Date_Of_Birth))
                .ForMember(dest => dest.gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.maritalStatus, opt => opt.MapFrom(src => src.Marital_Status))
                .ForMember(dest => dest.nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.phoneNumber, opt => opt.MapFrom(src => src.Phone_Number))
                .ForMember(dest => dest.alternateNumber, opt => opt.MapFrom(src => src.Alternate_Number))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.streetAddress, opt => opt.MapFrom(src => src.Street_Address))
                .ForMember(dest => dest.city, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.state, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.zipCode, opt => opt.MapFrom(src => src.Zip_Code))
                .ForMember(dest => dest.country, opt => opt.MapFrom(src => src.Country))
                .ForMember(dest => dest.designation, opt => opt.MapFrom(src => src.Designation))
                .ForMember(dest => dest.teamHRHead, opt => opt.MapFrom(src => src.Team_HR_Head))
                .ForMember(dest => dest.projectManager, opt => opt.MapFrom(src => src.Project_Manager))
                .ForMember(dest => dest.teamLead, opt => opt.MapFrom(src => src.Team_Lead))
                .ForMember(dest => dest.jobTitle, opt => opt.MapFrom(src => src.Job_Title))
                .ForMember(dest => dest.employmentStatus, opt => opt.MapFrom(src => src.Employment_Status))
                .ForMember(dest => dest.joinedDate, opt => opt.MapFrom(src => src.Joined_Date))
                .ForMember(dest => dest.skillset, opt => opt.MapFrom(src => src.Skillset))
                .ForMember(dest => dest.payGrade, opt => opt.MapFrom(src => src.Pay_Grade))
                .ForMember(dest => dest.currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.basicSalary, opt => opt.MapFrom(src => src.Basic_Salary))
                .ForMember(dest => dest.payFrequency, opt => opt.MapFrom(src => src.Pay_Frequency));

            //8. Get My Leave
            CreateMap<MyLeaveHistoryModel, MyLeaveHistoryViewModel>()
                .ForMember(dest => dest.leaveId, opt => opt.MapFrom(src => src.SS_Leave_PK))
                .ForMember(dest => dest.empCode, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.leaveType, opt => opt.MapFrom(src => src.Leave_Type))
                .ForMember(dest => dest.fromDate, opt => opt.MapFrom(src => src.From_Date))
                .ForMember(dest => dest.toDate, opt => opt.MapFrom(src => src.To_Date))
                .ForMember(dest => dest.fromTime, opt => opt.MapFrom(src => src.From_Time))
                .ForMember(dest => dest.toTime, opt => opt.MapFrom(src => src.To_Time))
                .ForMember(dest => dest.duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.leaveStatus, opt => opt.MapFrom(src => src.Leave_Status))
                .ForMember(dest => dest.reason, opt => opt.MapFrom(src => src.Reason));

            //9. Get Users Leave
            CreateMap<UsersLeaveHistoryModel, UsersLeaveHistoryViewModel>()
                .ForMember(dest => dest.empCode, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.leaveType, opt => opt.MapFrom(src => src.LeaveType))
                .ForMember(dest => dest.fromDate, opt => opt.MapFrom(src => src.From_Date))
                .ForMember(dest => dest.toDate, opt => opt.MapFrom(src => src.To_Date))
                .ForMember(dest => dest.duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.leaveStatus, opt => opt.MapFrom(src => src.Leave_Status))
                .ForMember(dest => dest.reason, opt => opt.MapFrom(src => src.Reason))
                .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.Full_Name));

            //10. Get Leave Action
            CreateMap<LeaveActionModel, LeaveActionViewModel>()
                .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.Full_Name))
                .ForMember(dest => dest.designation, opt => opt.MapFrom(src => src.Job_Title))
                .ForMember(dest => dest.leavePK, opt => opt.MapFrom(src => src.SS_Leave_PK))
                .ForMember(dest => dest.leaveType, opt => opt.MapFrom(src => src.Leave_Type))
                .ForMember(dest => dest.duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.reason, opt => opt.MapFrom(src => src.Reason))
                .ForMember(dest => dest.fromDate, opt => opt.MapFrom(src => src.From_Date))
                .ForMember(dest => dest.toDate, opt => opt.MapFrom(src => src.To_Date))
                .ForMember(dest => dest.leaveStatus, opt => opt.MapFrom(src => src.Leave_Status))
                .ForMember(dest => dest.updatedUser, opt => opt.MapFrom(src => src.Updated_User));

            //11. Get Employee Name Dropdown
            CreateMap<EmployeeNameModel, EmployeeNameViewModel>()
                .ForMember(dest => dest.empCode, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.fullName, opt => opt.MapFrom(src => src.Full_Name));
            
            //12. Get Leave Summary
            CreateMap<MyLeaveSummaryModel, MyLeaveSummaryViewModel>()
                .ForMember(dest => dest.empCode, opt => opt.MapFrom(src => src.SS_Emp_Code))
                .ForMember(dest => dest.leaveType, opt => opt.MapFrom(src => src.Leave_Type))
                .ForMember(dest => dest.leaveTaken, opt => opt.MapFrom(src => src.Leave_Taken))
                .ForMember(dest => dest.leaveScheduled, opt => opt.MapFrom(src => src.Leave_Scheduled))
                .ForMember(dest => dest.leaveRemaining, opt => opt.MapFrom(src => src.Leave_Remaining));
                
        }
    }
}