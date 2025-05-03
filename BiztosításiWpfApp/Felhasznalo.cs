using System;
using System.Collections.Generic;

namespace BiztositasKezelo;

public partial class Felhasznalo
{
    public int FelhasznaloId { get; set; }

    public string FelhNev { get; set; } = null!;

    public string Jelszo { get; set; } = null!;

    public int Jogosultsag { get; set; }

    public ICollection<Szemely> Szemely { get; set; } = new List<Szemely>();

    public virtual ICollection<Szerzodes> Szerzodes { get; set; } = new List<Szerzodes>();
}
