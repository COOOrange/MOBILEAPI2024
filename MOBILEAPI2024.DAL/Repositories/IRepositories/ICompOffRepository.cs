using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DTO.RequestDTO.CompOff;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories.IRepositories
{
    public interface ICompOffRepository : IGenericRepository<ActiveInActiveUser>
    {
        dynamic CompOffApplication(CompOffApplication compOffApplication);
    }
}
