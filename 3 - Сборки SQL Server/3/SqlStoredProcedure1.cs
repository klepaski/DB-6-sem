using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

/*
    Создать хранимую процедуру CLR с 2 параметрами типа datetime,
*/

public partial class StoredProcedures
{
    [SqlProcedure]
    public static int GetCount(SqlDateTime dateStart, SqlDateTime dateEnd)
    {
        int rows;
        SqlConnection conn = new SqlConnection("Context Connection=true");
        conn.Open();
        SqlCommand sqlCmd = conn.CreateCommand();

        sqlCmd.CommandText = @"select count(*) from Orders where order_date between @dateStart and @dateEnd";

        sqlCmd.Parameters.Add("@dateStart", dateStart);
        sqlCmd.Parameters.Add("@dateEnd", dateEnd);

        rows = (int)sqlCmd.ExecuteScalar();
        conn.Close();
        return rows;
    }
}