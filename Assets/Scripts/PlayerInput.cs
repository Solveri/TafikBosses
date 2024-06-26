using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float paddleWidth;
    private float minX;
    private float maxX;

    // Start is called before the first frame update
    void Start()
    {

        paddleWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;

        Vector3 leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 rightBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, Camera.main.nearClipPlane));

        minX = leftBoundary.x + paddleWidth;
        maxX = rightBoundary.x - paddleWidth;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPoision = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 newPos = new Vector2(touchPoision.x, transform.position.y);
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
          this.transform.position = newPos;
         
        }
        
    }
}
