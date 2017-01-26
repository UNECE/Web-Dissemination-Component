using QS2015.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QS2015.Connectivity
{

    public class DataLink
    {
        // establish connection to the PC-Axis database
        static public SqlConnection getConnection()
        {
            SqlConnection myConnection = null;

            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["QuickStatsConnection"].ConnectionString;

                myConnection = new SqlConnection(connStr);
                myConnection.Open();
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return myConnection;

        }

        // get Id for the latest valid update
        static public string getLatestUpdate(SqlConnection myConnection)
        {
            string result = "";
            string sqlCode = "SELECT TOP 1 UpdateId FROM dbo.QuickStatsUpdates WHERE Disseminate='1' ORDER BY UpdateDate DESC";

            SqlDataReader myReader = null;

            try
            {
                if (myConnection != null)
                {
                    SqlCommand myCommand = new SqlCommand(sqlCode, myConnection);

                    myReader = myCommand.ExecuteReader();

                    if (myReader.Read() == true)
                    {
                        result = myReader["UpdateId"].ToString();
                    }

                    myReader.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


            return result;
        }
    }
}