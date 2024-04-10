using Dapper;
using Microsoft.Data.SqlClient;
using MOBILEAPI2024.DAL.Entities;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.RequestDTO.Travel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOBILEAPI2024.DAL.Repositories
{
    public class TravelRepository : SqlDbRepository<ActiveInActiveUser>, ITravelRepository
    {
        public TravelRepository(string connectionString) : base(connectionString) { }

        public dynamic DisplayTavelType(int cmpId, string transType)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", cmpId);
            vParams.Add("@Type", transType);
            var response = vconn.Query("SP_MOBILE_HRMS_WEBSERVICE_Travel_Type", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic GetTravelAPIData(GetTravelAPIDataRequest getTravelAPIDataRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Travel_Application_ID", getTravelAPIDataRequest.TravelApplicationID);
            vParams.Add("@Rpt_Level", getTravelAPIDataRequest.RptLevel);
            vParams.Add("@EMP_ID", getTravelAPIDataRequest.EmpID);
            vParams.Add("@Type", getTravelAPIDataRequest.Type);
            var response = vconn.Query("SP_Mobile_HRMS_WebService_TravelAPI_Data", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic TravelAllDetails(TravelAllDetailsRequest travelAllDetailsRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            //vParams.Add("@Travel_Application_ID", travelAllDetailsRequest.TravelApplicationID);
            //vParams.Add("@Tran_Type", travelAllDetailsRequest.StringType);
            //vParams.Add("@Cmp_ID", travelAllDetailsRequest.CmpID);
            //vParams.Add("@Emp_id", travelAllDetailsRequest.EmpID);
            //vParams.Add("@S_Emp_ID", travelAllDetailsRequest.SEmpID);
            //vParams.Add("@Chk_International", travelAllDetailsRequest.CheckInternational);
            //vParams.Add("@Travel_Details", ConvertJsonToXml(travelAllDetailsRequest.strTravelDetails));
            //vParams.Add("@Travel_Other_Details", StrTourOtherDetails);
            //vParams.Add("@Travel_Adv_Details", strTravelAdvDetails);
            //vParams.Add("@TourAgendaPlanned", strTourAgenda);
            //vParams.Add("@Login_ID", Login_ID);
            //vParams.Add("@TravelTypeId", TravelTypeId);
            //vParams.Add("@User_Id", Login_ID);
            //vParams.Add("@AttachedDocuments", StrAttachment);
            //vParams.Add("@Application_Date", Application_Date, SqlDbType.DateTime);
            //vParams.Add("@Application_Status", Application_Status, SqlDbType.Char);
            //vParams.Add("@Result", "");

            var response = vconn.Query("SP_Mobile_HRMS_WebService_TravelAPI_Data", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic TravelApp(TravelAppRequest travelAppRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", travelAppRequest.CmpID);
            vParams.Add("@Emp_ID", travelAppRequest.EmpID);
            vParams.Add("@Type", travelAppRequest.TranType);
            vParams.Add("@FromDate", travelAppRequest.FromDate);
            vParams.Add("@ToDate", travelAppRequest.ToDate);
            var response = vconn.Query("SP_Mobile_WebService_TravelApp", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic TravelApplicationDelete(TravelApplicationDeleteRequest travelApplicationDeleteRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Travel_Application_ID", travelApplicationDeleteRequest.Travel_Application_Id);
            vParams.Add("@Cmp_ID", travelApplicationDeleteRequest.Cmp_Id);
            vParams.Add("@Emp_ID", travelApplicationDeleteRequest.Emp_Id);
            vParams.Add("@Login_ID", travelApplicationDeleteRequest.Login_ID);
            vParams.Add("@Type", travelApplicationDeleteRequest.Tran_Type);
            vParams.Add("@Result", "");
            var response = vconn.Query("Mobile_HRMS_P0100_TRAVEL_APPLICATION_DELETE", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic TravelApprovalDelete(TravelApprovalDeleteRequest travelApprovalDeleteRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Travel_Approval_ID", travelApprovalDeleteRequest.TravelApprovalId);
            vParams.Add("@Travel_Application_ID", travelApprovalDeleteRequest.TravelApplicationId);
            vParams.Add("@Cmp_ID", travelApprovalDeleteRequest.CmpId);
            vParams.Add("@Emp_ID", travelApprovalDeleteRequest.EmpId);
            vParams.Add("@S_Emp_ID", travelApprovalDeleteRequest.SEmpId);
            vParams.Add("@Approval_Date", travelApprovalDeleteRequest.ApprovalDate);
            vParams.Add("@Login_ID", travelApprovalDeleteRequest.LoginId);
            vParams.Add("@Rpt_Level", travelApprovalDeleteRequest.ReportLevel);
            vParams.Add("@Type", travelApprovalDeleteRequest.Type);
            vParams.Add("@Result", "");
            var response = vconn.Query("Mobile_HRMS_P0100_TRAVEL_APPROVAL_Delete_Level", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic TravelProof(TravelProofRequest travelProofRequest)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", travelProofRequest.CmpId);
            vParams.Add("@Emp_ID", travelProofRequest.EmpId);
            vParams.Add("@start_Journey", travelProofRequest.StartJourney);
            vParams.Add("@Reach_Destination", travelProofRequest.ReachDestination);
            vParams.Add("@t_Event", travelProofRequest.Event);
            vParams.Add("@End_Journey", travelProofRequest.EndJourney);
            vParams.Add("@Result", "");

            var response = vconn.Query("SP_Mobile_HRMS_WebService_Travel_picCount", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic TravelProofInsert(TravelProofInsertRequest travelProofInsertRequest,string imagePath,string strDocName)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_ID", travelProofInsertRequest.CmpId);
            vParams.Add("@Emp_ID", travelProofInsertRequest.EmpId);
            vParams.Add("@TravelApp_Code", travelProofInsertRequest.TravelApplicationCode);
            vParams.Add("@Image_Name", strDocName);
            vParams.Add("@Image_Path", imagePath);
            vParams.Add("@Travel_Proof_Type", travelProofInsertRequest.TravelProofType);
            vParams.Add("@Result", "");
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Travel_Proof", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic TravelProofValidation(int cmpId, int empId, int travelAppCode)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            string query;

            query = "SELECT Travel_Proof_Type FROM T0080_Emp_Travel_Proof WHERE Cmp_ID=@CmpID AND Emp_ID=@EmpId AND TravelApp_Code=@TravelAppCode";
            vParams.Add("@CmpID", cmpId);
            vParams.Add("@EmpId", empId);
            vParams.Add("@TravelAppCode", travelAppCode);
            var response = vconn.Query<dynamic>(query, vParams); // Pass the parameters object
            return response;
        }

        public dynamic Travel_Approval(int cmpId, int empId, string strType)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_Id", empId);
            vParams.Add("@Cmp_Id", cmpId);
            vParams.Add("@StrType", strType);
            vParams.Add("@Rpt_level", 0);
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Travel_Approval_List", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic Travel_Approval_AdminSetting(int cmpId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_Id", cmpId);
            var response = vconn.Query("SP_Mobile_HRMS_WebService_AdminSettings", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic Travel_Mode_Ddl(int cmpId, int empId, char tranType)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Emp_Id", empId);
            vParams.Add("@Cmp_Id", cmpId);
            vParams.Add("@tran_type", tranType);
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Get_TravelMode_Desg", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }

        public dynamic Travel_Settlement(int cmpId)
        {
            using var vconn = GetOpenConnection();
            var vParams = new DynamicParameters();
            vParams.Add("@Cmp_Id", cmpId);
            var response = vconn.Query("SP_Mobile_HRMS_WebService_Expense_Ddl", vParams, commandType: CommandType.StoredProcedure);
            return response;
        }
    }
}
