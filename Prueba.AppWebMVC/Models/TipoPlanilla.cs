using System;
using System.Collections.Generic;

namespace Prueba.AppWebMVC.Models;

public partial class TipoPlanilla
{
    public int Id { get; set; }

    public string NombreTipo { get; set; } = null!;

    public virtual ICollection<Planilla> Planillas { get; set; } = new List<Planilla>();
}
