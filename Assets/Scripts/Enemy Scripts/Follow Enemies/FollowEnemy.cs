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
    private float acceleration = 10;

    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float damage = 10;

    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float drag = 10;

    [SerializeField]
    [Range(0.0f, 1000.0f)]
    private float rotateSpeed = 10;

    private Vector3 direction = Vector3.zero;
    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        Debug.Log(rb.velocity.magnitude);
        UpdateMemoryTime();
        Debug.DrawLine(transform.position, transform.forward * sightDistance + transform.position, Color.blue);
        Debug.DrawLine(transform.position, (Quaternion.AngleAxis(fovAngle, transform.up) * (transform.forward * sightDistance)) + transform.position, Color.red);
        Debug.DrawLine(transform.position, (Quaternion.AngleAxis(-fovAngle, transform.up) * (transform.forward * sightDistance)) + transform.position, Color.red);

        if (CanSeeTarget() || remembersTarget || hasRecievedRelayInfo)
        {
            rb.drag = 0.05f;
            direction = target.transform.position - transform.position;
            direction.Normalize();

            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);

            Vector3 travelDirection = Vector3.Lerp(rb.velocity, transform.forward * speed, Time.deltaTime * acceleration);

            rb.velocity = travelDirection;
        }
        else
        {
            rb.drag = (drag);
        }
    }
}
