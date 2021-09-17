using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney
{
    /// <summary>
    /// Деньги в формате целого числа
    /// </summary>
    public int CurrentMoney { get; set; }

    /// <summary>
    /// Деньги в формате десятичной дроби до сотых
    /// </summary>
    public double CurrentMoneyReal { get { return CurrentMoney / 100; } }

    public override string ToString()
    {
        return $"{CurrentMoneyReal.ToString("N2")}$";
    }

    /// <summary>
    /// Добавить деньги в формате целого числа
    /// </summary>
    public void AddMoney(int money)
    {
        CurrentMoney += money;
    }

    /// <summary>
    /// Добавить доллары
    /// </summary>
    public void AddDollars(int dollars)
    {
        CurrentMoney += dollars * 100;
    }

    /// <summary>
    /// Добавить центы
    /// </summary>
    public void AddCents(int cents)
    {
        CurrentMoney += cents;
    }

    /// <summary>
    /// Добавить деньги в формате десятичной дроби до сотых
    /// </summary>
    public void AddMoneyReal(double money)
    {
        CurrentMoney += (int)Math.Round(money * 100);
    }
}
