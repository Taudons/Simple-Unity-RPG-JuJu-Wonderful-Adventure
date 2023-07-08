using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private float gravity = 9.81f;
    public float jumpSpeed = 7f;
    //锁定状态下的面向对象
    public Transform target;
    //角色控制器
    CharacterController characterController;
    //动画控制器
    PlayerAnimController animController;
    //角色状态
    CharacterState state;
    //按键
    [SerializeField] InputKeys keys;
    //模型
    Transform model;
    //锁定状态下的摄像机
    GameObject lockCamera;
    //移动
    Vector3 moveDirection;
    public float health;
    public int EnemyKilledCount;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        animController = GetComponentInChildren<PlayerAnimController>();
        animController.PlayerController = this;
        state = new CharacterState();
        animController.State = state;
        model = transform.Find("Model");
        lockCamera = GameObject.Find("Virtual Camera Group").transform.Find("LockLook").gameObject;
    }

    private void Update()
    {
        InputCheck();
        characterController.Move(new Vector3(0, moveDirection.y, 0) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        FreeLook();
        LockLook();

        Vector2 movement = SquareToCircle(moveDirection);
        if (state.isLocking && state.isEquiped)
        {
            animController.SetFloat("SpeedZ", movement.y);
            animController.SetFloat("SpeedX", movement.x);
        }
        else
        {
            animController.SetFloat("SpeedZ", movement.sqrMagnitude);
        }
    }

    //自由视角
    void FreeLook()
    {
        if (state.isLocking)
            return;
        if (moveDirection.sqrMagnitude < 0.1f)
            return;

        model.localRotation = Quaternion.Lerp(model.localRotation, Quaternion.Euler(0, 0, 0), 12 * Time.deltaTime);

        Vector3 dir = moveDirection.normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        Quaternion targetDir = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetDir, 12 * Time.deltaTime);
    }
    //锁定视角
    void LockLook()
    {
        if (!state.isLocking)
            return;
        if (moveDirection.sqrMagnitude < 0.1f)
            return;

        Vector3 cameraEuler = Camera.main.transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0, cameraEuler.y, 0);

        //非战斗状态
        if (!state.isEquiped)
        {
            Vector3 dir = moveDirection.normalized;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            Quaternion targetDir = Quaternion.Euler(0, angle, 0);
            model.localRotation = Quaternion.Lerp(model.localRotation, targetDir, 12 * Time.deltaTime);
        }
        //战斗状态
        else
        {
            model.localRotation = Quaternion.Lerp(model.localRotation, Quaternion.Euler(0, 0, 0), 12 * Time.deltaTime);
        }

    }
    //输入检测
    void InputCheck()
    {
        float fwd = 0;
        float right = 0;
        fwd = Input.GetKey(keys.fwd) ? 1 : fwd;
        fwd = Input.GetKey(keys.bwd) ? -1 : fwd;
        right = Input.GetKey(keys.right) ? 1 : right;
        right = Input.GetKey(keys.left) ? -1 : right;

        moveDirection.z = Mathf.Lerp(moveDirection.z, fwd, 24 * Time.deltaTime);
        moveDirection.x = Mathf.Lerp(moveDirection.x, right, 24 * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;

        //拔出武器
        if (Input.GetKeyDown(keys.equip))
        {
            state.isEquiped = !state.isEquiped;
            animController.SetTrigger("Equip");
            animController.SetBool("AttackCilck leftCombo 0", false);
        }

        //锁定视角
        if (Input.GetKeyDown(keys.Lock))
        {
            state.isLocking = !state.isLocking;
            lockCamera.SetActive(state.isLocking);
            animController.SetBool("LockState", state.isLocking);
        }

        if (characterController.isGrounded) // 如果在地面上
        {        
            if (Input.GetButton("Jump")) // 如果按下跳跃键
            {
                
                moveDirection.y = jumpSpeed; // 设置垂直移动速度
            }
        }

    }

    /// <summary>
    /// 椭圆映射
    /// </summary>
    /// <param name="oldVec"></param>
    /// <returns></returns>
    Vector2 SquareToCircle(Vector3 oldVec)
    {
        Vector2 newVec;
        newVec.x = oldVec.x * Mathf.Sqrt(1 - (oldVec.z * oldVec.z) / 2);
        newVec.y = oldVec.z * Mathf.Sqrt(1 - (oldVec.x * oldVec.x) / 2);
        return newVec;
    }

    /// <summary>
    /// 动画根节点速度作为移动速度
    /// </summary>
    /// <param name="velocity"></param>
    public void SetCharacterVelocity(Vector3 velocity)
    {
        characterController.Move(new Vector3(velocity.x, velocity.y, velocity.z) * Time.deltaTime);
    }

    public void TakeDamage(float level, float attackModel)
    {
        health = health - (2 + level) * attackModel;
        Debug.Log(health);
    }
}

