using UnityEngine;

public class State_Anaya_Commanding : BaseState
{
    public override string Name => "Commanding";

    Anaya anaya;

    public State_Anaya_Commanding(StateMachine_Anaya sm)
    {
        anaya = sm.anaya;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anaya.gameObject.name} State: {Name}");

        anaya.AllowMoveY = true;
        anaya.AllowJump = true;
        anaya.AllowDash = true;
        anaya.AllowClimb = true;
        anaya.AllowCrawl = true;
        anaya.AllowStand = true;
        anaya.AllowCommand = true;
    }

    protected override void OnUpdate(float deltaTime)
    {
    }

    protected override void OnExit()
    {
    }
}
