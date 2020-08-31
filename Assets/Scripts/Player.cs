using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    //float
    public float speed = 50f;
    public float jumpPower = 150f;
    public float maxSpeed = 3;
    //booleans
    public bool grounded;
    public bool canDoubleJump;
    public bool wallSliding;
    public bool facingRight = true;
    //references
    private Rigidbody2D rb2d;
    private Animator anim;
    private GameMaster gm;
    public Transform wallCheckPoint;
    public bool wallCheck;
    public LayerMask wallLayerMask;
    public Joystick joystick;
    //stats
    public int curHealth;
    public int maxHealth=5;
    public int score;
    public Text pointsText;


	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed",Mathf.Abs(rb2d.velocity.x));

        if (joystick.Horizontal < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }

        if (joystick.Horizontal > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

        if (grounded)
        {
            canDoubleJump = true;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump") && !wallSliding)
        {
            if (grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                    rb2d.AddForce(Vector2.up * jumpPower);
                }
            }
        }

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0)
        {
            Die();
            PlayerPrefs.DeleteKey("Score");
            score = 0;
            pointsText.text = ("Points: " + score);
        }

        if (!grounded && rb2d.velocity.y < 0.1f)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);
            if (facingRight && joystick.Horizontal > 0.1f || !facingRight && joystick.Horizontal < 0.1f)
            {
                if (wallCheck)
                {
                    HandleWallSliding();
                }

            }
        }

        if(wallCheck==false || grounded)
        {
            wallSliding = false;
        }
	}

    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7f);
        wallSliding = true;

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (facingRight)
            {
                rb2d.AddForce(new Vector2(-1.5f, 2.5f) * jumpPower);
            }
            else
            {
                rb2d.AddForce(new Vector2(1.5f, 2.5f) * jumpPower);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        float h = joystick.Horizontal;
        //fake friction/Easing the x speed of our player
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }
        //Moving the player
        if (grounded && h>=.2f) 
        {
            rb2d.AddForce((Vector2.right * speed) * h);
        }
        else
        {
            rb2d.AddForce((Vector2.right * speed/2) * h);
        }
        //Limiting player speed
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed,rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }

    void Die()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("Player_RedFlash");
    }

    public IEnumerator Knockback(float knockDur,float knockbackPwr,Vector3 knockbackDir)
    {
        float timer = 0;
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            rb2d.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y + knockbackPwr, transform.position.z));
        }

        yield return 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            
            gm.score += 1;
            Destroy(col.gameObject);
            SoundManager.sndMan.PlayCoinSound();
        }

        if (col.gameObject.CompareTag("Hand"))
        {
            SoundManager.PlaySound("player_hit");
            curHealth -= 1;
        }

        if (col.gameObject.CompareTag("Heart"))
        {
            
            curHealth += 1;
            Destroy(col.gameObject);
            SoundManager.sndMan.PlayCoinSound();
        }

        if (col.gameObject.CompareTag("Star_Coin"))
        {

            gm.score += 10;
            Destroy(col.gameObject);
            SoundManager.sndMan.PlayCoinSound();
        }

        if (col.gameObject.CompareTag("Speed"))
        {

            speed += 25f;
            Destroy(col.gameObject);
            SoundManager.sndMan.PlayCoinSound();
        }

        if (col.gameObject.CompareTag("JumpPower"))
        {

            jumpPower += 50f;
            Destroy(col.gameObject);
            SoundManager.sndMan.PlayCoinSound();
        }

    }
}
