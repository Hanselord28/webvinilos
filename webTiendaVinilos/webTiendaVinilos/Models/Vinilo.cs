namespace webTiendaVinilos.Models
{
    public class Vinilo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Artista { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string Genero { get; set; }
    }
}
