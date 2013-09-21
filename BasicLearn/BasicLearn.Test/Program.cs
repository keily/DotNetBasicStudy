using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace BasicLearn.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            string str = @"
DECLARE @TEMP VARCHAR(100);
SET @TEMP='Confections';
SELECT CategoryID,CategoryName 
FROM dbo.Categories
WHERE CategoryID>@CategoryID AND CategoryName=@TEMP;";
            string sqlConn = @"Data Source=keily\sqlexpress;Initial Catalog=Northwind;Integrated Security=True";
            DataSet ds;
            SqlConnection conn = new SqlConnection(sqlConn);
            using (SqlCommand command = new SqlCommand(str, conn))
            {
                command.CommandTimeout = 15;
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@CategoryID", SqlDbType.Int).Value=1;
                conn.Open();
                SqlDataAdapter adt = new SqlDataAdapter(command);
                ds = new DataSet();
                adt.Fill(ds);
            }
            conn.Close();

            str = @"
DECLARE @id int,@cname VARCHAR(100);
SELECT @id=CategoryID,@cname=CategoryName 
FROM dbo.Categories
WHERE CategoryID=@CategoryID;
update dbo.Categories
set CategoryName=@cname+'es'
WHERE CategoryID=@id;
";
            using (SqlCommand command = new SqlCommand(str, conn))
            {
                command.CommandTimeout = 15;
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@CategoryID", SqlDbType.Int).Value = 1;
                conn.Open();
                var ret = command.ExecuteNonQuery();
                Console.WriteLine(ret);
            }
            conn.Close();

            Console.WriteLine("");
        }

    }
}
