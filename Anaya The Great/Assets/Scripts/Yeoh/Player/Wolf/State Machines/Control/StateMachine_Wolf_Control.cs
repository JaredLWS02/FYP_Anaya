using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Wolf_Control : MonoBehaviour
{
    public Wolf wolf;

    // STATE MACHINE ================================================================================

    StateMachine sm;
    BaseState defaultState;

    void Awake()
    {
        sm = new StateMachine();
        
        // STATES ================================================================================

        State_Wolf_Control_None none = new(this);
        State_Wolf_Control_Player player = new(this);
        State_Wolf_Control_AI ai = new(this);

        // HUB TRANSITIONS ================================================================================

        none.AddTransition(player, (timeInState) =>
        {
            if(
                wolf.control == Wolf.Control.Player //&&
            ){
                return true;
            }
            return false;
        });

        none.AddTransition(ai, (timeInState) =>
        {
            if(
                wolf.control == Wolf.Control.AI //&&
            ){
                return true;
            }
            return false;
        });
        
        
        
        // RETURN TRANSITIONS ================================================================================

        player.AddTransition(none, (timeInState) =>
        {
            if(
                wolf.control != Wolf.Control.Player //||
            ){
                return true;
            }
            return false;
        });

        ai.AddTransition(none, (timeInState) =>
        {
            if(
                wolf.control != Wolf.Control.AI //||
            ){
                return true;
            }
            return false;
        });

        

        // DEFAULT ================================================================================
        
        defaultState = none;
        sm.SetInitialState(defaultState);
    }

    void Update()
    {
        sm.Tick(Time.deltaTime);
    }

    void OnDisable()
    {
        if(sm!=null)
        {
            sm.currentState.Exit(); // call OnExit on current state
            sm.SetState(defaultState); // Change back to default state
        }
    }
}
