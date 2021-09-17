using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Address
{
    /// <summary>
    /// Улица
    /// </summary>
    public string Street { get; set; }

    /// <summary>
    /// Номер дома
    /// </summary>
    public int Building { get; set; }

    /// <summary>
    /// Номер квартиры, офиса
    /// </summary>
    public int Appartment { get; set; }

    /// <summary>
    /// Координаты на карте (пока не реализовано, подумать как сделать)
    /// </summary>
    public string Coords { get; set; }

    /// <summary>
    /// Полный адрес
    /// </summary>
    public string FullAddress { get { var res = Street; if (Building != 0) res += $", д.{Building}"; if (Appartment != 0) res += $", кв.{Appartment}"; return res; } }

    public Address()
    {
        Street = ""; Building = 0; Appartment = 0;
    }

    public Address(string street, int building, int appartment)
    {
        Street = street; Building = building; Appartment = appartment;
    }
}
