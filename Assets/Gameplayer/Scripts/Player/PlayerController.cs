using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private float gravity = 9.81f;
    public float jumpSpeed = 7f;
    //����״̬�µ��������
    public Transform target;
    //��ɫ������
    CharacterController characterController;
    //����������
    PlayerAnimController animController;
    //��ɫ״̬
    CharacterState state;
    //����
    [SerializeField] InputKeys keys;
    //ģ��
    Transform model;
    //����״̬�µ������
    GameObject lockCamera;
    //�ƶ�
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

    //�����ӽ�
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
    //�����ӽ�
    void LockLook()
    {
        if (!state.isLocking)
            return;
        if (moveDirection.sqrMagnitude < 0.1f)
            return;

        Vector3 cameraEuler = Camera.main.transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0, cameraEuler.y, 0);

        //��ս��״̬
        if (!state.isEquiped)
        {
            Vector3 dir = moveDirection.normalized;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            Quaternion targetDir = Quaternion.Euler(0, angle, 0);
            model.localRotation = Quaternion.Lerp(model.localRotation, targetDir, 12 * Time.deltaTime);
        }
        //ս��״̬
        else
        {
            model.localRotation = Quaternion.Lerp(model.localRotation, Quaternion.Euler(0, 0, 0), 12 * Time.deltaTime);
        }

    }
    //������
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

        //�γ�����
        if (Input.GetKeyDown(keys.equip))
        {
            state.isEquiped = !state.isEquiped;
            animController.SetTrigger("Equip");
            animController.SetBool("AttackCilck leftCombo 0", false);
        }

        //�����ӽ�
        if (Input.GetKeyDown(keys.Lock))
        {
            state.isLocking = !state.isLocking;
            lockCamera.SetActive(state.isLocking);
            animController.SetBool("LockState", state.isLocking);
        }

        if (characterController.isGrounded) // ����ڵ�����
        {        
            if (Input.GetButton("Jump")) // ���������Ծ��
            {
                
                moveDirection.y = jumpSpeed; // ���ô�ֱ�ƶ��ٶ�
            }
        }

    }

    /// <summary>
    /// ��Բӳ��
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
    /// �������ڵ��ٶ���Ϊ�ƶ��ٶ�
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

