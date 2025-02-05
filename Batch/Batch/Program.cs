using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.IO;
class Program
{
    static void Main()
    {
        // Cadena de conexión a Oracle (ajusta según tu configuración)
        string connectionString = "Data Source=localhost:1521/xe; User Id=C##vinculosestrategicos;Password=Vinculo@2025;";

        // Consulta SQL que deseas ejecutar
        string query = "SELECT dui, nombre, fecha_nacimiento, generacion FROM visitantes";

        string logFilePath = @"C:\Users\dell\OneDrive\Documentos\8 - VinculosEstrategicos\EjecucionBatch.log"; // Ruta del archivo de log
        DateTime fechaEjecucion = DateTime.Now;
        string estado = "Exitoso";
        try
        {
            // Crear la conexión
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Conexión establecida con Oracle.");

                // Crear el comando SQL
                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        // Leer los resultados
                        while (reader.Read())
                        {
                            Console.WriteLine($"dui: {reader["dui"]}, Nombre: {reader["nombre"]}, fecha_nacimiento: {reader["fecha_nacimiento"]}, generacion: {reader["generacion"]}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        // Registrar la ejecución en el log
        EscribirLog(logFilePath, fechaEjecucion, estado);
    }
    static void EscribirLog(string filePath, DateTime fecha, string estado)
    {
        try
        {
            string logMessage = $"[{fecha:yyyy-MM-dd HH:mm:ss}] - Estado: {estado}";
            File.AppendAllText(filePath, logMessage + Environment.NewLine);
            Console.WriteLine("Log guardado en archivo.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error escribiendo el log: {ex.Message}");
        }
    }
}