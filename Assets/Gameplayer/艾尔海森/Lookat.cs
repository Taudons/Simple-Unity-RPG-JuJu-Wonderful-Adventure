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
        //��������ʱ���������ֵ���й�һ�������õ����������Կ�����Ҫ�ƶ��ķ���
        Vector3 dir = moveDir.normalized;
        //Atan2�������Եõ����뷽��Ļ��ȣ�Ȼ�����Mathf.Rad2Deg�ӻ���ת���ɽǶ�
        //����ĽǶ��ټ������Ŀǰ�ĽǶȾ��ܵõ����ս�ɫӦ�ó���ĽǶ�
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        //��Ŀ�곯�����ά����ת��Ϊ��Ԫ��
        Quaternion targetDir = Quaternion.Euler(0, angle, 0);
        //��ɫ���򻺶���Ŀ�곯��
        transform.rotation = Quaternion.Lerp(transform.rotation, targetDir, 12 * Time.deltaTime);
    }

}
