using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    public Joystick joystick;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (joystick.Vertical <= -.2f)
        {
            waitTime = 0.5f;
        }

        if (joystick.Vertical >= .2f)
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            effector.rotationalOffset=0;
        }

    }
}
