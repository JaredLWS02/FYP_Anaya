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

        anaya.AllowMoveY = false;
        anaya.AllowJump = false;
        anaya.AllowDash = false;
        anaya.AllowClimb = false;
        anaya.AllowCrawl = false;
        anaya.AllowStand = true;
        anaya.AllowCommand = false;
    }

    protected override void OnUpdate(float deltaTime)
    {
    }

    protected override void OnExit()
    {
    }
}
