using UnityEngine;

public class State_Anaya_MidAir : BaseState
{
    public override string Name => "MidAir";

    Anaya anaya;

    public State_Anaya_MidAir(StateMachine_Anaya sm)
    {
        anaya = sm.anaya;
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anaya.gameObject.name} State: {Name}");

        anaya.AllowMoveX = true;
        anaya.AllowMoveY = false;
        anaya.AllowJump = true;
        anaya.AllowDash = true;
        anaya.AllowClimb = true;
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
