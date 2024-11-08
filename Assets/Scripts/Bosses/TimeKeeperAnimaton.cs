using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeKeeperAnimaton : MonoBehaviour
{
    Animator ani;  
    // Start is called before the first frame update
    private void OnEnable()
    {
        BossHealth.onHit += playOnHit;
        TimeKeeperHealth.onEventEnd += stopAnimation;
        BossHealth.onEvent += playOnEvent;
    }
    private void Start()
    {
        
        ani = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        BossHealth.onHit -= playOnHit;
        BossHealth.onEvent -= playOnEvent;
        TimeKeeperHealth.onEventEnd -= stopAnimation;

    }

    void playOnHit()
    {
        ani.SetTrigger("Hit");
    }
    void playOnEvent()
    {
        ani.SetBool("IsInEvent",true);
    }
   void stopAnimation()
    {
        ani.SetBool("IsInEvent", false);
    }
}
