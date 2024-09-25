using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Anaya : MonoBehaviour
{
    public Anaya anaya;

    // STATE MACHINE ================================================================================

    StateMachine sm;
    BaseState defaultState;

    void Awake()
    {
        sm = new StateMachine();
        
        // STATES ================================================================================

        State_Hub hub = new();
        State_Anaya_Grounded grounded = new(this);
        State_Anaya_MidAir midair = new(this);
        State_Anaya_Dashing dashing = new(this);
        State_Anaya_Climbing climbing = new(this);

        // HUB TRANSITIONS ================================================================================

        hub.AddTransition(grounded, (timeInState) =>
        {
            // if(
            //     &&
            // ){
            //     return true;
            // }
            return false;
        });

        hub.AddTransition(midair, (timeInState) =>
        {
            // if(
            //     &&
            // ){
            //     return true;
            // }
            return false;
        });
        
        hub.AddTransition(dashing, (timeInState) =>
        {
            // if(
            //     &&
            // ){
            //     return true;
            // }
            return false;
        });
        
        hub.AddTransition(climbing, (timeInState) =>
        {
            // if(
            //     &&
            // ){
            //     return true;
            // }
            return false;
        });
        
        
        
        // RETURN TRANSITIONS ================================================================================

        grounded.AddTransition(hub, (timeInState) =>
        {
            // if(
            //     ||
            // ){
            //     return true;
            // }
            return false;
        });

        midair.AddTransition(hub, (timeInState) =>
        {
            // if(
            //     ||
            // ){
            //     return true;
            // }
            return false;
        });

        dashing.AddTransition(hub, (timeInState) =>
        {
            // if(
            //     ||
            // ){
            //     return true;
            // }
            return false;
        });

        climbing.AddTransition(hub, (timeInState) =>
        {
            // if(
            //     ||
            // ){
            //     return true;
            // }
            return false;
        });

        

        // DEFAULT ================================================================================
        
        defaultState = hub;
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
