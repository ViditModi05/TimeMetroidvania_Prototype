using UnityEngine;

public class Player_JumpState : Player_AirState
{
    [Header("Settings")]
    private bool jumpCutApplied;
    public Player_JumpState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        jumpCutApplied = false;
        player.DisableCoyoteTime();
        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        bool jumpPressed = player.input.Player.Jump.WasPerformedThisFrame(); //check for double jump
        bool jumpReleased = player.input.Player.Jump.ReadValue<float>() == 0; //Checking if jump released

        //Jump Cut
        if (!jumpCutApplied && jumpReleased && rb.linearVelocity.y > 0)
        {
            player.SetVelocity(rb.linearVelocity.x, rb.linearVelocity.y * player.jumpCutMultiplier); 
            jumpCutApplied = true;
        }

        //Switch to fall state
        if (rb.linearVelocity.y < 0 )
        {
            stateMachine.ChangeState(player.fallState);
        }

    }

}
