using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    private Transform target;
    public int curHealth;
    public int maxHealth;
    public float distance;
    public float wakeRange;
    public float shootInterval;
    public float bulletSpeed = 100;
    public float bulletTimer;
    public bool lookingRight = true;
    public GameObject bullet;
    public Transform shootPointLeft;
    public Transform shootPointRight;
    private GameMaster gm;
    public int LevelToLoad;

    void Start()
    {
        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2.Distance(transform.position, target.position) > 3) && (Vector2.Distance(transform.position, target.position) < 6))
        {
            var targetPos = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

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
