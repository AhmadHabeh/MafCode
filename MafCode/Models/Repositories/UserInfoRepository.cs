using MafCode.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MafCode.Models.Repositories
{
    public class UserInfoRepository
    {
        public static UserMapDetails GetUserMapDetails(string id)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName ="@ID", SqlDbType = SqlDbType.NVarChar,Value  =id }
                };

                var dbConnection = new DBConnection();
                var userDetails = dbConnection.ExecuteReaderAsDataTable("SP_SelectMapDetailByID", parameters, CommandType.StoredProcedure)
                    .ToList<UserMapDetails>().FirstOrDefault();

                return userDetails;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static UserInfo GetItemDetails(string id)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName ="@ItemID", SqlDbType = SqlDbType.NVarChar,Value  =id }
                };

                var dbConnection = new DBConnection();
                var userDetails = dbConnection.ExecuteReaderAsDataTable("SP_GetItemByID", parameters, CommandType.StoredProcedure)
                    .ToList<UserInfo>().FirstOrDefault();

                return userDetails;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static void SaveUser(UserInfo user)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter{ ParameterName ="@UserName" , SqlDbType = SqlDbType.NVarChar , Value = user.Username },
                new SqlParameter{ ParameterName ="@FirstName" , SqlDbType = SqlDbType.NVarChar , Value = user.FirstName},
                new SqlParameter{ ParameterName ="@LastName" , SqlDbType = SqlDbType.NVarChar , Value = user.LastName},
                new SqlParameter{ ParameterName ="@PhoneNumber" , SqlDbType = SqlDbType.NVarChar , Value = user.PhoneNumber},
                new SqlParameter{ ParameterName ="@Email" , SqlDbType = SqlDbType.NVarChar , Value = user.Email},
                new SqlParameter{ ParameterName ="@ProfileImage" , SqlDbType = SqlDbType.NVarChar , Value = user.ProfileImage},
            };
            var dbConnection = new DBConnection();
            dbConnection.ExecuteNonQuery("SaveUserInfo", parameters);

        }
        //ModifyUserInfo
        public static void ModifyUserInfo(string ID, UserInfo user)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter{ ParameterName ="@UserName" , SqlDbType = SqlDbType.NVarChar , Value = user.Username },
                new SqlParameter{ ParameterName ="@FirstName" , SqlDbType = SqlDbType.NVarChar , Value = user.FirstName},
                new SqlParameter{ ParameterName ="@LastName" , SqlDbType = SqlDbType.NVarChar , Value = user.LastName},
                new SqlParameter{ ParameterName ="@PhoneNumber" , SqlDbType = SqlDbType.NVarChar , Value = user.PhoneNumber},
                new SqlParameter{ ParameterName ="@Email" , SqlDbType = SqlDbType.NVarChar , Value = user.Email},
                new SqlParameter{ ParameterName ="@ProfileImage" , SqlDbType = SqlDbType.NVarChar , Value = user.ProfileImage+""},
                new SqlParameter{ ParameterName ="@UniqueID" ,SqlDbType = SqlDbType.NVarChar , Value = ID },
                new SqlParameter{ ParameterName ="@Latitude" ,SqlDbType = SqlDbType.NVarChar , Value = user.mapDetails.Latitude },
                new SqlParameter{ ParameterName ="@Longitude" ,SqlDbType = SqlDbType.NVarChar , Value = user.mapDetails.Longitude },

            };
            var dbConnection = new DBConnection();
            dbConnection.ExecuteNonQuery("ModifyUserInfo", parameters);

        }
        public static UserInfo GetUserByID(string ID)
        {
            UserInfo user = null;
            try
            {
                var parameters = new List<SqlParameter>
                {
                new SqlParameter{ ParameterName ="@ID" , SqlDbType = SqlDbType.NVarChar , Value = ID }
                };
                var dbConnection = new DBConnection();
                user = dbConnection.ExecuteReaderAsDataTable("SP_SelectUserByID", parameters, CommandType.StoredProcedure)
                    .ToList<UserInfo>().FirstOrDefault();

            }
            catch (Exception e)
            {
                Utilities.WriteLogs("Exception in GetUserByID" + e);
            }
            return user;

        }
        public static List<UserInfo> GetUserAllUsers()
        {
            List<UserInfo> users;
            try
            {
                var dbConnection = new DBConnection();
                users = dbConnection.ExecuteReaderAsDataTable("SP_SelectAllUsers", null, CommandType.StoredProcedure)
                    .ToList<UserInfo>();

            }
            catch (Exception e)
            {
                Utilities.WriteLogs("Exception in GetUserAllUsers" + e);
                return new List<UserInfo>();
            }
            return users;
        }


        public static List<UserItem> GetUserItems(string UserID)
        {
            List<UserItem> userItems = null;
            try
            {
                var dbConnection = new DBConnection();
                var parameters = new List<SqlParameter>
                {
                new SqlParameter{ ParameterName ="@UserID" , SqlDbType = SqlDbType.NVarChar , Value = UserID }
                };
                userItems = dbConnection.ExecuteReaderAsDataTable("GetUserItems",
                    parameters, CommandType.StoredProcedure).ToList<UserItem>();
            }
            catch (Exception)
            {
                userItems = null;
            }
            return userItems;
        }
        public static bool SaveUserItem(UserItem item)
        {
            var result = false;
            try
            {
                var parameters = new List<SqlParameter>
            {
                new SqlParameter{ ParameterName ="@UserID" , SqlDbType = SqlDbType.NVarChar , Value = item.UniqueID },
                new SqlParameter{ ParameterName ="@ItemName" , SqlDbType = SqlDbType.NVarChar , Value = item.ItemName},
                new SqlParameter{ ParameterName ="@ItemDescription" , SqlDbType = SqlDbType.NVarChar , Value = item.ItemDescription},
            };
                var dbConnection = new DBConnection();
                result = dbConnection.ExecuteNonQuery("SaveUserItems", parameters);
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
    
}