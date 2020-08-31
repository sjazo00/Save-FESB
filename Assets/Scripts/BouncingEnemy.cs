using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingEnemy : MonoBehaviour
{
    Rigidbody2D ebody;
    public int curHealth;
    public int maxHealth;
    private GameMaster gm;
    private Player player;

    void Start()
    {
        curHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        ebody = this.GetComponent<Rigidbody2D>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            ebody.velocity = new Vector2(0, 0);
            ebody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
        }

        if (other.tag == "Player")
        {
            SoundManager.PlaySound("player_hit");
            other.GetComponent<Player>().Damage(1);
        }
    }

    void Update()
    {
        if (curHealth <= 0)
        {
            
            gm.score += 10;
            Destroy(gameObject);
            PlayerPrefs.SetInt("Score", gm.score);
            
        }
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");

    }
}


