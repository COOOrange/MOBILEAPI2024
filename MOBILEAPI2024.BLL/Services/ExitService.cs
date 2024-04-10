using Microsoft.Extensions.Options;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Exit;
using System.Collections;

namespace MOBILEAPI2024.BLL.Services
{
    public class ExitService : IExitService
    {
        private readonly IExitRepository _exitRepository;
        private readonly AppSettings _appSettings;
        public ExitService(IExitRepository exitRepository,IOptions<AppSettings> appSettings)
        {
            _exitRepository = exitRepository;
            _appSettings = appSettings.Value;
        }

        public dynamic AddExitAppilcation(AddExitAppilcationRequest addExitAppilcationRequest)
        {
            string strDocName = string.Empty;
            string strFilesName = string.Empty;
            string strDocPath = string.Empty;
            string strDocPath2 = string.Empty;
            byte[] docBytes;
            string dsFileName = string.Empty;
            string strAllFileName = string.Empty;

            //if (!string.IsNullOrEmpty(addExitAppilcationRequest.ExitAppDoc))
            //{
            //    dtExitDetails = JsonConverter(Serializer()).(addExitAppilcationRequest.ExitAppDoc);
            //    ds.Tables.Add(dtExitDetails);
            //    ds.Tables[0].TableName = "ExitDetails";
            //    if (ds.Tables[0].Columns.Contains("Exit_Attachment"))
            //    {
            //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //        {
            //            string dsFile = ds.Tables[0].Rows[i]["Exit_Attachment"].ToString();
            //            if (!string.IsNullOrEmpty(dsFile))
            //            {
            //                string dsFileName = "";
            //                dsFileName = Convert.ToString(EmpID) + "" + ((Convert.ToString(DateTime.Now)).Replace("/", "")).Replace(" ", "").Replace(":", "") + "_" + i; // 26-11-2020 Deepal
            //                dsFileName = dsFileName.Replace("|", "#");
            //                dsFileName = dsFileName + ds.Tables[0].Rows[i]["Exit_Extension"].ToString();
            //                string strDocPath2 = _appSettings.ExitPath.ToString() + dsFileName;
            //                byte[] docBytes = Convert.FromBase64String(dsFile);
            //                using (MemoryStream ms = new MemoryStream(docBytes))
            //                {
            //                    using (FileStream fs = new FileStream(strDocPath2, FileMode.Create))
            //                    {
            //                        ms.WriteTo(fs);
            //                    }
            //                }
            //                ds.Tables[0].Rows[i]["Exit_Attachment"] = dsFileName.ToString();
            //                strAllFileName = strAllFileName + dsFileName + "#";
            //            }
            //        }
            //    }
            //}

            //if (!string.IsNullOrEmpty(strAllFileName))
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        int LstIndex = strAllFileName.LastIndexOf("#");
            //        if (LstIndex > -1)
            //        {
            //            strAllFileName = strAllFileName.Remove(LstIndex).ToString();
            //        }
            //    }
            //}

            var exitResponse = _exitRepository.AddExitAppilcation(addExitAppilcationRequest);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }

        public dynamic AddExitApprovalData(AddExitApprovaldataRequest addExitApprovaldataRequest)
        {
            var exitResponse = _exitRepository.AddExitApprovalData(addExitApprovaldataRequest);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
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

        public dynamic ExitApplicationNoticePeriod(ExitApplicationNoticePeriodRequest exitApplicationNoticePeriodRequest)
        {
            var exitResponse = _exitRepository.ExitApplicationNoticePeriod(exitApplicationNoticePeriodRequest);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }

        public dynamic ExitApplicationPreQuestion(int cmpId, int branchID)
        {
            var exitResponse = _exitRepository.ExitApplicationPreQuestion(cmpId,branchID);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }

        public dynamic ExitApplicationValidate(int cmpID, int empID)
        {
            var exitResponse = _exitRepository.ExitApplicationValidate(cmpID, empID);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }

        public dynamic GetExitApplicationRecords(int cmpID, int empID)
        {
            var exitResponse = _exitRepository.GetExitApplicationRecords(cmpID, empID);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }

        public dynamic GetExitApporvalRecords(int cmpID, int empID, string status)
        {
            var exitResponse = _exitRepository.GetExitApporvalRecords(cmpID,empID, status);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }

        public dynamic GetExitApprovalEMPData(GetExitApprovalEMPDataRequest getExitApprovalEMPDataRequest)
        {
            var exitResponse = _exitRepository.GetExitApprovalEMPData(getExitApprovalEMPDataRequest);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }

        public dynamic GetExitInterviewQAInterview(int cmpId, int empId, int exitId)
        {
            var exitResponse = _exitRepository.GetExitInterviewQAInterview(cmpId,empId,exitId);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }

        public dynamic GetExitTermsandConditions(int cmpId)
        {
            var exitResponse = _exitRepository.GetExitTermsandConditions(cmpId);
            if ((exitResponse as ICollection)?.Count == 0)
            {
                return null;
            }
            return exitResponse;
        }
    }
}
