namespace LMS.Common
{
    public class SPConstants
    {
        //Personal Details
        public const string employeeDetails = "LMS_EmployeeDetails";
        public const string contactDetails = "LMS_ContactDetails";
        public const string teamDetails = "LMS_TeamDetails";

        //Employment Details
        public const string jobDetails = "LMS_JobDetails";
        public const string salaryDetails = "LMS_SalaryDetails";

        //Leave Management
        public const string myLeaveHistory = "LMS_GetMyLeaveHistory";
        public const string usersLeaveHistory = "LMS_GetUsersLeaveHistory";
        public const string leaveRequest = "LMS_GetLeaveRequest";
        public const string leaveSummary = "LMS_GetLeaveSummary";
        public const string updateLeaveApplication = "LMS_UpdateLeaveApplication";
        public const string deleteLeaveApplication = "LMS_DeleteLeaveApplication";
        public const string applyLeave = "LMS_ApplyLeave";
        public const string updateLeaveRequest = "LMS_UpdateLeaveRequest";

        //Common Dropdowns
        public const string commonDropdowns = "LMS_GetCommonByType";  
        public const string teamDropdowns = "LMS_GetTeam";
        public const string empDropdowns = "LMS_GetEmployeesName"; 

        // Employee
        public const string empFullDetails = "LMS_GetEmployeeFullDetails";
        public const string insertFullEmployeeDetails = "LMS_InsertFullEmployeeDetails";
        public const string updateFullEmployeeDetails = "LMS_UpdateFullEmployeeDetails";

        //GooseMenu
        public const string gooseMenu = "LMS_GetGooseMenu_Hierarchical";

        //Login
        public const string login = "LMS_EmployeeLogin";
    }
}