using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCone : MonoBehaviour {

    public TurretAl turretAl;
    public bool isLeft = false;

    void Awake()
    {
        turretAl = gameObject.GetComponent<TurretAl>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isLeft)
            {
                turretAl.Attack(false);

            }
            else
            {
                turretAl.Attack(true);
            }
        }

    }
}
