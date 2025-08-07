using UnityEngine;

public class Player_GroundedState : EntityState
{
    public Player_GroundedState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.gravityScale = player.normalGravityScale;
        player.canDoubleJump = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (rb.linearVelocity.y < 0 && !player.groundDetected)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (player.input.Player.Jump.WasPerformedThisFrame() && player.coyoteTimer > 0f)
        {
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
