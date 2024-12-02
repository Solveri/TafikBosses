using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInvController : MonoBehaviour
{
    [SerializeField] GameObject BallInv;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ChangeBallInvState()
    {
       
        if (BallInv!= null)
        {
            BallInv.SetActive(!BallInv.activeSelf);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
