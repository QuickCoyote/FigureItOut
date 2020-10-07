using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 travelDirection = Vector3.zero;

    [SerializeField] float projectileSpeed = 10.0f;
    [SerializeField] int projectileDamage = 1;

    public GameObject parent = null;

    void Update()
    {
        transform.position += travelDirection * projectileSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject != parent)
        {
            Health collidedHealth = collision.gameObject.GetComponent<Health>();

            if(collidedHealth != null)
            {
                collidedHealth.TakeDamage(projectileDamage);
            }
        }
    }
}
