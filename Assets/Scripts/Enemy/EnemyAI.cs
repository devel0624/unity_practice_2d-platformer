using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public int enemyHealth;
    Animator animator;

    public LayerMask playerLayerMask;
    float sightRange;
    private bool isAttacking;
    private float shootElapsedTime;
    private float shootCoolTime;
    public GameObject enemyBullet;
    Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = 3;
        animator = GetComponent<Animator>();
        sightRange = 2;
        isAttacking = false;
        shootCoolTime = 2;
        shootElapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (isAttacking)
        {
            shootElapsedTime += Time.deltaTime;

            if (shootCoolTime < shootElapsedTime)
            {
                isAttacking = false;
                shootElapsedTime = 0;
                animator.SetBool("isDetect", true);
            }
        }
        else
        {
            DetectPlayer();
        }
    }

    void DetectPlayer()
    {
        Collider2D player;
        player = Physics2D.OverlapCircle(transform.position, sightRange, playerLayerMask);

        if (player != null)
        {
            targetTransform = player.transform;
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {

        animator.SetTrigger("isShoot");
         
        Debug.Log("attack");

        int facingRight;

        if (targetTransform.position.x <= transform.position.x)
        {
            transform.rotation = new Quaternion(0, 1, 0, 0);
            facingRight = -1;
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            facingRight = 1;
        }

        GameObject bullet;
        bullet = Instantiate(enemyBullet, transform.position, transform.rotation);

        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * facingRight, 0);

        isAttacking = true;
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        Debug.LogFormat("{0} received {1} damage, remain hp : {2}", this.name, damage, enemyHealth);

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
