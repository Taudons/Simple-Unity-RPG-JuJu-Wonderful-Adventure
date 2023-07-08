using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tp : MonoBehaviour
{
    public Transform TargetPos;
    //public Transform Player;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("111");
        if (other.CompareTag("Player"))
        {            
            CharacterController cc = other.GetComponent<CharacterController>();
            if (cc != null)
                cc.enabled = false;
            Debug.Log("222");
            // 将角色传送过去
            other.transform.position = TargetPos.position;
            if (cc != null)
                cc.enabled = true;
        }          
    }
}
