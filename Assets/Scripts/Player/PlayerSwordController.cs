using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordController : MonoBehaviour
{
    [SerializeField] int weaponDamage = 1;
    [SerializeField] GameObject WeaponProjectile = null;
    [SerializeField] float cooldownTime = 10.0f;
    [SerializeField] float cooldownTimer = 0.0f;

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Projectile swordProjectile = Instantiate(WeaponProjectile, transform.position, Quaternion.identity, null).GetComponent<Projectile>();

                Health swordProjectileHealth = swordProjectile.GetComponent<Health>();
                swordProjectileHealth.entityID = GlobalManager.Instance.entityDictionary.Count;
                swordProjectileHealth.Initialize();

                swordProjectile.travelDirection = GlobalManager.Instance.playerAimController.PlayerCamera.transform.forward;
                swordProjectile.parentID = GlobalManager.Instance.playerAimController.gameObject.GetComponent<Health>().entityID;


                swordProjectile.transform.LookAt(swordProjectile.travelDirection + swordProjectile.transform.position, -GlobalManager.Instance.playerAimController.PlayerCamera.transform.right);
                cooldownTimer = cooldownTime;
            }
        }
    }
}
