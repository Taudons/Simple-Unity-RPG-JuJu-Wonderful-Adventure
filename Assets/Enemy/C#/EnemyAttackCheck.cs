using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAttackCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Sword_R;
    private bool isAttacking = false; // 是否正在攻击
    private float attackStartTime; // 攻击开始时间
    private float attackDuration = 0.5f; // 攻击持续时间
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
            // 如果碰撞的是敌人，则调用 TakeDamage 函数
            //collider.gameObject.GetComponent<EnemyController>().TakeDamage(level,attackModel)
        }
        else
        {
            isAttacking = false;
            swordCollider.isTrigger = false;
        }
    }

    public void Attack02(AnimationEvent animationEvent)
    {
        isAttacking = true;
        attackStartTime = Time.time;
        // 添加攻击检测逻辑
    }

}