using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        moveDir.x = h;
        moveDir.z = v;

        FreeLook();
    }
    void FreeLook()
    {
        if (moveDir.sqrMagnitude == 0)
            return;
        //将输入检测时保存的输入值进行归一化处理，得到的向量可以看出想要移动的方向
        Vector3 dir = moveDir.normalized;
        //Atan2函数可以得到输入方向的弧度，然后乘于Mathf.Rad2Deg从弧度转换成角度
        //输入的角度再加上相机目前的角度就能得到最终角色应该朝向的角度
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        //将目标朝向的三维向量转换为四元数
        Quaternion targetDir = Quaternion.Euler(0, angle, 0);
        //角色朝向缓动至目标朝向
        transform.rotation = Quaternion.Lerp(transform.rotation, targetDir, 12 * Time.deltaTime);
    }

}
