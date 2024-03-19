using AutoMapper;

namespace HRMS.BLL.Utilities.AutoMapperConfig
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            //CreateMap<User, UserDTO>().ReverseMap(); // Map from User to UserModel and vice versa
            //CreateMap<Address, AddressDTO>().ReverseMap(); // Map from User to UserModel and vice versa
            //CreateMap<Salary, SalaryDTO>().ReverseMap(); // Map from User to UserModel and vice versa
        }
    }
}

