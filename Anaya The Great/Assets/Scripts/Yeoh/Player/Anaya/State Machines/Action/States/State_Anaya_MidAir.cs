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
    }

    protected override void OnUpdate(float deltaTime)
    {
        ToggleAllow(true);
    }

    protected override void OnExit()
    {
        ToggleAllow(false);
    }

    void ToggleAllow(bool toggle)
    {
        anaya.AllowMoveX = toggle;
        anaya.AllowJump = toggle; // for double jump
        anaya.AllowDash = toggle;
        anaya.AllowClimb = toggle;
        anaya.AllowSwitch = toggle;
    }
}
