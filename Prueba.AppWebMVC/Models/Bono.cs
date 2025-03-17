using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.AppWebMVC.Models
{
    public partial class Bono
    {
        public int Id { get; set; }

        public string NombreBono { get; set; } = null!;

        public decimal Valor { get; set; }

        public byte Estado { get; set; }

        public DateOnly? FechaValidacion { get; set; }

        public DateOnly? FechaExpiracion { get; set; }

        public byte Operacion { get; set; }

        public byte Planilla { get; set; }

        public virtual ICollection<AsignacionBono> AsignacionBonos { get; set; } = new List<AsignacionBono>();


        #region METODO PARA MOSTRAR EN TEXTO LAS OPCIONES BYTE
        //ESTO ES EXCLUSIVAMENTE PARA MOSTERAR EN EL INDEX
        [NotMapped]
        public string EstadoTexto
        {
            get
            {
                return Estado switch
                {
                    0 => "No definida",
                    1 => "Activo",
                    2 => "Inactivo",
                    _ => "Desconocido" 
                };
            }
        }

        [NotMapped]
        public string OperacionTexto
        {
            get
            {
                return Operacion switch
                {
                    0 => "No definida", 
                    1 => "Operación Fija",
                    2 => "Operación No Fija",
                    _ => "Desconocida" 
                };
            }
        }

        [NotMapped]
        public string PlanillaTexto
        {
            get
            {
                return Planilla switch
                {
                    0 => "No definida", 
                    1 => "Planilla Mensual",
                    2 => "Planilla Quincenal",
                    _ => "Desconocida" 
                };
            }
        }

        #endregion
    }
}
