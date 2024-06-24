using System.ComponentModel.DataAnnotations.Schema;

namespace _2daPracticaProgramada_OscarNaranjoZuniga.Models
{
    public class Lista
    {
        public int ListaId { get; set; }

        public int UsuarioId { get; set; }

        public required string Titulo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaCreacion { get; set; }

        // Propiedad de navegación a Usuario
        public required Usuario Usuario { get; set; }

        // Propiedad de navegación a Tareas
        public required List<Tarea> Tareas { get; set; }

    }
}
