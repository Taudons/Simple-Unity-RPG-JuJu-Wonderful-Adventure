using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float deathTime;
    private Animator anim;
    public float health=10;
    private float deathDuration = 5;
    private float attackDuration = 5;
    public bool hasDeath = false;
    public bool isAttact = false;
    private Transform target;
    private float distance;
    private NavMeshAgent navMeshAgent;  //设置寻路组件
    public int attackNum;
    public float attackCD;
    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        attackCD = Time.time;
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (hasDeath == false)
            {
                hasDeath = true;
                anim.SetTrigger("isDeath");
                deathTime = Time.time;  
                Player.GetComponent<PlayerController>().EnemyKilledCount++;
            }
            if(Time.time - deathTime >= deathDuration)
            {
                transform.localScale = Vector3.zero;
            }
        }

        distance = Vector3.Distance(transform.position, target.position);//获得敌人与玩家之间的距离
        if (distance <= 10&&distance>=3)
        {
            anim.SetBool("isRun", true);
        }
        if (distance <= 3)
        {
            isAttact = true;           
            anim.SetBool("isRun", false);
            navMeshAgent.isStopped = true;
            if (Time.time - attackCD >= attackDuration)
            {
                attackCD = Time.time;
                if (attackNum <= 2)
                {
                    anim.SetTrigger("isAttact");
                    attackNum++;
                }
                else
                {
                    anim.SetTrigger("isDeepAttack");
                    attackNum = 0;
                }
            }
        }
        if (distance > 10)
        {
            anim.SetBool("isRun", false);
        }
    }
    public void TakeDamage(float level, float attackModel)
    {
        health = health - (5+level) * attackModel;
        Debug.Log(health);
    }
}
