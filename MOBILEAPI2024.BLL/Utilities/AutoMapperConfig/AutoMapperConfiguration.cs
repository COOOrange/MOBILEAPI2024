using AutoMapper;
using MOBILEAPI2024.DTO.RequestDTO.Attendance;
using MOBILEAPI2024.DTO.RequestDTO.Claim;
using MOBILEAPI2024.DTO.RequestDTO.CompOff;
using MOBILEAPI2024.DTO.RequestDTO.Employee;
using MOBILEAPI2024.DTO.RequestDTO.Grievance;
using MOBILEAPI2024.DTO.RequestDTO.Leave;
using MOBILEAPI2024.DTO.RequestDTO.Medical;
using MOBILEAPI2024.DTO.RequestDTO.Ticket;
using MOBILEAPI2024.DTO.RequestDTO.User;
using MOBILEAPI2024.DTO.ResponseDTO.Employee;
using MOBILEAPI2024.DTO.ResponseDTO.User;

namespace HRMS.BLL.Utilities.AutoMapperConfig
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<ClaimAppDetailsRequest, ClaimRecords>().ReverseMap();
            CreateMap<ClaimApplicationRequest, ClaimRecords>().ReverseMap();
            CreateMap<ClaimApplicationDeleteRequest, ClaimRecords>().ReverseMap();
            CreateMap<ClaimApplicationDetailsRequest, ClaimRecords>().ReverseMap();
            CreateMap<ClaimApplicationRecordsRequest, ClaimRecords>().ReverseMap();
            CreateMap<ClaimApplicationStatusRequest, ClaimRecords>().ReverseMap();
            CreateMap<ClaimLimitRequest, ClaimRecords>().ReverseMap();

            CreateMap<AttendanceInsertRequest, AttendanceInsert>().ReverseMap();
            CreateMap<LeaveBalanceRequest, AttendanceRegularizeDetails>().ReverseMap();
            CreateMap<AttendanceRegularizeApprovalRequest, AttendanceRegularizeInsert>().ReverseMap();
            CreateMap<AttendanceRegularizeInsertRequest, AttendanceRegularizeInsert>().ReverseMap();
            CreateMap<AddCommentRequest, GetNotification>().ReverseMap();

            CreateMap<strDetails, GetNotification>().ReverseMap();
            CreateMap<GetLikeCommentListRequest, GetNotification>().ReverseMap();
            CreateMap<GetNotificationRequest, GetNotification>().ReverseMap();
            CreateMap<ChangeRequestDepInsertRequest, ChangeRequest>().ReverseMap();
            CreateMap<ChangeRequestFavInsertRequest, ChangeRequestFav>().ReverseMap();

            CreateMap<CompOffApplicationRequest, CompOffApplication>().ReverseMap();
            CreateMap<CompOffApprovalRequest, CompOffApplication>().ReverseMap();
            
            CreateMap<TicketApplicationRequest, BindTicketRecords>().ReverseMap();
            CreateMap<TicketApplicationStatusRequest, BindTicketRecords>().ReverseMap();
            CreateMap<TicketApprovalRequest, BindTicketRecords>().ReverseMap();

            CreateMap<GrievanceApplicationRequest, GrievanceApplication>().ReverseMap();

            CreateMap<MedicalInsertRequest, MedicalInsert>().ReverseMap();
            CreateMap<MedicalUpdateRequest, MedicalInsert>().ReverseMap();



        }
    }
}

