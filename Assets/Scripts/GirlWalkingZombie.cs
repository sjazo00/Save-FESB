using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlWalkingZombie : MonoBehaviour
{
    float dirX;
    private GameMaster gm;
    [SerializeField]
    float moveSpeed = 3f;
    public int curHealth;
    public int maxHealth;
    Rigidbody2D rb;
    bool facingRight = false;

    Vector3 localScale;

    public static bool isAttacking = false;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirX = -1f;
        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 12.8f)
            dirX = 1f;
        else if (transform.position.x > 18.4f)
            dirX = -1f;

        if (isAttacking)
            anim.SetBool("isAttacking", true);
        else
            anim.SetBool("isAttacking", false);

        if (curHealth <= 0)
        {
            
            gm.score += 10;
            Destroy(gameObject);
            PlayerPrefs.SetInt("Score", gm.score);
        }

    }

    void FixedUpdate()
    {
        if (!isAttacking)
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        else
            rb.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");

    }
}
