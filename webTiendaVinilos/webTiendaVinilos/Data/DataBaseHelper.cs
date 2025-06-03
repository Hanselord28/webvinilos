using webTiendaVinilos.Models;
using Microsoft.Data.SqlClient;
namespace webTiendaVinilos.Data

{
    public class DataBaseHelper
    {
        private readonly string _connectionString;

        public DataBaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("VinilosDB");
        }

        // Método para insertar un vinilo
        public void InsertVinilo(string nombre, string artista, decimal precio, int stock, DateTime fechaLanzamiento, string genero)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Vinilos (Nombre, Artista, Precio, Stock, FechaLanzamiento, Genero) VALUES (@nombre, @artista, @precio, @stock, @fecha, @genero)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@artista", artista);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.Parameters.AddWithValue("@fecha", fechaLanzamiento);
                    cmd.Parameters.AddWithValue("@genero", genero);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener los vinilos de la BD
        public List<Vinilo> GetVinilos()
        {
            List<Vinilo> lista = new List<Vinilo>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Vinilos";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Vinilo
                            {
                                Id = (int)reader["Id"],
                                Nombre = reader["Nombre"].ToString(),
                                Artista = reader["Artista"].ToString(),
                                Precio = (decimal)reader["Precio"],
                                Stock = (int)reader["Stock"],
                                FechaLanzamiento = reader["FechaLanzamiento"] != DBNull.Value ? (DateTime)reader["FechaLanzamiento"] : default,
                                Genero = reader["Genero"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}