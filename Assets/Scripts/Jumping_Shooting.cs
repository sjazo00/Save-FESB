using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping_Shooting : MonoBehaviour
{
    //integers
    public int curHealth;
    public int maxHealth;
    public int LevelToLoad;
    //float
    public float distance;
    public float wakeRange;
    public float shootInterval;
    public float bulletSpeed = 100;
    public float bulletTimer;
    //booleans
    public bool lookingRight = true;
    //references
    public GameObject bullet;
    public Transform target;
    public Transform shootPointLeft;
    public Transform shootPointRight;
    private GameMaster gm;
    Rigidbody2D ebody;
    private Player player;


    void Start()
    {
        curHealth = maxHealth;
        ebody = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
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

        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < wakeRange)
        {
            Attack(lookingRight);
        }

        if (target.transform.position.x > transform.position.x)
        {
            lookingRight = true;
        }

        if (target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }

        if (curHealth <= 0)
        {
            
            gm.score += 10;
            Destroy(gameObject);
            PlayerPrefs.SetInt("Score", gm.score);
            Application.LoadLevel(LevelToLoad);
        }
    }

    public void Attack(bool attackingRight)
    {
        bulletTimer += Time.deltaTime;

        if (bulletTimer >= shootInterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();

            if (!attackingRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;

            }
            if (attackingRight)
            {
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;

            }

        }
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");

    }
}
