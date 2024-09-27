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
    
    public void TrySwitch(GameObject switcher)
    {
        if(Singleton.Current.players!=1) return;
        if(characters.Count<=1) return;

        int i = GetIndex(switcher);

        i++;

        if(i >= characters.Count)
        {
            i=0;
        }

        EventManager.Current.OnPlayerSwitch(switcher, characters[i]);

        Debug.Log($"Switched from {switcher.name} to {characters[i].name}");
    }
}
