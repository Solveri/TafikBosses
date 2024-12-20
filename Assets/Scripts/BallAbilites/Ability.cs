using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour,IAbility
{
  
    public BallController ball { get; protected set; } 
    public float duration { get; protected set; }
    public bool onCooldown { get; protected set; }
    public float CurrentCooldown { get; protected set; }
    public float MaxCooldown { get; protected set; }

    public virtual void Activate()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCooldownProgress()
    {
        return (CurrentCooldown / MaxCooldown); // Returns a value from 0 to 1
    }
}
