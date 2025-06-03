using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public LayerMask playerLayerMask;

    float sightRange;

    Transform targetTransform;
    Animator animator;
    private bool isChasing;

    private bool isActivated;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sightRange = 5;
        isActivated = true;
    }

    void Update()
    {
        if (isActivated)
        {
            DetectPlayer();

            if (isChasing)
            {
                ChasePlayer();
            }

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggerd");
        Deactivate();
    }

    void DetectPlayer()
    {
        Collider2D player;
        player = Physics2D.OverlapCircle(transform.position, sightRange, playerLayerMask);

        if (player != null)
        {
            animator.SetBool("isChase", true);
            isChasing = true;
        }
    }

    void ChasePlayer()
    {
        Collider2D player;
        player = Physics2D.OverlapCircle(transform.position, sightRange, playerLayerMask);

        if (isChasing)
        {
            targetTransform = player.transform;
            transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y + 5, targetTransform.position.z);

        }

    }

    public void Deactivate()
    {
        isActivated = false;
        isChasing = false;
        animator.SetBool("isChase", false);
    }

    public bool isDeactivated()
    {
        return !isActivated;
    }
}
