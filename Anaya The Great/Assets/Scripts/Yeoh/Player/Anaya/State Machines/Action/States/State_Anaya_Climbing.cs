using UnityEngine;

public class State_Anaya_Climbing : BaseState
{
    public override string Name => "Climbing";

    Anaya anaya;

    public State_Anaya_Climbing(StateMachine_Anaya sm)
    {
        anaya = sm.anaya;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anaya.gameObject.name} State: {Name}");

        anaya.AllowMoveX = false;
        anaya.AllowMoveY = true;
        anaya.AllowJump = true;
        anaya.AllowDash = true;
        anaya.AllowClimb = false;
        anaya.AllowCrawl = false;
        anaya.AllowStand = false;
        anaya.AllowSwitch = true;
        anaya.AllowCommand = false;
    }

    protected override void OnUpdate(float deltaTime)
    {
    }

    protected override void OnExit()
    {
    }
}
