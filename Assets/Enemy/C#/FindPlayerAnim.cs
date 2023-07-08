using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindPlayerAnim : MonoBehaviour
{
    private bool isAttact;
    private bool isDeath;
    private Transform target;   //����׷��Ŀ���λ��
    public float MoveSpeed = 2.5f; //�����ƶ��ٶ�
    private NavMeshAgent navMeshAgent;  //����Ѱ·���
    private float distance;

    void Start()
    {
        isAttact = GetComponent<EnemyController>().isAttact;
        target = GameObject.FindWithTag("Player").transform;  //��ȡ��Ϸ�����ǵ�λ��
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = MoveSpeed;  //����Ѱ·���������ٶ�
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);//��õ��������֮��ľ���
        if (distance <= 10)
        {
            if (isAttact == false)
            {
                if (isDeath == false)
                {
                    navMeshAgent.SetDestination(target.transform.position); //����Ѱ·Ŀ��
                    navMeshAgent.isStopped = false;
                }
            }
            else
            {
                navMeshAgent.ResetPath();
                navMeshAgent.isStopped = true;
            }
        }
        isDeath = GetComponent<EnemyController>().hasDeath;
    }

}
