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
        anaya.AllowMoveY = toggle;
        anaya.AllowJump = toggle;
        anaya.AllowDash = toggle;
        anaya.AllowSwitch = toggle;
    }
}
