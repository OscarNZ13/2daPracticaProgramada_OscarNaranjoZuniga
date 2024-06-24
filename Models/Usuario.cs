using System.ComponentModel.DataAnnotations.Schema;

namespace _2daPracticaProgramada_OscarNaranjoZuniga.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        public required string Nombre { get; set; }

        public required string Email { get; set; }

        public required string Contrasena { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaCreacion { get; set; }

        // Propiedad de navegación a Listas
        public required List<Lista> Listas { get; set; }

    }
}
