using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseFact : CaseElementBase
{
    public CaseFact() : base()
    {
        IsNeedToBeProven = false;
    }

    public CaseFact(string name, string text, bool isSecret = false) : base(name, text, isSecret)
    {
        IsNeedToBeProven = false;
    }
}
