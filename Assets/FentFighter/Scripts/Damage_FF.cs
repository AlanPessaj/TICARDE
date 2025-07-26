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
        if ((type == DamageType.Ability || type == DamageType.Ulti) && ((transform.position.x > owner.GetComponent<PlayerController_FF>().otherPlayer.transform.position.x + 1 && transform.eulerAngles.y == 0) || (transform.position.x < owner.GetComponent<PlayerController_FF>().otherPlayer.transform.position.x - 1 && transform.eulerAngles.y != 0)) && disableAction != null)
        {
            disableAction(this);
            disableAction = null;
        }
    }

    private void OnDestroy()
    {
        if ((type == DamageType.Ability || type == DamageType.Ulti) && disableAction != null)
        {
            disableAction(this);
            disableAction = null;
        }
    }
}

public enum DamageType
{
    Punch,
    UpperCut,
    Smash,
    Kick,
    SlideKick,
    Ability,
    Ulti
}
