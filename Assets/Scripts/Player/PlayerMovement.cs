using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D playerRb;
    float movePower;
    float jumpPower;
    private bool jumpActivated;
    float horizontalInput;

    float shootCooltime;
    float shootElapsedTime;

    public GameObject playerBullet;
    public GameObject playerMuzzle;

    Animator animator;
    private int playerHealth;
    private int maxPlayerHealth;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movePower = 10.0f;
        jumpPower = 10.0f;
        horizontalInput = 0;
        maxPlayerHealth = 3;
        playerHealth = maxPlayerHealth;
    }

    void OnCollisionStay2D(Collision2D collision)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GetIsGameOver() || GameManager.instance.GetIsGameClear())
        {
            return;
        }

        if (transform.position.y <= -10)
        {
            if (!GameManager.instance.GetIsGameOver())
            {
                GameManager.instance.GameOver();
            }

            return;
        }

        if (playerHealth <= 0)
        {
            GameManager.instance.GameOver();
        }





        float yVelocity = playerRb.velocity.y;

        shootElapsedTime += Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && (yVelocity == 0))
        {
            jumpActivated = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (shootCooltime < shootElapsedTime)
            {
                GameObject firedBullet;

                firedBullet = Instantiate(playerBullet, playerMuzzle.transform.position, playerMuzzle.transform.rotation);

                Quaternion quaternion = transform.rotation;

                Debug.Log(transform.rotation);

                float direction = quaternion.y > 0 ? -1 : 1;

                firedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * direction, 0);

                animator.SetTrigger("isShoot");
                shootElapsedTime = 0;
            }
        }

        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            animator.SetBool("isWalk", true);

            if (horizontalInput < 0)
            {
                transform.rotation = new Quaternion(0, 1, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }
    void FixedUpdate()
    {
        if (jumpActivated)
        {
            playerRb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpActivated = !jumpActivated;
        }

        if (horizontalInput != 0)
        {
            playerRb.AddForce(new Vector2(horizontalInput, 0) * movePower);
        }
    }

    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;

        Debug.LogFormat("{0} received {1} damage, remain hp : {2}", this.name, damage, playerHealth);

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            GameManager.instance.GameOver();
        }
    }

    public void IncreaseHealth(int health)
    {
        playerHealth += health;

        playerHealth = Math.Min(playerHealth, maxPlayerHealth);

        Debug.LogFormat("{0} increase {1} health, remain hp : {2}", this.name, health, playerHealth);

    }
}
