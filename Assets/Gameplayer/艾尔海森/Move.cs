using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform mainCamera;
    public float speed;
    public Animator animController;
    public CharacterController characterController;
    public Transform player;
    Vector3 moveDir;
    float feet;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    private void Update()
    {
        InputCheck();
        animController.SetFloat("SpeedZ", feet);
        SetCharacterVelocity();

    }
 

    void InputCheck()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        moveDir.x = h;
        moveDir.z = v;
        if (moveDir != Vector3.zero)
        {
            feet = 1;
        }
        else
        {
            feet = 0;
        }
    }
    public void SetCharacterVelocity()
    {
        Vector3 moveWorld = transform.TransformDirection(moveDir);
        // ÒÆ¶¯½ÇÉ«
        characterController.Move(moveWorld * speed * Time.deltaTime);
    }

    
}
