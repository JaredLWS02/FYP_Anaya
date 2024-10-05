using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : MonoBehaviour
{
    public AbilitySO healAbility;

    public HPManager hp;
    public float healAmount=20;

    // Event Manager ============================================================================

    void OnEnable()
    {
        //EventManager.Current.CastReleaseEvent += OnCastRelease;
        //temp, no anim event yet
        EventManager.Current.CastWindUpEvent += OnCastRelease;
        EventManager.Current.CastHealEvent += OnCastHeal;
    }
    void OnDisable()
    {
        //EventManager.Current.CastReleaseEvent -= OnCastRelease;
        //temp, no anim event yet
        EventManager.Current.CastWindUpEvent -= OnCastRelease;
        EventManager.Current.CastHealEvent -= OnCastHeal;
    }

    // Events ============================================================================

    void OnCastRelease(GameObject caster, Ability ability)
    {
        if(caster!=gameObject) return;

        if(ability.SO!=healAbility) return;

        EventManager.Current.OnCastHeal(gameObject);

        //temp, no anim event yet
        EventManager.Current.OnCastRelease(gameObject, ability);
        EventManager.Current.OnCastFinish(gameObject);

        //DisableCastTrails();

        //AudioManager.Current.PlaySFX(SFXManager.Current.sfxHeal1, transform.position);
        //AudioManager.Current.PlaySFX(SFXManager.Current.sfxHeal2, transform.position);
    }

    void OnCastHeal(GameObject caster)
    {
        if(caster!=gameObject) return;

        hp.Add(healAmount);

        TempVFX();
    }

    // Move to vfx manager later ============================================================================

    void TempVFX()
    {
        // flash sprite green
        SpriteManager.Current.FlashColor(gameObject, -1, 1, -1);

        Vector3 top = SpriteManager.Current.GetColliderTop(gameObject);

        // pop up text
        VFXManager.Current.SpawnPopUpText(top, $"+{healAmount}", Color.green);
    }
}
