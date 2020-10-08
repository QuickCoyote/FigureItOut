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
        Health CollidedHealth = collision.gameObject.GetComponent<Health>();

        if (CollidedHealth != null)
        {
            if (CollidedHealth.entityID != parentID)
            {
                Health collidedHealth = collision.gameObject.GetComponent<Health>();

                if (collidedHealth != null)
                {
                    if(collidedHealth.gameObject.layer == destructableObjectLayer)
                    {
                        if(collidedHealth.currentHealth > hp.currentHealth)
                        {
                            collidedHealth.TakeDamage(hp.currentHealth);
                            hp.currentHealth = 0;
                        }
                        else
                        {
                            hp.TakeDamage(collidedHealth.currentHealth);
                            collidedHealth.currentHealth = 0;
                        }
                    }
                    collidedHealth.TakeDamage(projectileDamage);
                }
            }
        }

        hp.TakeDamage(1);
        Debug.Log("Taking Damage");
    }
}
