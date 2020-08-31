using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlWalkingZombieAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            GirlWalkingZombie.isAttacking = true;

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            GirlWalkingZombie.isAttacking = false;
        }
    }
}
