using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public Vector3 travelDirection = Vector3.zero;

    [SerializeField] float projectileSpeed = 10.0f;
    [SerializeField] int projectileDamage = 1;

    [SerializeField] int destructableObjectLayer;

    public int parentID = 0;

    Health hp = null;
    Rigidbody rb = null;

    private void Start()
    {
        hp = GetComponent<Health>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.position += travelDirection * projectileSpeed * Time.deltaTime;
        rb.WakeUp();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided");
        Health CollidedHealth = collision.gameObject.GetComponent<Health>();

        if (CollidedHealth != null)
        {
            if (CollidedHealth.entityID != parentID)
            {
                if (CollidedHealth.gameObject.layer == destructableObjectLayer)
                {
                    if (CollidedHealth.currentHealth > hp.currentHealth)
                    {
                        CollidedHealth.TakeDamage(hp.currentHealth);
                        hp.TakeDamage(hp.currentHealth);
                    }
                    else
                    {
                        hp.TakeDamage(CollidedHealth.currentHealth);
                        CollidedHealth.TakeDamage(CollidedHealth.currentHealth);
                    }
                }
                CollidedHealth.TakeDamage(1);
            }
            return;
        }

        hp.TakeDamage(hp.currentHealth);
    }
}
