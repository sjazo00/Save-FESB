using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true & col.tag != ("Enemy"))
        {
            if (col.CompareTag("Player"))
            {
                SoundManager.PlaySound("player_hit");
                col.GetComponent<Player>().Damage(1);
            }
            Destroy(gameObject);
        }

    }
}
