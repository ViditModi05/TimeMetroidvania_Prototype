using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Refs")]
    private StateMachine stateMachine;
    public Rigidbody2D rb {  get; private set; }
    public Animator anim { get; private set; }

    [Header("Input Actions")]
    public PlayerInputSystem input { get; private set; }

    [Header("Movement")]
    public float moveSpeed;
    public Vector2 moveInput { get; private set; }

    [Header("Dash")]
    public float dashDuration = .25f;
    public float dashSpeed = 20;

    [Header("Jump")]
    public float jumpForce = 5;
    [Range(0f, 1f)]
    public float inAirMoveMultiplier = .7f;
    public float normalGravityScale {  get; private set; }
    [Header("Flip")]
    private bool facingRight = true;
    public int facingDir { get; private set; } = 1;

    [Header("Collision Detection")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    public bool groundDetected { get; private set; }
    public bool wallDetected { get; private set; }

    [Header("Time Checks")]
    public bool isTimeSlowed { get; private set; }

    #region Player_States

    [Header("States")]
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_DashState dashState { get; private set; }
    //public Player_WallSlideState wallSlideState { get; private set; }
    //public Player_WallJumpState wallJumpState { get; private set; }
    //public PlayerBasicAttackState basicAttackState { get; private set; }
    //public Player_JumpAttackState jumpAttackState { get; private set; }

    #endregion



    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        normalGravityScale = rb.gravityScale;


        stateMachine = new StateMachine();
        input = new PlayerInputSystem();
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;

        idleState = new Player_IdleState(stateMachine, this, "idle");
        moveState = new Player_MoveState(stateMachine, this, "move");
        jumpState = new Player_JumpState(stateMachine, this, "jumpFall");
        fallState = new Player_FallState(stateMachine, this, "jumpFall");
        dashState = new Player_DashState(stateMachine, this, "dash");
        //wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        //wallJumpState = new Player_WallJumpState(this, stateMachine, "jumpFall");
        //basicAttackState = new PlayerBasicAttackState(this, stateMachine, "basicAttack");
        //jumpAttackState = new Player_JumpAttackState(this, stateMachine, "jumpAttack");
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    public void CallAnimationTrigger()
    {
        stateMachine.currentState.CallAnimationTrigger();
    }

    private void Update()
    {
        HandleCollisionDetection();
        stateMachine.UpdateActiveState();
        //Debug.Log(moveInput.x);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        float adjustedX = _xVelocity * TimeManager.Instance.playerTimeScale;
        rb.linearVelocity = new Vector2(adjustedX, _yVelocity);
        HandleFlip(_xVelocity);
    }

    private void HandleFlip(float _xVelocity)
    {
        if (_xVelocity > 0 && !facingRight)
        {
            Flip();
        }
        if (_xVelocity < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir *= -1;
    }

    private void HandleCollisionDetection()
    {
        groundDetected = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        //wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround) &&
                       //Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        //Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
        //Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
    }
    private void OnDisable()
    {
        input.Disable();
    }
}
