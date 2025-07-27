using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript_FF : StateMachineBehaviour
{
    public float slideSpeed;
    bool facingLeft;
    public float slideKickCooldown;
    public AudioClip[] wooshSounds;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Smash") && animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction != null)
        {
            animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction(animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>());
            animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction = null;
        }
        if (stateInfo.IsName("Punch") || stateInfo.IsName("UpperCut") || stateInfo.IsName("Smash"))
        {
            animator.gameObject.GetComponent<PlayerController_FF>().fist.SetActive(true);
            if (stateInfo.IsName("UpperCut"))
            {
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().damage *= 1.5f;
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().type = DamageType.UpperCut;
            }
            if (stateInfo.IsName("Smash"))
            {
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().damage *= 1.5f;
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().type = DamageType.Smash;
            }
        }
        else
        {
            animator.gameObject.GetComponent<PlayerController_FF>().foot.SetActive(true);
            if (stateInfo.IsName("SlideKick"))
            {
                animator.GetComponent<AudioSource>().PlayOneShot(wooshSounds[0]);
                animator.gameObject.GetComponent<PlayerController_FF>().foot.GetComponent<Damage_FF>().type = DamageType.SlideKick;
                animator.gameObject.GetComponent<PlayerController_FF>().slideKickCooldown = slideKickCooldown;
                facingLeft = animator.GetComponent<PlayerController_FF>().facingLeft;
                Physics.IgnoreCollision(animator.GetComponent<Collider>(), animator.GetComponent<PlayerController_FF>().otherPlayer.GetComponent<Collider>(), true);
            }
        }
    }

    bool jumped = false;
    bool playedWoosh = false;

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 0.5f && animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction == null && !playedWoosh && !stateInfo.IsName("SlideKick"))
        {
            playedWoosh = true;
            animator.GetComponent<AudioSource>().PlayOneShot(wooshSounds[Random.Range(0, wooshSounds.Length)]);
        }
        if (stateInfo.IsName("SlideKick") && (!animator.IsInTransition(layerIndex) || animator.GetNextAnimatorStateInfo(layerIndex).IsName("SlideKick")))
        {
            if (facingLeft)
            {
                animator.transform.Translate(Vector3.left * slideSpeed * Time.deltaTime * stateInfo.speed, Space.World);
            }
            else
            {
                animator.transform.Translate(Vector3.right * slideSpeed * Time.deltaTime * stateInfo.speed, Space.World);
            }
        }
        if (stateInfo.IsName("UpperCut") && !jumped && (animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction != null || stateInfo.normalizedTime >= 0.5f))
        {
            jumped = true;
            if (!animator.GetComponent<PlayerController_FF>().airborne)
            {
                animator.GetComponent<Rigidbody>().velocity = new Vector3(animator.GetComponent<PlayerController_FF>().movementSpeed * animator.GetComponent<PlayerController_FF>().pMovDirection, 0, 0);
                animator.GetComponent<Rigidbody>().AddForce(0, animator.GetComponent<PlayerController_FF>().jumpForce * 1.3f, 0, ForceMode.Impulse);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("Punch") || stateInfo.IsName("UpperCut") || stateInfo.IsName("Smash"))
        {
            if (animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction != null)
            {
                animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction(animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>());
                animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction = null;
            }
            animator.GetComponent<PlayerController_FF>().fist.SetActive(false);
            if (stateInfo.IsName("UpperCut") || stateInfo.IsName("Smash"))
            {
                animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().damage /= 1.5f;
                animator.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().type = DamageType.Normal;
            }
        }
        else
        {
            if (animator.gameObject.GetComponent<PlayerController_FF>().foot.GetComponent<Damage_FF>().disableAction != null)
            {
                animator.gameObject.GetComponent<PlayerController_FF>().foot.GetComponent<Damage_FF>().disableAction(animator.gameObject.GetComponent<PlayerController_FF>().foot.GetComponent<Damage_FF>());
                animator.gameObject.GetComponent<PlayerController_FF>().foot.GetComponent<Damage_FF>().disableAction = null;
            }
            animator.gameObject.GetComponent<PlayerController_FF>().foot.SetActive(false);
            if (stateInfo.IsName("SlideKick"))
            {
                animator.gameObject.GetComponent<PlayerController_FF>().foot.GetComponent<Damage_FF>().type = DamageType.Normal;
                Physics.IgnoreCollision(animator.GetComponent<Collider>(), animator.GetComponent<PlayerController_FF>().otherPlayer.GetComponent<Collider>(), false);
            }
        }
        // if (animator.GetBool("cutToSmash"))
        // {
        //     animator.SetBool("cutToSmash", false);
        //     animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().type = DamageType.Smash;
        //     animator.gameObject.GetComponent<PlayerController_FF>().fist.SetActive(true);
        // }
        jumped = false;
        playedWoosh = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
