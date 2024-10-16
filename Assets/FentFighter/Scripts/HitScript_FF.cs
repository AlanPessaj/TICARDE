﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript_FF : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("punch") || stateInfo.IsName("upperCut"))
        {
            animator.gameObject.GetComponent<PlayerController_FF>().fist.SetActive(true);
            if (stateInfo.IsName("upperCut"))
            {
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().damage *= 1.5f;
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().type = DamageType.UpperCut;
            }
        }
        else
        {
            animator.gameObject.GetComponent<PlayerController_FF>().foot.SetActive(true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("punch") || stateInfo.IsName("upperCut"))
        {
            if (animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction != null)
            {
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction(animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>());
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction = null;
            }
            animator.gameObject.GetComponent<PlayerController_FF>().fist.SetActive(false);
            if (stateInfo.IsName("upperCut"))
            {
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().damage /= 1.5f;
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().type = DamageType.Punch;
            }
        }
        else
        {
            if (animator.gameObject.GetComponent<PlayerController_FF>().foot.GetComponent<Damage_FF>().disableAction != null)
            {
                animator.gameObject.GetComponent<PlayerController_FF>().foot.GetComponent<Damage_FF>().disableAction(animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>());
                animator.gameObject.GetComponent<PlayerController_FF>().fist.GetComponent<Damage_FF>().disableAction = null;
            }
            animator.gameObject.GetComponent<PlayerController_FF>().foot.SetActive(false);
        }
        Debug.Log(stateInfo.shortNameHash);
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
