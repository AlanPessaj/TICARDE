﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager_FF : MonoBehaviour
{
    public Collider[] hColliders = new Collider[3];
    public Damage_FF damageProperties = null;
    public Animator animator;
    public float XPMultiplier;
    public float smashForce;
    public float knockbackForce;
    bool detectedHit;
    bool detectedBlock;
    bool detectedAbility;
    public bool detectedFall;
    int hTrigger;
    public float slideKickDamage;
    public float smashDamage;
    public AudioClip[] blockSounds;
    public AudioClip[] hitSounds;
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
                            if (GetComponent<PlayerController_FF>().InState("Blocking"))
                            {
                                if (GetComponent<PlayerController_FF>().cColliders.activeSelf)
                                {
                                    //Debug.Log("(block cabeza pecho piernas)");
                                    CheckBlock();
                                }
                                else
                                {
                                    //Debug.Log("pego en piernas (block pecho cabeza)");
                                    TakeDamage();
                                }
                            }
                            else
                            {
                                //Debug.Log("pego en cabeza, pecho y piernas");
                                CheckTriggerDistance(hColliders);
                                TakeDamage();
                            }
                            collided = true;
                            break;
                        }
                        else
                        {
                            if (GetComponent<PlayerController_FF>().InState("Blocking"))
                            {
                                //Debug.Log("(block cabeza pecho)");
                                CheckBlock();
                            }
                            else
                            {
                                //Debug.Log("pego en cabeza y pecho");
                                CheckTriggerDistance(new Collider[] { hColliders[0], hColliders[1] });
                                TakeDamage();
                            }
                            collided = true;
                        }
                    }
                    else
                    {
                        if (GetComponent<PlayerController_FF>().InState("Blocking"))
                        {
                            if (GetComponent<PlayerController_FF>().cColliders.activeSelf)
                            {
                                //Debug.Log("(block cabeza piernas)");
                                CheckBlock();
                            }
                            else
                            {
                                //Debug.Log("pego en piernas (block cabeza)");
                                TakeDamage();
                            }
                        }
                        else
                        {
                            //Debug.Log("pego en cabeza y piernas");
                            CheckTriggerDistance(new Collider[] { hColliders[0], hColliders[2] });
                            TakeDamage();
                        }
                        collided = true;
                    }
                }
                else
                {
                    if (GetComponent<PlayerController_FF>().InState("Blocking"))
                    {
                        if (GetComponent<PlayerController_FF>().cColliders.activeSelf)
                        {
                            //Debug.Log("(block pecho piernas)");
                            CheckBlock();
                        }
                        else
                        {
                            //Debug.Log("pego en piernas (block pecho)");
                            TakeDamage();
                        }
                    }
                    else
                    {
                        //Debug.Log("pego en pecho y piernas");
                        CheckTriggerDistance(new Collider[] { hColliders[1], hColliders[2] });
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
                    if (GetComponent<PlayerController_FF>().InState("Blocking"))
                    {
                        if (damageProperties.type == DamageType.Ulti || damageProperties.type == DamageType.Ability)
                        {
                            TakeDamage();
                        }
                        else
                        {
                            //Debug.Log("(block cabeza)");
                            CheckBlock();
                        }
                    }
                    else
                    {
                        //Debug.Log("pego en cabeza");
                        hTrigger = 0;
                        TakeDamage();
                    }
                    break;
                case 1:
                    if (GetComponent<PlayerController_FF>().InState("Blocking"))
                    {
                        if (damageProperties.type == DamageType.Ulti || damageProperties.type == DamageType.Ability)
                        {
                            TakeDamage();
                        }
                        else
                        {
                            //Debug.Log("(block pecho)");
                            CheckBlock();
                        }
                    }
                    else
                    {
                        //Debug.Log("pego en pecho");
                        hTrigger = 1;
                        TakeDamage();
                    }
                    break;
                case 2:
                    if (GetComponent<PlayerController_FF>().cColliders.activeSelf)
                    {
                        //Debug.Log("(block piernas)");
                        CheckBlock();
                    }
                    else
                    {
                        //Debug.Log("pego en piernas");
                        hTrigger = 2;
                        TakeDamage();
                    }
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
        if (((!detectedHit && damageProperties.type != DamageType.Ability && damageProperties.type != DamageType.Ulti) || (!detectedAbility && (damageProperties.type == DamageType.Ability || damageProperties.type == DamageType.Ulti))) && !GetComponent<PlayerController_FF>().InState("Death") && (!GetComponent<PlayerController_FF>().InState("HitSlideKick") || hTrigger == 2))
        {
            GetComponent<UIManager_FF>().ChangeHealth(-damageProperties.damage);
            if (damageProperties.type != DamageType.Ability && damageProperties.type != DamageType.Ulti)
            {
                GetComponent<PlayerController_FF>().otherPlayer.GetComponent<UIManager_FF>().AddXP(damageProperties.damage * XPMultiplier);
                if (damageProperties.type == DamageType.UpperCut)
                {
                    GetComponent<Rigidbody>().AddForce(GetComponent<PlayerController_FF>().movementSpeed * -GetComponent<PlayerController_FF>().pMovDirection, GetComponent<PlayerController_FF>().jumpForce * 1.3f, 0, ForceMode.Impulse);
                }
                if (damageProperties.type == DamageType.SlideKick)
                {
                    GetComponent<Rigidbody>().velocity = GetComponent<PlayerController_FF>().movementSpeed * GetComponent<PlayerController_FF>().pMovDirection * Vector3.right * 2f;
                    animator.GetComponent<Rigidbody>().AddForce(0.5f * animator.GetComponent<PlayerController_FF>().jumpForce * Vector3.up, ForceMode.Impulse);
                }
                if (damageProperties.type == DamageType.Smash && GetComponent<PlayerController_FF>().airborne)
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.down * smashForce, ForceMode.Impulse);
                }
                detectedHit = true;
            }
            else detectedAbility = true;
            if (!(GetComponent<PlayerController_FF>().InState("HitSlideKick") || GetComponent<PlayerController_FF>().InState("HitSmashAir")))
            {
                if (damageProperties.type != DamageType.Ulti && damageProperties.type != DamageType.UpperCut && damageProperties.type != DamageType.Smash && damageProperties.type != DamageType.SlideKick)
                    CalculateKnockback();
                if (GetComponent<UIManager_FF>().health > 0) PlayHitAnimation();
            }
            damageProperties.disableAction = ResetDetection;
        }
    }

    public void TakeFallDamage(bool smash)
    {
        if (!detectedFall)
        {
            StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SingleBlink(GetComponent<PlayerController_FF>().isPlayer1, "RED"));
            GetComponent<AudioSource>().PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
            GetComponent<UIManager_FF>().ChangeHealth(smash ? -smashDamage : -slideKickDamage);
            detectedFall = true;
        }
    }

    void CalculateKnockback()
    {
        float knockback = 2;
        if (damageProperties.type != DamageType.Ability)
        {
            knockback += Mathf.Round(damageProperties.owner.GetComponent<PlayerController_FF>().pMovDirection + GetComponent<PlayerController_FF>().pMovDirection);
        }
        else
        {
            knockback += Mathf.Round(-GetComponent<PlayerController_FF>().pMovDirection);
        }
        if (GetComponent<PlayerController_FF>().InState("Crouched") || GetComponent<PlayerController_FF>().InState("CrouchedRunFowards") || GetComponent<PlayerController_FF>().InState("CrouchedRunBackwards"))
        {
            knockback--;
        }
        if (damageProperties.type == DamageType.Normal)
        {
            knockback *= 0.5f;
        }
        GetComponent<Rigidbody>().AddForce(Mathf.Clamp(knockback, 0, Mathf.Infinity) * knockbackForce * -transform.forward);
    }

    void CheckBlock()
    {
        if (!detectedBlock)
        {
            if (damageProperties.type == DamageType.Ulti || damageProperties.type == DamageType.Ability || damageProperties.type == DamageType.SlideKick)
            {
                TakeDamage();
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(blockSounds[Random.Range(0, blockSounds.Length)]);
                GetComponent<UIManager_FF>().AddXP(damageProperties.damage);
                detectedBlock = true;
                damageProperties.disableAction = ResetDetection;
            }
        }
    }

    void PlayHitAnimation()
    {
        StartCoroutine(GAMEMANAGER.Instance.GetComponent<LedsController>().SingleBlink(GetComponent<PlayerController_FF>().isPlayer1, "RED"));
        GetComponent<AudioSource>().PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
        if (GetComponent<PlayerController_FF>().InState("Crouched") || GetComponent<PlayerController_FF>().InState("CrouchedRunFowards") || GetComponent<PlayerController_FF>().InState("CrouchedRunBackwards"))
        {
            animator.SetTrigger("crouchHit");
            return;
        }
        switch (damageProperties.type)  
        {
            case DamageType.UpperCut:
                animator.SetTrigger("upperCutHit");
                break;
            case DamageType.Smash:
                if (GetComponent<PlayerController_FF>().airborne) animator.SetTrigger("airSmashHit"); else animator.SetTrigger("floorSmashHit");
                break;
            case DamageType.SlideKick:
                animator.SetTrigger("slideKickHit");
                break;
            default:
                switch (hTrigger)
                {
                    case 0:
                        animator.SetTrigger("headHit");
                        break;
                    case 1:
                        animator.SetTrigger("chestHit");
                        break;
                    case 2:
                        animator.SetTrigger("legsHit");
                        break;
                    default:
                        Debug.LogError("invalid hTrigger value");
                        break;
                }
                break;
        }
    }

    void CheckTriggerDistance(Collider[] triggers)
    {
        float distance = Mathf.Infinity;
        foreach (Collider item in triggers)
        {
            if (distance >= Vector3.Distance(item.transform.position, damageProperties.transform.position))
            {
                distance = Vector3.Distance(item.transform.position, damageProperties.transform.position);
                hTrigger = item.GetComponent<HitDetector_FF>().colNumber;
            }
        }
    }
}
