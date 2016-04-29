using BookManageSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookManageSystem.DAL
{
    public class BorrowBookRecordDAL
    {
        public BorrowBookRecord GetUserInfoModel(int id)
        {
            string sql = "select * from BorrowBookRecord where ID=@id";
            SqlParameter[] pars = { 
                                 new SqlParameter("@id",SqlDbType.Int)
                                 };
            pars[0].Value = id;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            BorrowBookRecord userInfo = null;
            if (da.Rows.Count > 0)
            {
                userInfo = new BorrowBookRecord();
                LoadEntity(da.Rows[0], userInfo);
            }
            return userInfo;
        }
        public void LoadEntity(DataRow row, BorrowBookRecord userInfo)
        {
            userInfo.ID = Convert.ToInt32(row["ID"]);
            userInfo.User_ID = Convert.ToInt32(row["User_ID"]);
            userInfo.User_Name = row["User_Name"] != DBNull.Value ? row["User_Name"].ToString() : string.Empty;
            userInfo.Book_ID = Convert.ToInt32(row["Book_ID"]);
            userInfo.Book_Name = row["Book_Name"] != DBNull.Value ? row["Book_Name"].ToString() : string.Empty;
            userInfo.BorrowBookDate = Convert.ToDateTime(row["BorrowBookDate"]);
            userInfo.ReturnBookDate = Convert.ToDateTime(row["ReturnBookDate"]);
            userInfo.States = row["States"] != DBNull.Value ? row["States"].ToString() : string.Empty;
            userInfo.CreateDate = Convert.ToDateTime(row["CreateDate"]);
        }


        public List<BorrowBookRecord> GetEntityList(string wh)
        {
            string sql = "select * from BorrowBookRecord "+wh;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text);
            List<BorrowBookRecord> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<BorrowBookRecord>();
                BorrowBookRecord newInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    newInfo = new BorrowBookRecord();
                    LoadEntity(row, newInfo);
                    list.Add(newInfo);
                }
            }
            return list;
        }

        public BorrowBookRecord GetEntityModel(int id)
        {
            string sql = "select * from BorrowBookRecord where ID=@Id";
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, new SqlParameter("@Id", id));
            BorrowBookRecord newInfo = null;
            if (da.Rows.Count > 0)
            {
                newInfo = new BorrowBookRecord();
                LoadEntity(da.Rows[0], newInfo);
            }
            return newInfo;
        }

        public int DeleteEntityModel(int id)
        {
            string sql = "delete from BorrowBookRecord where ID=@Id";
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@Id", id));
        }
        public int InsertEntityModel(BorrowBookRecord model)
        {
            string sql = "insert into BorrowBookRecord(User_ID,User_Name,Book_ID,Book_Name,BorrowBookDate,ReturnBookDate,States,CreateDate) values(@User_ID,@User_Name,@Book_ID,@Book_Name,@BorrowBookDate,@ReturnBookDate,@States,@CreateDate)";
            SqlParameter[] parameters = {
                    new SqlParameter("@User_ID", SqlDbType.Int),
                    new SqlParameter("@User_Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Book_ID", SqlDbType.Int),
                    new SqlParameter("@Book_Name", SqlDbType.NVarChar,50),
					new SqlParameter("@BorrowBookDate", SqlDbType.DateTime),
                    new SqlParameter("@ReturnBookDate", SqlDbType.DateTime),
                    new SqlParameter("@States", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
            parameters[0].Value = model.User_ID;
            parameters[1].Value = model.User_Name;
            parameters[2].Value = model.Book_ID;
            parameters[3].Value = model.Book_Name;
            parameters[4].Value = model.BorrowBookDate;
            parameters[5].Value = model.ReturnBookDate;
            parameters[6].Value = model.States;
            parameters[7].Value = model.CreateDate;
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parameters);
        }
        public int UpdateEntityModel(BorrowBookRecord model)
        {
            string sql = "update BorrowBookRecord set ReturnBookDate=@ReturnBookDate,States=@States where ID=@ID";
            SqlParameter[] parameters = {
                    new SqlParameter("@ReturnBookDate", SqlDbType.DateTime),
				    new SqlParameter("@States", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int)};
            parameters[0].Value = model.ReturnBookDate;
            parameters[1].Value = model.States;
            parameters[2].Value = model.ID;
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parameters);
        }
    }
}