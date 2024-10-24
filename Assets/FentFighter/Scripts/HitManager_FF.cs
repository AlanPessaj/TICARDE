﻿using System.Collections;
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
                                TakeDamage(damageProperties);
                            }
                            else
                            {
                                //Debug.Log("pego en cabeza, pecho y piernas");
                                TakeDamage(damageProperties);
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
                                        TakeDamage(damageProperties);
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
                                TakeDamage(damageProperties);
                            }
                            collided = true;
                        }
                    }
                    else
                    {
                        if (blocking)
                        {
                            //Debug.Log("pego en piernas (block cabeza)");
                            TakeDamage(damageProperties);
                        }
                        else
                        {
                            //Debug.Log("pego en cabeza y piernas");
                            TakeDamage(damageProperties);
                        }
                        collided = true;
                    }
                }
                else
                {
                    if (blocking)
                    {
                        //Debug.Log("pego en piernas (block pecho)");
                        TakeDamage(damageProperties);
                    }
                    else
                    {
                        //Debug.Log("pego en pecho y piernas");
                        TakeDamage(damageProperties);
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
                            TakeDamage(damageProperties);
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
                        TakeDamage(damageProperties);
                    }
                    break;
                case 1:
                    if (blocking)
                    {
                        if (damageProperties.type == DamageType.Ulti || damageProperties.type == DamageType.Ability)
                        {
                            TakeDamage(damageProperties);
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
                        TakeDamage(damageProperties);
                    }
                    break;
                case 2:
                    //Debug.Log("pego en piernas");
                    TakeDamage(damageProperties);
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

    void TakeDamage(Damage_FF damageProperties)
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
                    GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<PlayerController_FF>().movementSpeed * -GetComponent<PlayerController_FF>().movDirection, 0, 0);
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
            damageProperties.disableAction = ResetDetection;
        }
    }
}
