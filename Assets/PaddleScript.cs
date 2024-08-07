using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour
{
    [SerializeField] Transform ballSpawnPosition;
    private void Awake()
    {
        if (FindObjectsOfType<PaddleScript>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("PaddlePositionX") && PlayerPrefs.HasKey("PaddlePositionY"))
        {
            float posX = PlayerPrefs.GetFloat("PaddlePositionX");
            float posY = PlayerPrefs.GetFloat("PaddlePositionY");
            transform.position = new Vector2(posX, posY);
        }
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat("PaddlePositionX", transform.position.x);
        PlayerPrefs.SetFloat("PaddlePositionY", transform.position.y);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBallInSpawnPoint(BallController ball)
    {
       GameManager.instance.currentBall = Instantiate(ball, ballSpawnPosition.position, Quaternion.identity);
        Debug.Log("Ball has spawned");

    }
    
}
