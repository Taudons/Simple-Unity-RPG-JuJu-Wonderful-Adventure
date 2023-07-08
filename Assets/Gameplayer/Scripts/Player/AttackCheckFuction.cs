using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackCheckFuction : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Sword_R;
    private bool isAttacking = false; // �Ƿ����ڹ���
    private float attackStartTime; // ������ʼʱ��
    private float attackDuration = 0.5f; // ��������ʱ��
    public float level = 1;
    public float attackModel = 1;
    public bool isColliding = false;
    private Collider swordCollider;

    private void Start()
    {
        swordCollider = Sword_R.GetComponent<Collider>();
    }
    private void Update()
    {
        if (isAttacking && Time.time - attackStartTime <= attackDuration)
        {
            swordCollider.isTrigger = true;
            isColliding = true;          
            //swordCollider.GetComponent<Collider>().OnTriggerEnter() += OnTriggerEnter;
            // �����ײ���ǵ��ˣ������ TakeDamage ����
            //collider.gameObject.GetComponent<EnemyController>().TakeDamage(level,attackModel)
        }
        else
        {
            isAttacking = false;
            swordCollider.isTrigger = false;
        }
    }

    public void Attack01(AnimationEvent animationEvent)
    {
        isAttacking = true;
        attackStartTime = Time.time;
        // ��ӹ�������߼�
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    ��ײ�¼������߼�
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        do something...
    //    }
    //}
}
