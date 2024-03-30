using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Exit;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.BLL.Services
{
    public class ExitService : IExitService
    {
        private readonly IExitRepository _exitRepository;
        public ExitService(IExitRepository exitRepository)
        {
            _exitRepository = exitRepository;
        }

        public dynamic AddExitAppilcation(AddExitAppilcationRequest addExitAppilcationRequest)
        {
            var exitResponse = _exitRepository.AddExitAppilcation(addExitAppilcationRequest);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }

        public dynamic AddExitApprovalData(AddExitApprovaldataRequest addExitApprovaldataRequest)
        {
            throw new NotImplementedException();
        }

        public dynamic ExitAppInsert(ExitAppInsertRequest exitAppInsertRequest)
        {
            if(exitAppInsertRequest.ExitDetails.Count() > 0)
            {
                foreach(var item in exitAppInsertRequest.ExitDetails) 
                {
                    var exitResponse = _exitRepository.ExitAppInsert(exitAppInsertRequest,item.QUEST_ID,item.Answer_rate,item.Comments);
                    if ((exitResponse as ICollection)?.Count == 0)
                    {
                        return null;
                    }
                    return exitResponse;
                }
            }
            return null;
            
        }

        public dynamic ExitApplicationDelete(ExitApplicationDeleteRequest exitApplicationDeleteRequest)
        {
            var exitResponse = _exitRepository.ExitApplicationDelete(exitApplicationDeleteRequest);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }
    }
}
