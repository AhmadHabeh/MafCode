using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MafCode.Common
{

    public class DBConnection
    {
        private static string connectionString
            = ConfigurationManager.ConnectionStrings["BaseDataBase"].ConnectionString;
        public bool ExecuteNonQuery(string command, List<SqlParameter> parameters,
            CommandType commandType = CommandType.StoredProcedure)
        {
            var result = false;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var sqlCommand = connection.CreateCommand())
                    {
                        sqlCommand.Parameters.Clear();
                        sqlCommand.CommandText = command;
                        sqlCommand.CommandType = commandType;

                        parameters.ForEach(parameter =>
                        {
                            sqlCommand.Parameters.Add(parameter);
                        });
                        var test = sqlCommand.ExecuteNonQuery();
                        if (test >= 0)
                            result = true;
                        Utilities.WriteLogs($"Exceute NonQuery Result {test}");
                    }
                    CloseConnection(connection);
                }
            }
            catch (Exception e)
            {
                Utilities.WriteLogs($"Error in ExecuteNonQuery ex: {e}");
                result = false;
            }
            return result;
        }

        private void CloseConnection(SqlConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public DataTable ExecuteReaderAsDataTable(string query, List<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
        {
            var dataTable = new DataTable();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Clear();
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }
                        command.CommandType = commandType;
                        using (var dataAdapter = new SqlDataAdapter(command))
                        {
                            var result = dataAdapter.Fill(dataTable);
                            Utilities.WriteLogs($"Execute Reader Result {result}");
                            dataAdapter.Dispose();
                        }
                    }
                    CloseConnection(connection);
                }
            }
            catch (Exception e)
            {
                Utilities.WriteLogs($"ExecuteReaderAsDataTable Exception: {e}");
                dataTable = null;
            }
            return dataTable;
        }


    }
}