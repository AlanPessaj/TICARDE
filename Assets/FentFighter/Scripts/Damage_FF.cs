using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_FF : MonoBehaviour
{
    public float damage;
    public DamageType type;
    public GameObject owner;

    public System.Action disableAction;
}

public enum DamageType
{
    Punch,
    Kick,
    Ability,
    Ulti
}
