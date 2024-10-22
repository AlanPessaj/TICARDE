using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage_FF : MonoBehaviour
{
    public float damage;
    public DamageType type;
    public GameObject owner;

    public System.Action<Damage_FF> disableAction;

    private void Update()
    {
        if (((transform.position.x > owner.GetComponent<PlayerController_FF>().otherPlayer.transform.position.x + 1 && transform.eulerAngles.y == 0) || (transform.position.x < owner.GetComponent<PlayerController_FF>().otherPlayer.transform.position.x - 1 && transform.eulerAngles.y != 0)) && disableAction != null)
        {
            disableAction(this);
        }
    }

    private void OnDestroy()
    {
        if ((type == DamageType.Ability || type == DamageType.Ulti) && disableAction != null)
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
    SlideKick,
    Ability,
    Ulti
}
