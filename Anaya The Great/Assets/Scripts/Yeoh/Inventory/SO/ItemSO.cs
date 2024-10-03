using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public AnimatorOverrideController icon;

    public string Name;

    [TextArea]
    public string description;

    //public int maxStackSize=64;
    
    [HideInInspector]
    public int ID => GetInstanceID();
}
