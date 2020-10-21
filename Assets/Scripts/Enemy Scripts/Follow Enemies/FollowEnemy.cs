using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : Enemy
{
    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float speed = 10;

    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float damage = 10;

    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float drag = 10;

    private Vector3 direction = Vector3.zero;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (CanSeeTarget())
        {
            rb.drag = 0.05f;
            direction = target.transform.position - transform.position;
            direction.Normalize();

            Vector3 velocity = direction * speed;

            rb.velocity = new Vector3(velocity.x, velocity.y, velocity.z);
        }
        else
        {
            rb.drag = (drag);
        }
    }
}
