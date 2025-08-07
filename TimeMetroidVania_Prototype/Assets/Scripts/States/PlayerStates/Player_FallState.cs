using UnityEngine;

public class Player_FallState : Player_AirState
{
    public Player_FallState(StateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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

        float target = player.maxFallGravityScale;
        rb.gravityScale = Mathf.Lerp(rb.gravityScale, target, player.gravityLerpSpeed * Time.deltaTime);

        if (player.groundDetected)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
