using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private float level;
    private float attackModel;
    private bool isColliding;
    private Transform parentTransform;
    public SkinnedMeshRenderer meshRenderer;
    public MeshCollider coll;
    void Start()
    {
        // 获取父物体的 MyParent 属性
        level = transform.parent.GetComponent<AttackCheckFuction>().level;
        attackModel = transform.parent.GetComponent<AttackCheckFuction>().attackModel;
        isColliding = transform.parent.GetComponent<AttackCheckFuction>().isColliding;
        parentTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMesh();
        GetComponent<Collider>().transform.position = transform.position;
        //transform.position = parentTransform.position;
        //transform.rotation = parentTransform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            return; // 如果进入触发器的对象标签为"Player"，则退出方法，不执行下面的代码
        }
        //other.gameObject.GetComponent<EnemyController>().TakeDamage(level, attackModel);
        //Debug.Log(other.tag);
        //Debug.Log(other.name);
        if (other.tag == "Enemy")
        {
            Debug.Log("触发器触发了");
            other.gameObject.GetComponent<EnemyController>().TakeDamage(level, attackModel);
        }
        if (isColliding == true)
        {
            if (other.tag == "Enemy")
            {
                Debug.Log("触发器触发了");
                other.gameObject.GetComponent<EnemyController>().TakeDamage(level, attackModel);
            }
        }
    }
    private void UpdateMesh()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh); //更新mesh
        coll.sharedMesh = null;
        coll.sharedMesh = colliderMesh; //将新的mesh赋给meshcollider
    }
}
