using System;
using System.Collections.Generic;

namespace ClubNauticoDBFirst.Models;

public partial class Socio
{
    public int IdSocio { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Barco> Barcos { get; set; } = new List<Barco>();
}
