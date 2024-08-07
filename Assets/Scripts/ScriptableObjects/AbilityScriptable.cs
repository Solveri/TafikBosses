using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Ability", order = 1)]
public class AbilityScriptable : ScriptableObject
{
    public Sprite background;
    public Sprite banner;
    public Sprite activated;
}
