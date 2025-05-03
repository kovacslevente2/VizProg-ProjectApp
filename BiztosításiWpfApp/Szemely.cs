using System;
using System.Collections.Generic;

namespace BiztositasKezelo;

public partial class Szemely
{
    public int SzemelyId { get; set; }

    public int FelhId { get; set; }

    public string Veznev { get; set; } = null!;

    public string Utonev { get; set; } = null!;

    public DateOnly SzulDatum { get; set; }

    public string Varos { get; set; } = null!;

    public Felhasznalo Felh { get; set; } = null!;
}
