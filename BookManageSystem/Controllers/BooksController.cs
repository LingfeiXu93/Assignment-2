using BookManageSystem.DAL;
using BookManageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManageSystem.Controllers
{

    public class BooksController : Controller
    {
        BooksDAL booksDAL = new BooksDAL();
        private static Users users;
        //
        // GET: /Books/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookInfoEdit()
        {
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                int id = int.Parse(Request["id"]);
                Books bookInfo = booksDAL.GetEntityModel(id);
                ViewData.Model = bookInfo;
                ViewData["type"] = "update";
            }
            else
            {
                ViewData["type"] = "add";
            }
            return View();
        }

        public ActionResult BooksInfoList()
        {
            List<Books> list = booksDAL.GetEntityList();
            ViewData["booksInfoList"] = list;
            return View();
        }

        public ActionResult BooksInfoList_User()
        {
            if (Session["Users"] == null)
            {
                Response.Redirect("/WebUser/Login");
            }
            users = Session["Users"] as Users;
            ViewData.Model = users;


            List<Books> list = booksDAL.GetEntityList();
            ViewData["booksInfoList"] = list;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddBooks(Books booksInfo)
        {
            booksInfo.States = "can borrow book";
            booksInfo.CreateDate = DateTime.Now;
            if (booksDAL.InsertEntityModel(booksInfo) > 0)
            {
                return Content("ok:Book Add Success!");
            }
            else
            {
                return Content("no:Book Add fail");
            }
        }

        #region 
        public ActionResult DeleteBookInfo()
        {
            int id = int.Parse(Request["id"]);
            bool b = booksDAL.DeleteEntityModel(id) > 0;
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

        #region
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateBooks(Books newInfo)
        {
            if (booksDAL.UpdateEntityModel(newInfo) > 0)
            {
                return Content("ok:Book update Success");
            }
            else
            {
                return Content("no:Book update fail");
            }
        }
        #endregion

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateBookStates()
        {
            users = Session["Users"] as Users;
            int bookId = int.Parse(Request["id"]);
            Books book = new Books();
            book.ID = bookId;
            book.States = "book out on loan";
            if (booksDAL.UpdateBookStates(book) > 0)
            {
                Books bo = booksDAL.GetEntityModel(bookId);
                BorrowBookRecordDAL recordDAL = new BorrowBookRecordDAL();

                BorrowBookRecord record = new BorrowBookRecord();
                record.User_ID = users.ID;
                record.User_Name = users.UserName;
                record.Book_ID = int.Parse(Request["id"]);
                record.Book_Name = bo.BookName;
                record.BorrowBookDate = DateTime.Now;
                record.ReturnBookDate = DateTime.Parse("1970-1-1");
                record.States = "Borrow";
                record.CreateDate = DateTime.Now;

                if (recordDAL.InsertEntityModel(record) > 0)
                {
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
            else
            {
                return Content("no");
            }
        }
    }
}