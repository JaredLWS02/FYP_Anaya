using UnityEngine;

public class State_Anaya_Standing : BaseState
{
    public override string Name => "Standing";

    Anaya anaya;

    public State_Anaya_Standing(StateMachine_Anaya sm)
    {
        anaya = sm.anaya;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anaya.gameObject.name} State: {Name}");

        anaya.AllowMoveY = false;
        anaya.AllowJump = true;
        anaya.AllowDash = true;
        anaya.AllowClimb = true;
        anaya.AllowCrawl = true;
        anaya.AllowStand = false;
        anaya.AllowCommand = true;
    }

    protected override void OnUpdate(float deltaTime)
    {
    }

    protected override void OnExit()
    {
    }
}
