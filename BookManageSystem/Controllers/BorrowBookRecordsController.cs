using BookManageSystem.DAL;
using BookManageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManageSystem.Controllers
{
    public class BorrowBookRecordsController : Controller
    {
        BooksDAL booksDAL = new BooksDAL();
        BorrowBookRecordDAL recordDAL = new BorrowBookRecordDAL();
        Users users;
        //
        // GET: /BorrowBookRecords/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult BorrowBookRecordInfoList()
        {
            users = Session["Users"] as Users;
            List<BorrowBookRecord> list = recordDAL.GetEntityList(" where User_ID=" + users.ID);
            ViewData["bookRecordList"] = list;
            return View();
        }

        public ActionResult BorrowBookRecordInfoList_Admin()
        {
            users = Session["Users"] as Users;
            List<BorrowBookRecord> list = recordDAL.GetEntityList("");
            ViewData["bookRecordList"] = list;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateBookStates()
        {
            users = Session["Users"] as Users;
            int id = int.Parse(Request["id"]);
            BorrowBookRecord recordModel = recordDAL.GetEntityModel(id);

            Books book = new Books();
            book.ID = recordModel.Book_ID.Value;
            book.States = "can borrow book";
            if (booksDAL.UpdateBookStates(book) > 0)
            {
                BorrowBookRecord record = new BorrowBookRecord();
                record.ID = id;
                record.ReturnBookDate = DateTime.Now;
                record.States = "return";

                if (recordDAL.UpdateEntityModel(record) > 0)
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