using AutoMapper;
using MOBILEAPI2024.DTO.RequestDTO.Claim;

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
            CreateMap<int, ClaimRecords>().ReverseMap();
        }
    }
}

