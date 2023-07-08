using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    /// <summary>
    /// 角色逻辑控制器
    /// </summary>
    PlayerController playerController;
    public PlayerController PlayerController { set { playerController = value; } }

    /// <summary>
    /// 角色状态
    /// </summary>
    CharacterState state;
    public CharacterState State { set { state = value; } }

    /// <summary>
    /// 动画状态机
    /// </summary>
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 设置Float参数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public void SetFloat(string name, float value)
    {
        anim.SetFloat(name, value);
    }

    /// <summary>
    /// 触发器
    /// </summary>
    /// <param name="name"></param>
    public void SetTrigger(string name)
    {
        anim.SetTrigger(name);
    }

    public void SetBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    /// <summary>
    /// 动画执行时每一帧调用
    /// </summary>
    private void OnAnimatorMove()
    {
        playerController.SetCharacterVelocity(anim.velocity);
    }

    #region 帧事件
    /// <summary>
    /// 拔出武器调用
    /// </summary>
    public void OnEquipStateEnter()
    {
        SetBool("IsEquiped", true);
    }

    /// <summary>
    /// 收起武器调用
    /// </summary>
    public void OnUnEquipStateEnter()
    {
        SetBool("IsEquiped", false);
    }
    #endregion

}
