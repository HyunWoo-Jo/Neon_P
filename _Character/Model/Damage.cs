using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Damage
{
    public int bufferDamage;
    public bool bufferIsHit;

    public Damage(int bufferDamage, bool bufferIsHit)
    {
        this.bufferDamage = bufferDamage;
        this.bufferIsHit = bufferIsHit;
    }
}
