using UnityEngine;

public class State_Anaya_Dashing : BaseState
{
    public override string Name => "Dashing";

    Anaya anaya;

    public State_Anaya_Dashing(StateMachine_Anaya sm)
    {
        anaya = sm.anaya;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anaya.gameObject.name} State: {Name}");

        anaya.AllowMoveX = false;
        anaya.AllowMoveY = false;
        anaya.AllowJump = false;
        anaya.AllowDash = false;
        anaya.AllowClimb = true;
        anaya.AllowCrawl = false;
        anaya.AllowStand = true;
        anaya.AllowSwitch = false;
        anaya.AllowCommand = false;
    }

    protected override void OnUpdate(float deltaTime)
    {
    }

    protected override void OnExit()
    {
    }
}
