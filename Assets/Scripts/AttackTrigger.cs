using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public int dmg = 20;

    void OnTriggerEnter2D(Collider2D col)
    {
        if ( col.CompareTag("Enemy"))
        {
            SoundManager.PlaySound("enemydeath");
            col.SendMessageUpwards("Damage", dmg);
        }
    }
}
