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
        anaya.AllowClimb = toggle;
        anaya.AllowSwitch = toggle;
    }
}
