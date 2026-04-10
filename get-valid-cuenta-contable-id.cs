using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        var connStr = "Data Source=tcp:lamaregionnorte-sql-a90e.database.windows.net,1433;Initial Catalog=LAMAMedellinContable;Integrated Security=false;User ID=Administrador@lamaregionnorte-sql-a90e;Password=Mc901128365-2;Connect Timeout=10;";

        try
        {
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TOP 1 Id, Codigo FROM CuentasContables WHERE Codigo = '110505';";
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine(reader["Id"].ToString());
                        }
                        else
                        {
                            Console.WriteLine("");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("");
        }
    }
}
