using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.AppWebMVC.Models
{
    public partial class TipoPlanilla
    {
        public TipoPlanilla()
        {
            Planillas = new HashSet<Planilla>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del tipo de planilla es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        [Display(Name = "Nombre del Tipo de Planilla")]
        public string NombreTipo { get; set; } = null!;

        public virtual ICollection<Planilla> Planillas { get; set; }


        [NotMapped]
        public bool Activo { get; set; } = true;


        public DateTime FechaCreacion { get; set; } = DateTime.Now;


        public DateTime? FechaModificacion { get; set; }
    }
}
