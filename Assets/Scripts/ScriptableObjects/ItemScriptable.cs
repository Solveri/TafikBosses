using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemScriptable : ScriptableObject
{
    public Sprite Sprite;
    public string Name;
    public string Description;
    public ItemType Type;

    public AbilityScriptable AbilityScriptable;
    
    

   
}
