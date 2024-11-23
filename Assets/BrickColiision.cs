using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickColiision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enter");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Exit");

    }
}
