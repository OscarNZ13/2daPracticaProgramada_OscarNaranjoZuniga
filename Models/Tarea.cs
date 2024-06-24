using System.ComponentModel.DataAnnotations.Schema;

namespace _2daPracticaProgramada_OscarNaranjoZuniga.Models
{
    public class Tarea
    {
        public int TareaId { get; set; }

        public int ListaId { get; set; }

        public required string Titulo { get; set; }

        public required string Descripcion { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime FechaCreacion { get; set; }

        public bool Completada { get; set; }

        // Propiedad de navegación a Lista
        public required Lista Lista { get; set; }

    }
}
