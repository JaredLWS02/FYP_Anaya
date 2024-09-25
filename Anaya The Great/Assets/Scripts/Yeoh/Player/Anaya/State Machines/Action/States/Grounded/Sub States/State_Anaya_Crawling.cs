using UnityEngine;

public class State_Anaya_Crawling : BaseState
{
    public override string Name => "Crawling";

    Anaya anaya;

    public State_Anaya_Crawling(StateMachine_Anaya sm)
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
        anaya.AllowStand = toggle;
    }
}
