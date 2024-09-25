using UnityEngine;

public class State_Anaya_Control_AI : BaseState
{
    public override string Name => "AI Controls";

    Anaya anaya;

    // SUB STATE MACHINE ================================================================================

    BaseState defaultSubState;

    public State_Anaya_Control_AI(StateMachine_Anaya_Control sm)
    {
        anaya = sm.anaya;

        subsm = new StateMachine();
        
        // SUB STATES ================================================================================

        State_Anaya_Control_AI_Idle idle = new(sm);
        State_Anaya_Control_AI_Seeking seeking = new(sm);
        State_Anaya_Control_AI_Fleeing fleeing = new(sm);
        State_Anaya_Control_AI_Staying staying = new(sm);

        // HUB TRANSITIONS ================================================================================

        idle.AddTransition(seeking, (timeInState) =>
        {
            // if(
            //     &&
            // ){
            //     return true;
            // }
            return false;
        });
        
        idle.AddTransition(fleeing, (timeInState) =>
        {
            // if(
            //     &&
            // ){
            //     return true;
            // }
            return false;
        });
        
        idle.AddTransition(staying, (timeInState) =>
        {
            // if(
            //     &&
            // ){
            //     return true;
            // }
            return false;
        });
        
        
        
        // RETURN TRANSITIONS ================================================================================

        seeking.AddTransition(idle, (timeInState) =>
        {
            // if(
            //     ||
            // ){
            //     return true;
            // }
            return false;
        });
        
        fleeing.AddTransition(idle, (timeInState) =>
        {
            // if(
            //     ||
            // ){
            //     return true;
            // }
            return false;
        });
        
        staying.AddTransition(idle, (timeInState) =>
        {
            // if(
            //     ||
            // ){
            //     return true;
            // }
            return false;
        });
        
        

        // DEFAULT ================================================================================
        
        defaultSubState = idle;
        subsm.SetInitialState(defaultSubState);
    }

    protected override void OnEnter()
    {
        Debug.Log($"{anaya.gameObject.name} State: {Name}");

        anaya.AllowPlayer = false;
        anaya.AllowAI = true;
    }

    protected override void OnUpdate(float deltaTime)
    {
    }

    protected override void OnExit()
    {
    }
}
