using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBigHeadZombie : MonoBehaviour
{
    public float rotationSpeed;
    public float distance;
    private Player player;
    public int curHealth;
    public int maxHealth;
    public LineRenderer lineOfSight;
    public Gradient redColor;
    public Gradient greenColor;
    private GameMaster gm;
    

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            lineOfSight.SetPosition(1, hitInfo.point);
            lineOfSight.colorGradient = redColor;

            if (hitInfo.collider.CompareTag("Player"))
            {
                SoundManager.PlaySound("player_hit");
                player.Damage(1);
            }

        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
            lineOfSight.SetPosition(1, transform.position + transform.right * distance);
            lineOfSight.colorGradient = greenColor;
        }

        lineOfSight.SetPosition(0, transform.position);

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
