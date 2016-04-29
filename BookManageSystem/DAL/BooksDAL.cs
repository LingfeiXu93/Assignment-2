using BookManageSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookManageSystem.DAL
{
    public class BooksDAL
    {
        public Books GetUserInfoModel(int id)
        {
            string sql = "select * from Books where ID=@id";
            SqlParameter[] pars = { 
                                 new SqlParameter("@id",SqlDbType.Int)
                                 };
            pars[0].Value = id;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            Books userInfo = null;
            if (da.Rows.Count > 0)
            {
                userInfo = new Books();
                LoadEntity(da.Rows[0], userInfo);
            }
            return userInfo;
        }
        public void LoadEntity(DataRow row, Books userInfo)
        {
            userInfo.ID = Convert.ToInt32(row["ID"]);
            userInfo.BookName = row["BookName"] != DBNull.Value ? row["BookName"].ToString() : string.Empty;
            userInfo.States = row["States"] != DBNull.Value ? row["States"].ToString() : string.Empty;
            userInfo.CreateDate = Convert.ToDateTime(row["CreateDate"]);
        }


        public List<Books> GetEntityList()
        {
            string sql = "select * from Books";
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text);
            List<Books> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<Books>();
                Books newInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    newInfo = new Books();
                    LoadEntity(row, newInfo);
                    list.Add(newInfo);
                }
            }
            return list;
        }

        public Books GetEntityModel(int id)
        {
            string sql = "select * from Books where ID=@Id";
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, new SqlParameter("@Id", id));
            Books newInfo = null;
            if (da.Rows.Count > 0)
            {
                newInfo = new Books();
                LoadEntity(da.Rows[0], newInfo);
            }
            return newInfo;
        }

        public int DeleteEntityModel(int id)
        {
            string sql = "delete from Books where ID=@Id";
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@Id", id));
        }
        public int InsertEntityModel(Books model)
        {
            string sql = "insert into Books(BookName,States,CreateDate) values(@BookName,@States,@CreateDate)";
            SqlParameter[] parameters = {
                    new SqlParameter("@BookName", SqlDbType.NVarChar,50),
					new SqlParameter("@States", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime)};
            parameters[0].Value = model.BookName;
            parameters[1].Value = model.States;
            parameters[2].Value = model.CreateDate;
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parameters);
        }
        public int UpdateEntityModel(Books model)
        {
            string sql = "update Books set BookName=@BookName where ID=@ID";
            SqlParameter[] parameters = {
				    new SqlParameter("@BookName", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.BookName;
            parameters[1].Value = model.ID;
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parameters);
        }

        public int UpdateBookStates(Books model)
        {
            string sql = "update Books set States=@States where ID=@ID";
            SqlParameter[] parameters = {
				    new SqlParameter("@States", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.States;
            parameters[1].Value = model.ID;
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parameters);
        }
    }
}