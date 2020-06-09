using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

/*
    Создать хранимую процедуру CLR
    в кот. выполн построение рез. набор сводных д-х (PIVOT) для табл
*/

public partial class StoredProcedures
{
    [SqlProcedure]
    public static string Pivot_()
    {

        SqlConnection conn = new SqlConnection("Context Connection=true");
        conn.Open();
        SqlCommand sqlCmd = conn.CreateCommand();

        string command = @"DECLARE @cols  AS NVARCHAR(MAX)= '';
                            DECLARE @query AS NVARCHAR(MAX)= '';
                            SELECT @cols = @cols + QUOTENAME(order_year) + ',' FROM(select distinct order_year from orders ) as tmp
                            select @cols = substring(@cols, 0, len(@cols))
                            print @cols



                            set @query = 'select * from orders 
                            pivot" +                 ///формир пивот-таблицы
                            "(sum(summ)" +           ///агрег ф-я, формир содержимое
                            "for order_year" +       ///значения столбца будут заголовками столб
                            "in (' + @cols + '))" +  ///конкр знач order_year, исп-е в кач заголовков
                            "piv '" +                ///алиас для сводной таблицы

                            "execute(@query)";

        sqlCmd.CommandText = command;

        string res = "";

        using (var reader = sqlCmd.ExecuteReader())
        {
            while (reader.Read())
            {
                res += reader[0];
                res += "\n";
            }
        }
        
        conn.Close();
        return res;
    }
}