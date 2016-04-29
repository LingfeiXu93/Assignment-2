using BookManageSystem.DAL;
using BookManageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManageSystem.Controllers
{
    public class WebUserController : Controller
    {

        UsersDAL userDAL = new UsersDAL();
        private static Users users;
        //
        // GET: /WebUser/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Regist()
        {
            return View();
        }
        public ActionResult Exit()
        {
            Session["Users"] = null;
            Session["UserName"] = null;
            Session["UserType"] = null;
            Session.RemoveAll();
            Response.Redirect("/WebUser/Login");
            return View();
        }

        public ActionResult UserInfoList()
        {
            List<Users> list = userDAL.GetEntityList();
            ViewData["userInfoList"] = list;
            return View();
        }

        public ActionResult ValidateCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult RegistWebUser(Users newInfo)
        {
            newInfo.UserType = "user";
            newInfo.CreateDate = DateTime.Now;
            if (userDAL.InsertEntityModel(newInfo) > 0)
            {
                return Content("ok:User Add Success!");
            }
            else
            {
                return Content("no:User Add fail");
            }
        }

        #region 
        public ActionResult DeleteUserInfo()
        {
            int id = int.Parse(Request["id"]);
            bool b = userDAL.DeleteEntityModel(id) > 0;
            if (b)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CheckLogin()
        {
            string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:ValidateCode  error");
            }
            Session["validateCode"] = null;
            string requestCode = Request["vCode"];
            if (!requestCode.Equals(validateCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:ValidateCode  error");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            string userType = Request["radUser"];

            Users userInfo = userDAL.GetUserInfoModel(userName, userPwd, userType);
            if (userInfo != null)
            {
                Session["Users"] = userInfo;
                Session["UserName"] = userInfo.UserName;
                Session["UserType"] = userInfo.UserType;
                ViewData.Model = userInfo;
                return Content("ok:" + userInfo.UserType);
            }
            else
            {
                return Content("no:UserName And Password Error");
            }

        }

        public ActionResult UserInfoEdit()
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                int id = int.Parse(Request["id"]);
                Users newInfo = userDAL.GetEntityModel(id);
                ViewData.Model = newInfo;
                ViewData["type"] = "update";
            }
            else
            {
                ViewData["type"] = "add";
            }
            return View();
        }

        #region 
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditUserInfo(Users newInfo)
        {
            if (userDAL.UpdateEntityModel(newInfo) > 0)
            {
                return Content("ok:User update Success");
            }
            else
            {
                return Content("no:User update fail");
            }
        }
        #endregion

    }
}