using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float length;
    private bool movingRight = true;
    public Transform groundDetection;
    public int curHealth;
    public int maxHealth;
    private GameMaster gm;
    private Player player;

    void Start()
    {
        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, length);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }

        if (curHealth <= 0)
        {
            
            gm.score += 5;
            Destroy(gameObject);
            PlayerPrefs.SetInt("Score", gm.score);
        }

    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");

    }

   private  void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.PlaySound("player_hit");
            player.Damage(1);
        }
    }
}
