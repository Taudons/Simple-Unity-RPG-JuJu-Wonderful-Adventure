using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("AttackCilck left", false);
        anim.SetBool("AttackCilck right", false);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("AttackCilck left", true);
            anim.SetTrigger("AttackCilck leftCombo");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.SetBool("AttackCilck right", true);
        }
    }
}
