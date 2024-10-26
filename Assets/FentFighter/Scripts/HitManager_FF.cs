using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager_FF : MonoBehaviour
{
    public Collider[] hColliders = new Collider[3];
    public Damage_FF damageProperties = null;
    public Animator animator;
    public bool blocking;
    public float XPMultiplier;
    public float smashForce;
    public float knockbackForce;
    bool detectedHit;
    bool detectedBlock;
    bool detectedAbility;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hColliders.Length; i++)
        {
            bool collided = false;
            if (damageProperties == null) break;
            if (damageProperties.owner == gameObject) break;
            if (hColliders[i] == null) continue;
            for (int o = i + 1; o < hColliders.Length; o++)
            {
                if (hColliders[o] == null) continue;
                if (i == 0)
                {
                    if (o == 1)
                    {
                        if (hColliders[o + 1] != null)
                        {
                            if (blocking)
                            {
                                //Debug.Log("pego en piernas (block pecho cabeza)");
                                TakeDamage();
                            }
                            else
                            {
                                //Debug.Log("pego en cabeza, pecho y piernas");
                                TakeDamage();
                            }
                            collided = true;
                            break;
                        }
                        else
                        {
                            if (blocking)
                            {
                                //Debug.Log("(block cabeza pecho)");
                                if (!detectedBlock)
                                {
                                    if (damageProperties.type == DamageType.Ulti || damageProperties.type == DamageType.Ability)
                                    {
                                        TakeDamage();
                                    }
                                    else
                                    {
                                        GetComponent<UIManager_FF>().AddXP(damageProperties.damage);
                                        detectedBlock = true;
                                        damageProperties.disableAction = ResetDetection;
                                    }
                                }
                            }
                            else
                            {
                                //Debug.Log("pego en cabeza y pecho");
                                TakeDamage();
                            }
                            collided = true;
                        }
                    }
                    else
                    {
                        if (blocking)
                        {
                            //Debug.Log("pego en piernas (block cabeza)");
                            TakeDamage();
                        }
                        else
                        {
                            //Debug.Log("pego en cabeza y piernas");
                            TakeDamage();
                        }
                        collided = true;
                    }
                }
                else
                {
                    if (blocking)
                    {
                        //Debug.Log("pego en piernas (block pecho)");
                        TakeDamage();
                    }
                    else
                    {
                        //Debug.Log("pego en pecho y piernas");
                        TakeDamage();
                    }
                    collided = true;
                }
            }
            if (collided)
            {
                break;
            }
            switch (i)
            {
                case 0:
                    if (blocking)
                    {
                        if (damageProperties.type == DamageType.Ulti || damageProperties.type == DamageType.Ability)
                        {
                            TakeDamage();
                        }
                        else
                        {
                            //Debug.Log("(block cabeza)");
                            if (!detectedBlock)
                            {
                                GetComponent<UIManager_FF>().AddXP(damageProperties.damage);
                                detectedBlock = true;
                                damageProperties.disableAction = ResetDetection;
                            }
                        }
                    }
                    else
                    {
                        //Debug.Log("pego en cabeza");
                        TakeDamage();
                    }
                    break;
                case 1:
                    if (blocking)
                    {
                        if (damageProperties.type == DamageType.Ulti || damageProperties.type == DamageType.Ability)
                        {
                            TakeDamage();
                        }
                        else
                        {
                            //Debug.Log("(block pecho)");
                            if (!detectedBlock)
                            {
                                GetComponent<UIManager_FF>().AddXP(damageProperties.damage);
                                detectedBlock = true;
                                damageProperties.disableAction = ResetDetection;
                            }
                        }
                    }
                    else
                    {
                        //Debug.Log("pego en pecho");
                        TakeDamage();
                    }
                    break;
                case 2:
                    //Debug.Log("pego en piernas");
                    TakeDamage();
                    break;
            }
        }
        hColliders = new Collider[3];
        damageProperties = null;
    }


    void ResetDetection(Damage_FF damageProperties)
    {
        if (damageProperties.type != DamageType.Ability && damageProperties.type != DamageType.Ulti)
        {
            detectedBlock = false;
            detectedHit = false;
        }
        else
        {
            detectedAbility = false;
        }
    }

    void TakeDamage()
    {
        if ((!detectedHit && damageProperties.type != DamageType.Ability && damageProperties.type != DamageType.Ulti) || (!detectedAbility && (damageProperties.type == DamageType.Ability || damageProperties.type == DamageType.Ulti)))
        {
            GetComponent<UIManager_FF>().ChangeHealth(-damageProperties.damage);
            if (damageProperties.type != DamageType.Ability && damageProperties.type != DamageType.Ulti)
            {
                GetComponent<PlayerController_FF>().otherPlayer.GetComponent<UIManager_FF>().AddXP(damageProperties.damage * XPMultiplier);
                if (damageProperties.type == DamageType.Punch)
                {
                    //animacion de me pego un puñetazo + stun
                }
                if (damageProperties.type == DamageType.Kick)
                {
                    //animacion de me pego una patada + stun
                }
                if (damageProperties.type == DamageType.UpperCut)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<PlayerController_FF>().movementSpeed * -GetComponent<PlayerController_FF>().pMovDirection, 0, 0);
                    GetComponent<Rigidbody>().AddForce(Vector3.up * GetComponent<PlayerController_FF>().jumpForce, ForceMode.Impulse);
                    //animacion de me pego un gancho + stun
                }
                if (damageProperties.type == DamageType.Smash)
                {
                    if (GetComponent<PlayerController_FF>().airborne)
                    {
                        GetComponent<Rigidbody>().AddForce(Vector3.down * smashForce, ForceMode.Impulse);
                    }
                }
                detectedHit = true;
            }
            else
            {
                detectedAbility = true;
            }
            if (damageProperties.type == DamageType.Ability || damageProperties.type == DamageType.SlideKick || damageProperties.type == DamageType.Smash)
            {
                CalculateKnockback();
            }
            damageProperties.disableAction = ResetDetection;
        }
    }

    void CalculateKnockback()
    {
        int knockback = 1;
        if (damageProperties.type != DamageType.Ability)
        {
            knockback += Mathf.RoundToInt(damageProperties.owner.GetComponent<PlayerController_FF>().pMovDirection - GetComponent<PlayerController_FF>().pMovDirection);
        }
        else
        {
            knockback += Mathf.RoundToInt(-GetComponent<PlayerController_FF>().pMovDirection);
        }
        if (Mathf.Abs(GetComponent<PlayerController_FF>().pMovDirection) == 0.5f)
        {
            knockback--;
        }
        GetComponent<Rigidbody>().AddForce(Mathf.Clamp(knockback, 0, Mathf.Infinity) * knockbackForce * -transform.forward);
    }
}
