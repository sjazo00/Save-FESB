using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour
{
    float dirY, moveSpeed = 3f;
    bool moveUp = true;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 12.9f)
            moveUp = false;
        if (transform.position.y < 9.5f)
            moveUp = true;

        if (moveUp)
            transform.position = new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
    }
}
