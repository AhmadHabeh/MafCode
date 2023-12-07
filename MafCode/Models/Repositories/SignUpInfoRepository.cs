using MafCode.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MafCode.Models.Repositories
{
    public class SignUpInfoRepository
    {
        public static bool SignUpUser(UserSignUpInfo user)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter{ ParameterName ="@UserName" , SqlDbType = SqlDbType.NVarChar , Value = user.Username },
                    new SqlParameter{ ParameterName ="@FirstName" , SqlDbType = SqlDbType.NVarChar , Value = user.FirstName},
                    new SqlParameter{ ParameterName ="@LastName" , SqlDbType = SqlDbType.NVarChar , Value = user.LastName},
                    new SqlParameter{ ParameterName ="@PhoneNumber" , SqlDbType = SqlDbType.NVarChar , Value = user.PhoneNumber},
                    new SqlParameter{ ParameterName ="@Email" , SqlDbType = SqlDbType.NVarChar , Value = user.Email},
                    new SqlParameter{ ParameterName ="@Password" , SqlDbType = SqlDbType.NVarChar , Value =Encryption.EncryptString( user.Password)},
                };
                var dbConnection = new DBConnection();
                return dbConnection.ExecuteNonQuery("SaveUserInfo", parameters);
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool Login(LoginModel loginInfo)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter{ ParameterName ="@UserName" , SqlDbType = SqlDbType.NVarChar , Value = loginInfo.Username },
                    new SqlParameter{ ParameterName ="@Password" , SqlDbType = SqlDbType.NVarChar , Value =Encryption.EncryptString( loginInfo.Password)},
                };
                var dbConnection = new DBConnection();
                return (dbConnection.ExecuteReaderAsDataTable("SP_Login", parameters,
                    CommandType.StoredProcedure).Rows.Count>0?true:false);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetUserIDByName(string username)
        {
            var userid = string.Empty;
            try
            {
                var parameters = new List<SqlParameter>
                {
                new SqlParameter{ ParameterName ="@UserName" , SqlDbType = SqlDbType.NVarChar , Value = username }
                };
                var dbConnection = new DBConnection();
                userid = dbConnection.ExecuteReaderAsDataTable("SP_SelectUserIdByName",
                    parameters, CommandType.StoredProcedure)
                   .Rows?[0]["UniqueID"].ToString();

            }
            catch (Exception e)
            {
                Utilities.WriteLogs("Exception in GetUserIDByName" + e);
            }
            return userid;
        }

    }
}