using BookManageSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookManageSystem.DAL
{
    public class UsersDAL
    {
        public Users GetUserInfoModel(string userName, string userPwd, string userType)
        {
            string sql = "select * from Users where UserType='" + userType + "' and UserName=@UserName and Password=@Password";
            SqlParameter[] pars = { 
                                 new SqlParameter("@UserName",SqlDbType.NVarChar,32),
                                 new SqlParameter("@Password",SqlDbType.NVarChar,32)
                                 };
            pars[0].Value = userName;
            pars[1].Value = userPwd;
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            Users userInfo = null;
            if (da.Rows.Count > 0)
            {
                userInfo = new Users();
                LoadEntity(da.Rows[0], userInfo);
            }
            return userInfo;
        }
        public void LoadEntity(DataRow row, Users userInfo)
        {
            userInfo.ID = Convert.ToInt32(row["ID"]);
            userInfo.UserName = row["UserName"] != DBNull.Value ? row["UserName"].ToString() : string.Empty;
            userInfo.Password = row["Password"] != DBNull.Value ? row["Password"].ToString() : string.Empty;
            userInfo.Age = Convert.ToInt32(row["Age"]);
            userInfo.Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : string.Empty;
            userInfo.UserType = row["UserType"] != DBNull.Value ? row["UserType"].ToString() : string.Empty;
            userInfo.CreateDate = Convert.ToDateTime(row["CreateDate"]);

        }


        public List<Users> GetEntityList()
        {
            string sql = "select * from Users where UserType!='admin'";
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text);
            List<Users> list = null;
            if (da.Rows.Count > 0)
            {
                list = new List<Users>();
                Users newInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    newInfo = new Users();
                    LoadEntity(row, newInfo);
                    list.Add(newInfo);
                }
            }
            return list;
        }

        public List<Users> GetPageEntityList(int start, int end)
        {
            string sql = "select * from (select row_number() over(order by id) as num, * from Users where UserType in(0,1)) as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars = { 
                                   new SqlParameter("@start",start),
                                   new SqlParameter("@end",end)
                                  };
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, pars);
            List<Users> newInfoList = null;
            if (da.Rows.Count > 0)
            {
                newInfoList = new List<Users>();
                Users newInfo = null;
                foreach (DataRow row in da.Rows)
                {
                    newInfo = new Users();
                    LoadEntity(row, newInfo);
                    newInfoList.Add(newInfo);
                }
            }
            return newInfoList;
        }

        public Users GetEntityModel(int id)
        {
            string sql = "select * from Users where ID=@Id";
            DataTable da = SqlHelper.GetTable(sql, CommandType.Text, new SqlParameter("@Id", id));
            Users newInfo = null;
            if (da.Rows.Count > 0)
            {
                newInfo = new Users();
                LoadEntity(da.Rows[0], newInfo);
            }
            return newInfo;
        }

        public int DeleteEntityModel(int id)
        {
            string sql = "delete from Users where ID=@Id";
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@Id", id));
        }
        public int InsertEntityModel(Users newInfo)
        {
            string sql = "insert into Users(UserName,Password,Age,Phone,UserType,CreateDate) values(@UserName,@Password,@Age,@Phone,@UserType,@CreateDate)";
            SqlParameter[] pars = { 
                                 new SqlParameter("@UserName",SqlDbType.NVarChar),
                                 new SqlParameter("@Password",SqlDbType.NVarChar),
                                 new SqlParameter("@Age",SqlDbType.Int),
                                 new SqlParameter("@Phone",SqlDbType.NVarChar),
                                 new SqlParameter("@UserType",SqlDbType.NVarChar),
                                 new SqlParameter("@CreateDate",SqlDbType.DateTime)
                                 };
            pars[0].Value = newInfo.UserName;
            pars[1].Value = newInfo.Password;
            pars[2].Value = newInfo.Age;
            pars[3].Value = newInfo.Phone;
            pars[4].Value = newInfo.UserType;
            pars[5].Value = newInfo.CreateDate;
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pars);
        }
        public int UpdateEntityModel(Users model)
        {
            string sql = "update Users set UserName=@UserName,Password=@Password,Age=@Age,Phone=@Phone where ID=@ID";
            SqlParameter[] parameters = { 
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
                    new SqlParameter("@Age", SqlDbType.Int),
                    new SqlParameter("@Phone", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.UserName;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.Age;
            parameters[3].Value = model.Phone;
            parameters[4].Value = model.ID;
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parameters);
        }

        public int GetRecordCount()
        {
            string sql = "select count(*) from Users";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(sql, CommandType.Text));
            return count;
        }
    }
}