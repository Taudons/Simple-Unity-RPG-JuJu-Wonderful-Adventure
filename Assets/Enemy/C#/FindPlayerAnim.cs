using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindPlayerAnim : MonoBehaviour
{
    private bool isAttact;
    private bool isDeath;
    private Transform target;   //设置追踪目标的位置
    public float MoveSpeed = 2.5f; //敌人移动速度
    private NavMeshAgent navMeshAgent;  //设置寻路组件
    private float distance;

    void Start()
    {
        isAttact = GetComponent<EnemyController>().isAttact;
        target = GameObject.FindWithTag("Player").transform;  //获取游戏中主角的位置
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = MoveSpeed;  //设置寻路器的行走速度
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, target.position);//获得敌人与玩家之间的距离
        if (distance <= 10)
        {
            if (isAttact == false)
            {
                if (isDeath == false)
                {
                    navMeshAgent.SetDestination(target.transform.position); //设置寻路目标
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
