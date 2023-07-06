using System;
using System.Collections.Generic;

namespace ClubNauticoDBFirst.Models;

public partial class Barco
{
    public int IdBarco { get; set; }

    public string Nombre { get; set; } = null!;

    public int NroAmarre { get; set; }

    public double Cuota { get; set; }

    public int NroMatricula { get; set; }

    public int IdSocio { get; set; }

    public virtual Socio IdSocioNavigation { get; set; } = null!;
}
