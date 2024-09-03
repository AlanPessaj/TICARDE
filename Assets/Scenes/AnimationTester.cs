using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetTrigger("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetTrigger("2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.SetTrigger("3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            animator.SetTrigger("4");
        }
    }
}
