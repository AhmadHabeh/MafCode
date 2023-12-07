using MafCode.Common;
using MafCode.Models;
using MafCode.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MafCode.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        //public ActionResult Index()
        //{
        //    return View();
        //}

        // GET: UserInfo/Details/id
        public ActionResult Details(string id)
        {
            string Ip = ConfigurationManager.AppSettings["QrUrlIpAddres"].ToString();
            UserInfo user = null;
            try
            {
                user = UserInfoRepository.GetUserByID(id);
                if (user != null)
                {
                    //Session["UserID"] = id;
                    var mapDetails = UserInfoRepository.GetUserMapDetails(user.UniqueID);
                    ViewBag.Latitude = mapDetails?.Latitude;
                    ViewBag.Longitude = mapDetails?.Longitude;

                    var userUrl = $"http://{Ip}/MafCode/UserInfo/ItemDetail?id={id}";
                    ViewBag.QRCodeImage = Utilities.GenerateQrCode(userUrl);

                    return View(user);
                }
            }
            catch (Exception e)
            {
                Utilities.WriteLogs(e.Message);

            }
            return RedirectToAction("Index", "Home");
        }



        // GET: UserInfo/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: UserInfo/Create
        //[HttpPost]
        //public ActionResult Create(UserInfo userInfo)
        //{
        //    try
        //    {
        //        UserInfoRepository.SaveUser(userInfo);
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserInfo/Edit/id
        public ActionResult Edit(string id)
        {
            if (System.Web.HttpContext.Current.Session["ID"] != null
                && System.Web.HttpContext.Current.Session["ID"].ToString().Equals(id, StringComparison.OrdinalIgnoreCase))
            {
                var user = UserInfoRepository.GetUserByID(id);
                var mapDetails = UserInfoRepository.GetUserMapDetails(id);
                ViewBag.Latitude = mapDetails?.Latitude;
                ViewBag.Longitude = mapDetails?.Longitude;
                return View(user);
            }
            else
            {
                return new HttpUnauthorizedResult();
            }
        }

        public ActionResult ItemDetail(string id)
        {
            var user = UserInfoRepository.GetItemDetails(id);
            var mapDetails = UserInfoRepository.GetUserMapDetails(user.UniqueID);
            ViewBag.Latitude = mapDetails?.Latitude;
            ViewBag.Longitude = mapDetails?.Longitude;
            return View(user);
            
        }

        public ActionResult AddItem(string id)
        {
            ViewBag.UniqueID = id;
            var items = UserInfoRepository.GetUserItems(id);
            return View(items);

        }
        public ActionResult UserQr(string id)
        {
            string Ip = ConfigurationManager.AppSettings["QrUrlIpAddres"].ToString();
            var userUrl = string.Format($"http://{Ip}/MafCode/UserInfo/ItemDetail?id={id}");
            ViewBag.QRCodeImage = Utilities.GenerateQrCode(userUrl);
            return View();

        }
        [HttpPost]
        public bool AddItem(string id,UserItem item)
        {
            var result = UserInfoRepository.SaveUserItem(item);
            Utilities.WriteLogs($"Save Item Result: {result}");
            return result;
        }

        //// POST: UserInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, UserInfo data)
        {
            try
            {
                if (System.Web.HttpContext.Current.Session["ID"].ToString().Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    UserInfoRepository.ModifyUserInfo(id, data);
                }
                else
                {
                    return new HttpUnauthorizedResult();
                }
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Details","UserInfo", id);
        }
    }
}
