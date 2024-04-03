using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MOBILEAPI2024.BLL.Services.IServices;
using MOBILEAPI2024.DAL.Repositories;
using MOBILEAPI2024.DAL.Repositories.IRepositories;
using MOBILEAPI2024.DTO.Common;
using MOBILEAPI2024.DTO.RequestDTO.Ticket;
using MOBILEAPI2024.DTO.RequestDTO.Travel;
using MOBILEAPI2024.DTO.RequestDTO.User;
using MOBILEAPI2024.DTO.ResponseDTO.User;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MOBILEAPI2024.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper, IOptions<AppSettings> appSetting)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _appSettings = appSetting.Value;
        }

        public void AddClockIn(ClockIn clockIn)
        {
            _userRepository.AddClockIn(clockIn);
        }

        public void AddClockOut(ClockIn clockIn)
        {
            _userRepository.AddClockOut(clockIn);
        }

        public dynamic AddComment(AddCommentRequest addCommentRequest)
        {

            var getNotificatioon = _mapper.Map<AddCommentRequest, GetNotification>(addCommentRequest);
            getNotificatioon.NotificationDate = DateTime.Now;

            if (getNotificatioon.Comment == "D")
            {
                getNotificatioon.strType = "CD";
                getNotificatioon.Comment = "";
                var notificationResponse = _userRepository.GetNotification(getNotificatioon);
                if ((notificationResponse as ICollection)?.Count == 0 || notificationResponse == null)
                {
                    return null;
                }
                return notificationResponse;
            }
            getNotificatioon.strType = "CI";
            var notificationResponse1 = _userRepository.GetNotification(getNotificatioon);
            if ((notificationResponse1 as ICollection)?.Count == 0 || notificationResponse1 == null)
            {
                return null;
            }
            return notificationResponse1;

        }

        public dynamic AddLike(strDetails item)
        {
            var getNotificatioon = _mapper.Map<strDetails, GetNotification>(item);
            getNotificatioon.NotificationDate = DateTime.Now;

            getNotificatioon.strType = "LI";
            var notificationResponse = _userRepository.GetNotification(getNotificatioon);
            if ((notificationResponse as ICollection)?.Count == 0 || notificationResponse == null)
            {
                return null;
            }
            return notificationResponse;

        }

        public void AddTransactionData(Transaction transactionRequest)
        {
            _userRepository.AddTransactionData(transactionRequest);
        }

        public dynamic AddWorkPlanOnClockIn(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest)
        {
            var Response = _userRepository.AddWorkPlanOnClockIn(addWorkPlanOnClockInRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic AddWorkPlanOnClockOut(AddWorkPlanOnClockInRequest addWorkPlanOnClockInRequest)
        {
            var Response = _userRepository.AddWorkPlanOnClockOut(addWorkPlanOnClockInRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestBind(int cmpId, int empID, string tranType)
        {
            var Response = _userRepository.ChangeRequestBind(cmpId, empID,tranType);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestDepDelete(int cmpId, int empID, int row_ID)
        {
            ChangeRequest changeRequest = new();
            changeRequest.Emp_Id = empID;
            changeRequest.CmpID = cmpId;
            changeRequest.Row_ID = row_ID;
            changeRequest.TranType = "D";
            changeRequest.Request_Date = DateTime.Now;
            changeRequest.Dependant_DOB = DateTime.Now;
            var Response = _userRepository.ChangeRequest(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }
        public dynamic ChangeRequestDepInsert(ChangeRequestDepInsertRequest changeRequestDepInsert)
        {
            string imgName = "";
            if (!string.IsNullOrEmpty(changeRequestDepInsert.ImagePath))
            {
                string strImagePath = Path.Combine(_appSettings.DocPath);
                imgName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + changeRequestDepInsert.EmpId + ".png";
                byte[] imgBytes = Convert.FromBase64String(changeRequestDepInsert.ImagePath);
                File.WriteAllBytesAsync(Path.Combine(strImagePath, imgName), imgBytes);
            }
            else
            {
                imgName = "";
            }

            var changeRequest = _mapper.Map<ChangeRequestDepInsertRequest, ChangeRequest>(changeRequestDepInsert);
            changeRequest.TranType = "I";
            changeRequest.ImageName = imgName;
            var Response = _userRepository.ChangeRequest(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestDepUpdate(ChangeRequestDepInsertRequest changeRequestDepInsert)
        {
            string imgName = "";
            if (!string.IsNullOrEmpty(changeRequestDepInsert.ImagePath))
            {
                string strImagePath = Path.Combine(_appSettings.DocPath);
                imgName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + changeRequestDepInsert.EmpId + ".png";
                byte[] imgBytes = Convert.FromBase64String(changeRequestDepInsert.ImagePath);
                File.WriteAllBytesAsync(Path.Combine(strImagePath, imgName), imgBytes);
            }
            else
            {
                imgName = "";
            }

            var changeRequest = _mapper.Map<ChangeRequestDepInsertRequest, ChangeRequest>(changeRequestDepInsert);
            changeRequest.TranType = "U";
            changeRequest.ImageName = imgName;
            var Response = _userRepository.ChangeRequest(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestFavDelete(int cmpId, int empID, int row_ID)
        {
            ChangeRequestFav changeRequest = new();
            changeRequest.Emp_Id = empID;
            changeRequest.CmpID = cmpId;
            changeRequest.Row_ID = row_ID;
            changeRequest.TranType = "D";
            changeRequest.Request_Date = DateTime.Now;
            var Response = _userRepository.ChangeRequestFav(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestFavInsert(ChangeRequestFavInsertRequest changeRequestDepInsert)
        {
            string imgName = "";
            if (!string.IsNullOrEmpty(changeRequestDepInsert.ImagePath))
            {
                string strImagePath = Path.Combine(_appSettings.DocPath);
                imgName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + changeRequestDepInsert.Emp_Id + ".png";
                byte[] imgBytes = Convert.FromBase64String(changeRequestDepInsert.ImagePath);
                File.WriteAllBytesAsync(Path.Combine(strImagePath, imgName), imgBytes);
            }
            else
            {
                imgName = "";
            }

            var changeRequest = _mapper.Map<ChangeRequestFavInsertRequest, ChangeRequestFav>(changeRequestDepInsert);
            changeRequest.TranType = "I";
            changeRequest.ImagePath = imgName;
            var Response = _userRepository.ChangeRequestFav(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic ChangeRequestFavUpdate(ChangeRequestFavInsertRequest changeRequestDepInsert)
        {
            string imgName = "";
            if (!string.IsNullOrEmpty(changeRequestDepInsert.ImagePath))
            {
                string strImagePath = Path.Combine(_appSettings.DocPath);
                imgName = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + changeRequestDepInsert.Emp_Id + ".png";
                byte[] imgBytes = Convert.FromBase64String(changeRequestDepInsert.ImagePath);
                File.WriteAllBytesAsync(Path.Combine(strImagePath, imgName), imgBytes);
            }
            else
            {
                imgName = "";
            }

            var changeRequest = _mapper.Map<ChangeRequestFavInsertRequest, ChangeRequestFav>(changeRequestDepInsert);
            changeRequest.TranType = "U";
            changeRequest.ImagePath = imgName;
            var Response = _userRepository.ChangeRequestFav(changeRequest);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public bool CheckEnrollNoExixts(Transaction transaction)
        {
            bool checkEnroll = _userRepository.CheckEnrollNoExixts(transaction);
            if (checkEnroll)
            {
                return true;
            }
            return false;
        }

        public bool CheckTransactionExistence(Transaction transaction)
        {
            bool transactiondata = _userRepository.CheckTransactionExistence(transaction);
            if (transactiondata)
            {
                return true;
            }
            return false;
        }

        public DashboardDTO DashboardData(string empId, string cmpId)
        {
            var dashboardData = _userRepository.DashboardData(empId, cmpId);
            if (dashboardData != null)
            {
                return dashboardData;
            }
            return null;
        }

        public dynamic Dashboard_backup(int cmpId, int empID)
        {
            var Response = _userRepository.Dashboard_backup(cmpId,empID);
            if ((Response as ICollection)?.Count == 0 || Response == null)
            {
                return null;
            }
            return Response;
        }

        public dynamic GeoLocationTracking(GeoLocationRequest geoLocationRequest)
        {
            var geoLocationResponse = _userRepository.GeoLocationTracking(geoLocationRequest);
            if ((geoLocationResponse as ICollection)?.Count == 0 || geoLocationResponse == null)
            {
                return null;
            }
            return geoLocationResponse;
        }

        public dynamic GeoLocationTrackingList(int cmpId, int empId, DateTime date)
        {
            var geoLocationResponse = _userRepository.GeoLocationTrackingList(cmpId, empId, date);
            if ((geoLocationResponse as ICollection)?.Count == 0 || geoLocationResponse == null)
            {
                return null;
            }
            return geoLocationResponse;
        }
    }
}
