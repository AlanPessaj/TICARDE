using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_FF : MonoBehaviour
{
    public float damage;
    public DamageType type;
    public GameObject owner;

    public System.Action<Damage_FF> disableAction;

    private void OnDestroy()
    {
        if (type == DamageType.Ability || type == DamageType.Ulti)
        {
            disableAction(this);
        }
    }
}

public enum DamageType
{
    Punch,
    Kick,
    UpperCut,
    Ability,
    Ulti
}
