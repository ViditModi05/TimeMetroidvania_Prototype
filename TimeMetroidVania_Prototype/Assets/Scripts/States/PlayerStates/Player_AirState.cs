using UnityEngine;

public class Player_AirState : EntityState
{
    public Player_AirState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        bool jumpPressed = player.input.Player.Jump.WasPerformedThisFrame();

        if (player.moveInput.x != 0)
        {
            player.SetVelocity(player.moveInput.x * (player.moveSpeed * player.inAirMoveMultiplier), rb.linearVelocity.y);
        }



        if (jumpPressed && player.canDoubleJump)
        {
            player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
            player.canDoubleJump = false;
        }
    }
}
