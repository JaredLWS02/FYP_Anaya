using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Current;

    void Awake()
    {
        Current=this;
    }

    // ============================================================================

    public List<GameObject> characters = new();

    public void Register(GameObject obj)
    {
        if(!characters.Contains(obj))
        characters.Add(obj);
    }

    public void Unregister(GameObject obj)
    {
        if(characters.Contains(obj))
        characters.Remove(obj);
    }

    // ============================================================================

    [HideInInspector]
    public GameObject player;

    void Update()
    {
        if(characters.Count>0)
        {
            player = characters[0];
        }
        else player = null;
    }

    // ============================================================================

    public int GetIndex(GameObject obj)
    {
        int index=0;

        for(int i=0; i<characters.Count; i++)
        {
            if(obj == characters[i])
            {
                index=i;
            }
        }

        return index;
    }

    // ============================================================================
    
    bool canSwitch=true;
    public float switchCooldown=.5f;

    public void TrySwitch(GameObject switcher)
    {
        //if(MultiplayerManager.Current.players!=1) return;
        if(characters.Count<=1) return;

        if(!canSwitch) return;
        StartCoroutine(SwitchCoolingDown());

        int i = GetIndex(switcher);

        i++;

        if(i >= characters.Count)
        {
            i=0;
        }

        EventManager.Current.OnSwitchPilot(switcher, Pilot.Type.AI);
        EventManager.Current.OnSwitchPilot(characters[i], Pilot.Type.Player);

        Debug.Log($"Switched Player from {switcher.name} to {characters[i].name}");
    }

    IEnumerator SwitchCoolingDown()
    {
        canSwitch=false;
        yield return new WaitForSeconds(switchCooldown);
        canSwitch=true;
    }
}
