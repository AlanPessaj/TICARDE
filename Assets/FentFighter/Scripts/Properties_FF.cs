using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties_FF : MonoBehaviour
{
    public float damage;
    public DamageType type;
}

public enum DamageType
{
    Punch,
    Kick,
    Ability,
    Ulti
}
