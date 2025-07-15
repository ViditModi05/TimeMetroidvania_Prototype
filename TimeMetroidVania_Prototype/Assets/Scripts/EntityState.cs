using UnityEngine;
using UnityEngine.Windows;

public class EntityState
{
    [Header("Refs")]
    protected StateMachine stateMachine;
    protected Player player;
    protected string animBoolName;
    protected Rigidbody2D rb;
    protected Animator anim;
    protected PlayerInputSystem input;
    protected float stateTimer;
    protected bool triggerCalled;
    public EntityState(StateMachine stateMachine, Player player, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animBoolName = animBoolName;

        anim = player.anim;
        rb = player.rb;
        input = player.input;

    }

    public virtual void Enter()
    {
        Debug.Log("Entering state: " +  animBoolName);
        anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }

    public virtual void Update()
    {
        Debug.Log("Currently in state: " + animBoolName);
        stateTimer -= Time.deltaTime;

        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        if (input.Player.Dash.WasPressedThisFrame() && CanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }

        if (input.Player.TimeSlow.WasPressedThisFrame())
        {
            TimeManager.Instance.SlowWorldTime();
            return;
        }
        if(input.Player.TimeFast.WasPressedThisFrame())
        {
            TimeManager.Instance.IncreasePlayerTime();
            return;
        }
        if (input.Player.CancelTimeSlow.WasPressedThisFrame())
        {
            TimeManager.Instance.ResetWorldTime();
        }
    }

    public virtual void Exit()
    {
        Debug.Log("Exiting state: " + animBoolName);
        anim.SetBool(animBoolName, false);
    }

    public void CallAnimationTrigger()
    {
        triggerCalled = true;
    }

    private bool CanDash()
    {
        if (player.wallDetected)
        {
            return false;
        }

        if (stateMachine.currentState == player.dashState)
        {
            return false;
        }

        return true;
    }
}
