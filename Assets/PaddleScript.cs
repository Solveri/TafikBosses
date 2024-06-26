using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [SerializeField] Transform ballSpawnPosition;
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBallInSpawnPoint(BallController ball)
    {
        Instantiate(ball, ballSpawnPosition.position, Quaternion.identity);

    }
    private void OnBallDestory()
    {
        
    }
}
