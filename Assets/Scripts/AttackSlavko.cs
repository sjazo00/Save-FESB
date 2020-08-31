using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSlavko : MonoBehaviour {

    public ButtonRobot zombie1;
    public bool isLeft = false;

    void Start()
    {
        zombie1 = gameObject.GetComponent<ButtonRobot>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isLeft)
            {
                zombie1.Attack(false);

            }
            else
            {
                zombie1.Attack(true);
            }
        }

    }
}
