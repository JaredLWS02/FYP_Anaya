using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCaster : MonoBehaviour
{
    public AbilityListSO abilityList;

    public HPManager MP_Manager;

    [HideInInspector]
    public bool isCasting;

    // Event Manager ============================================================================

    void OnEnable()
    {
        abilityList.ResetCooldowns();

        EventManager.Current.StartCastEvent += OnStartCast;
        EventManager.Current.CastingEvent += OnCasting;
        EventManager.Current.CastWindUpEvent += OnCastWindUp;
        EventManager.Current.CastReleaseEvent += OnCastRelease;
        EventManager.Current.CastFinishEvent += OnCastFinish;
        EventManager.Current.CastCancelEvent += OnCastCancel;
    }
    void OnDisable()
    {
        abilityList.ResetCooldowns();

        EventManager.Current.StartCastEvent -= OnStartCast;
        EventManager.Current.CastingEvent -= OnCasting;
        EventManager.Current.CastWindUpEvent -= OnCastWindUp;
        EventManager.Current.CastReleaseEvent -= OnCastRelease;
        EventManager.Current.CastFinishEvent -= OnCastFinish;
        EventManager.Current.CastCancelEvent -= OnCastCancel;
    }

    // Start ============================================================================
    
    void OnStartCast(GameObject caster, AbilitySO abilitySO)
    {
        if(caster!=gameObject) return;

        if(isCasting) return;

        // if got that ability
        if(abilityList.HasAbility(abilitySO, out Ability ability))
        {
            // not on cooldown
            if(ability.IsCooling()) return;

            // not enough mp
            if(MP_Manager.hp < abilitySO.cost) return;

            EventManager.Current.OnCasting(gameObject, ability);
        }
    }

    // Casting ============================================================================

    void OnCasting(GameObject caster, Ability ability)
    {
        if(caster!=gameObject) return;

        castingRt = StartCoroutine(Casting(ability));
    }

    Coroutine castingRt;
    IEnumerator Casting(Ability ability)
    {
        isCasting=true;

        //sfxCastingLoop = AudioManager.Current.LoopSFX(gameObject, SFXManager.Current.sfxCastingLoop);

        yield return new WaitForSeconds(ability.SO.castingTime);

        EventManager.Current.OnCastWindUp(gameObject, ability);

        //if(sfxCastingLoop) AudioManager.Current.StopLoop(sfxCastingLoop);
    }

    // Done ============================================================================

    void OnCastWindUp(GameObject caster, Ability ability)
    {
        if(caster!=gameObject) return;

        // play ability.anim or something
        // it must have anim event to trigger EventManager.Current.OnCastRelease
    }
    
    // Release ============================================================================

    void OnCastRelease(GameObject caster, Ability ability)
    {
        if(caster!=gameObject) return;

        // mp cost
        MP_Manager.Hurt(ability.SO.cost);

        ability.DoCooldown();
    }

    // Finish ============================================================================

    void OnCastFinish(GameObject caster)
    {
        if(caster!=gameObject) return;

        isCasting=false;
    }

    // Cancel ============================================================================

    public void Cancel()
    {
        EventManager.Current.OnCastCancel(gameObject);
    }

    void OnCastCancel(GameObject caster)
    {
        if(caster!=gameObject) return;

        if(!isCasting) return;
        isCasting=false;

        if(castingRt!=null) StopCoroutine(castingRt);

        //if(sfxCastingLoop) AudioManager.Current.StopLoop(sfxCastingLoop);
    }

    // Misc ============================================================================

    void Update()
    {
        abilityList.UpdateCooldowns();
        abilityList.CleanUp();
    }

    /*
    //AudioSource sfxCastingLoop;

    public void OnDeath()
    {
        Cancel();
    }

    */
}
