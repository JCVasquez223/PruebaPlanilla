using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.AppWebMVC.Models
{
    public partial class Planilla
    {
        public Planilla()
        {
            EmpleadoPlanillas = new HashSet<EmpleadoPlanilla>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la planilla es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        [Display(Name = "Nombre de la Planilla")]
        public string NombrePlanilla { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de planilla es obligatorio.")]
        [Display(Name = "Tipo de Planilla")]
        public int TipoPlanillaId { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [Display(Name = "Fecha de Inicio")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [Display(Name = "Fecha de Fin")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Autorización")]
        public byte Autorizacion { get; set; } = 0;

        [Required(ErrorMessage = "El total de pago es obligatorio.")]
        [Display(Name = "Total de Pago")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPago { get; set; }

        public virtual ICollection<EmpleadoPlanilla> EmpleadoPlanillas { get; set; }

        [ForeignKey("TipoPlanillaId")]
        public virtual TipoPlanilla? TipoPlanilla { get; set; }

        [NotMapped]
        [Display(Name = "Cantidad de Empleados")]
        public int CantidadEmpleados => EmpleadoPlanillas?.Count ?? 0;

        [NotMapped]
        [Display(Name = "Estado de Autorización")]
        public string EstadoAutorizacion => Autorizacion == 1 ? "Autorizada" : "No Autorizada";


        public bool EstaActiva()
        {
            var fechaActual = DateTime.UtcNow.Date;
            return fechaActual >= FechaInicio.Date && fechaActual <= FechaFin.Date;
        }
    }
}
