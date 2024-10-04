using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
    public AbilitySO SO;
    public float cooldownLeft=0;

    public bool IsEmpty() => SO==null;

    public bool IsCooling() => cooldownLeft>0;

    public void ResetCooldown() => cooldownLeft=0;

    public void DoCooldown() => cooldownLeft=SO.cooldown;

    public void UpdateCooldown()
    {
        if(IsCooling())
        {
            cooldownLeft -= Time.deltaTime;

            if(cooldownLeft<0)
            cooldownLeft=0;
        }
    }
};

// ============================================================================

[CreateAssetMenu]
public class AbilityListSO : ScriptableObject
{
    public List<Ability> abilities;

    // Getters ============================================================================

    public Ability GetAbility(AbilitySO abilitySO)
    {
        foreach(var abi in abilities)
        {
            if(abi.SO == abilitySO)
            {
                return abi;
            }
        }
        return null;
    }

    public Ability GetAbility(Ability ability)
    {
        foreach(var abi in abilities)
        {
            if(abi == ability)
            {
                return abi;
            }
        }
        return null;
    }

    public bool HasAbility(AbilitySO abilitySO, out Ability ability)
    {
        ability = GetAbility(abilitySO);

        return ability != null;
    }
    
    public bool HasAbility(Ability abi, out Ability ability)
    {
        ability = GetAbility(abi);

        return ability != null;
    }

    // Setters ============================================================================

    public void AddAbility(AbilitySO abilitySO)
    {
        if(HasAbility(abilitySO, out Ability ability))
        {
            Debug.Log($"Already have ability: {abilitySO.Name}");
            return;
        }

        Ability new_ability = new()
        {
            SO = abilitySO,
        };

        abilities.Add(new_ability);
    }

    public void RemoveAbility(Ability ability)
    {
        if(!abilities.Contains(ability)) return;

        abilities.Remove(ability);
    }

    public void RemoveAllAbilities()
    {
        abilities.Clear();
    }

    // Actions ============================================================================

    public void UpdateCooldowns()
    {
        foreach(var ability in abilities)
        {
            ability.UpdateCooldown();
        }
    }

    public void ResetCooldowns()
    {
        foreach(var ability in abilities)
        {
            ability.ResetCooldown();
        }
    }

    // Leftovers ============================================================================

    public void CleanUp()
    {
        abilities.RemoveAll(ability => ability.IsEmpty());
    }
}

