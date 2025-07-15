using UnityEngine;

public class Player_JumpState : Player_AirState
{
    public Player_JumpState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(rb.linearVelocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.linearVelocity.y < 0 )
        {
            stateMachine.ChangeState(player.fallState);
        }
    }

}
