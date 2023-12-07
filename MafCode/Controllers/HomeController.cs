using MafCode.Common;
using MafCode.Models;
using MafCode.Models.Repositories;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MafCode.Controllers
{
    public class HomeController : Controller
    {
        //home/HowItWorks
        public ActionResult HowItWorks()
        {
            ViewBag.Title = "About";
            return View();
        }


        //home/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        //[HttpPost]
        //public ActionResult Login(LoginModel login)
        //{
        //    var result = SignUpInfoRepository.Login(login);
        //    if (result)
        //    {
        //        System.Web.HttpContext.Current.Session["Username"] = login.Username;
        //        System.Web.HttpContext.Current.Session["ID"] = SignUpInfoRepository.GetUserIDByName(login.Username);
        //    }
        //    if (result)
        //    {

        //    } else
        //    {
                
        //    }
        //    return result;
        //    return View();
        //}

        public ActionResult LogOut()
        {
            try
            {

                System.Web.HttpContext.Current.Session["Username"] = null;
                System.Web.HttpContext.Current.Session["ID"] = null;

            }
            catch (Exception e)
            {
                Utilities.WriteLogs("Logout Exception: "+e.Message);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public bool Login(LoginModel login)
        {
            var result = SignUpInfoRepository.Login(login);
            if (result)
            {
                System.Web.HttpContext.Current.Session["Username"] = login.Username;
                System.Web.HttpContext.Current.Session["ID"] = SignUpInfoRepository.GetUserIDByName(login.Username);
            }
            return result;
        }
        //home/Signup
        public ActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public bool Signup(UserSignUpInfo signUpInfo)
        {
            var result = SignUpInfoRepository.SignUpUser(signUpInfo);
            if (result)
            {
                System.Web.HttpContext.Current.Session["Username"] = signUpInfo.Username;
                System.Web.HttpContext.Current.Session["ID"] = SignUpInfoRepository.GetUserIDByName(signUpInfo.Username);
            }
            return result;
        }

        public ActionResult Index()
        {
            
            return View();
        }


        public string UserID(string username)
        {
            var result = SignUpInfoRepository.GetUserIDByName(username);
            return result;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
            
        }
       

    }
}