using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingZombieAttack : MonoBehaviour
{
    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            WalkingZombie.isAttacking = true;
           
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            WalkingZombie.isAttacking = false;
        }
    }
}
