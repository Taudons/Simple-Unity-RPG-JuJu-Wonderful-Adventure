using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCubeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public float level = 1;
    public float attackModel = 5;
    private bool isColliding;
    private Transform parentTransform;
    public SkinnedMeshRenderer meshRenderer;
    public MeshCollider coll;
    void Start()
    {
        // 获取父物体的 MyParent 属性
        //level = transform.parent.GetComponent<AttackCheckFuction>().level;
        //attackModel = transform.parent.GetComponent<AttackCheckFuction>().attackModel;
        //isColliding = transform.parent.GetComponent<AttackCheckFuction>().isColliding;
        //parentTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateMesh();
        //GetComponent<Collider>().transform.position = transform.position;
        //transform.position = parentTransform.position;
        //transform.rotation = parentTransform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            return; // 如果进入触发器的对象标签为"Enemy"，则退出方法，不执行下面的代码
        }
        //other.gameObject.GetComponent<EnemyController>().TakeDamage(level, attackModel);
        //Debug.Log(other.tag);
        //Debug.Log(other.name);
        if (other.tag == "Player")
        {
            Debug.Log("触发器触发了");
            other.gameObject.GetComponent<PlayerController>().TakeDamage(level, attackModel);
        }
        if (isColliding == true)
        {
            if (other.tag == "Enemy")
            {
                Debug.Log("触发器触发了");
                other.gameObject.GetComponent<PlayerController>().TakeDamage(level, attackModel);
            }
        }
    }

}
