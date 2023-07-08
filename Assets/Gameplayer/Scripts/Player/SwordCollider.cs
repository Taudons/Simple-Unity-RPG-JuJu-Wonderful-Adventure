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
        // ��ȡ������� MyParent ����
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
            return; // ������봥�����Ķ����ǩΪ"Player"�����˳���������ִ������Ĵ���
        }
        //other.gameObject.GetComponent<EnemyController>().TakeDamage(level, attackModel);
        //Debug.Log(other.tag);
        //Debug.Log(other.name);
        if (other.tag == "Enemy")
        {
            Debug.Log("������������");
            other.gameObject.GetComponent<EnemyController>().TakeDamage(level, attackModel);
        }
        if (isColliding == true)
        {
            if (other.tag == "Enemy")
            {
                Debug.Log("������������");
                other.gameObject.GetComponent<EnemyController>().TakeDamage(level, attackModel);
            }
        }
    }
    private void UpdateMesh()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh); //����mesh
        coll.sharedMesh = null;
        coll.sharedMesh = colliderMesh; //���µ�mesh����meshcollider
    }
}
