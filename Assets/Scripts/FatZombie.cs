using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatZombie : MonoBehaviour
{
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


    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float frequency = 20f;

    [SerializeField]
    float magnitude = 0.5f;

    bool facingRight = true;

    Vector3 pos, localScale;

    // Use this for initialization
    void Start()
    {
        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        pos = transform.position;

        localScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {


        CheckWhereToFace();

        if (facingRight)
            MoveRight();
        else
            MoveLeft();

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

        }
    }

    void CheckWhereToFace()
    {
        if (pos.x < 2.5f)
            facingRight = true;

        else if (pos.x > 6.93f)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;

    }

    void MoveRight()
    {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
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
