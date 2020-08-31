using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    float dirX, moveSpeed = 3f;
    bool moveRight = true;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 26.32f)
            moveRight = false;
        if (transform.position.x < 21.88f)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
    }
}
