using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : Enemy
{
    [SerializeField] GameObject turretBase = null;
    [SerializeField] GameObject turretHead = null;
    [SerializeField] GameObject turretBarrelTip = null;
    [SerializeField] GameObject turretProjectile = null;

    [Header("Turret Aiming Variables")]
    [SerializeField] float turretBaseRotationRate = 10.0f;
    [SerializeField] float turretHeadRotationRate = 10.0f;
    [SerializeField] float turretMaxAngle = 45.0f;
    [SerializeField] float acceptableAngleToShoot = 3f;
    [SerializeField] float firingCooldown = 0.0f;
    [SerializeField] float firingCooldownTime = 0.0f;

    private Quaternion qTurret = Quaternion.identity;
    private Quaternion qGun = Quaternion.identity;
    private Quaternion qGunStart = Quaternion.identity;

    void Start()
    {
        qGunStart = turretHead.transform.localRotation;
    }

    void Update()
    {
        if(firingCooldown > 0)
        {
            firingCooldown -= Time.deltaTime;
        }
        if(CanSeeTarget() || hasRecievedRelayInfo || remembersTarget)
        {
            if (target != null)
            {
                float distanceToPlane = Vector3.Dot(transform.up, target.transform.position - transform.position);
                Vector3 planePoint = target.transform.position - transform.up * distanceToPlane;

                qTurret = Quaternion.LookRotation(planePoint - transform.position, transform.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, qTurret, turretBaseRotationRate * Time.deltaTime);

                float tempDist = Vector3.Dot(transform.up, target.transform.position - turretBarrelTip.transform.position);
                Vector3 tempPlanePoint = target.transform.position - turretBarrelTip.transform.position * tempDist;

                Vector3 v3 = new Vector3(0.0f, tempDist, (tempPlanePoint - turretBarrelTip.transform.position).magnitude);
                qGun = Quaternion.LookRotation(v3);

                float angleVal = Quaternion.Angle(qGunStart, qGun);

                if (angleVal <= turretMaxAngle)
                {
                    Debug.Log("THREE DEGREES");
                    turretHead.transform.localRotation = Quaternion.RotateTowards(turretHead.transform.localRotation, qGun, turretHeadRotationRate * Time.deltaTime);
                    if(angleVal <= acceptableAngleToShoot && firingCooldown <= 0)
                    {
                        Projectile turret_Projectile = Instantiate(turretProjectile, turretBarrelTip.transform.position, Quaternion.identity, null).GetComponent<Projectile>();

                        Health swordProjectileHealth = turret_Projectile.GetComponent<Health>();
                        swordProjectileHealth.entityID = GlobalManager.Instance.entityDictionary.Count;
                        swordProjectileHealth.Initialize();

                        turret_Projectile.travelDirection = (turretBarrelTip.transform.position - target.transform.position).normalized;
                        turret_Projectile.parentID = gameObject.GetComponent<Health>().entityID;

                        turret_Projectile.transform.LookAt(turret_Projectile.travelDirection + turret_Projectile.transform.position, turretBarrelTip.transform.right);
                        firingCooldown = firingCooldownTime;
                    }
                }
                else
                {
                    Debug.Log("Gun Angle: " + angleVal);
                    Debug.Log("Target beyond gun range");
                }
            }
        }

        Debug.DrawLine(turretBase.transform.position, turretBase.transform.forward * sightDistance + turretBase.transform.position, Color.blue);
        Debug.DrawLine(turretBase.transform.position, (Quaternion.AngleAxis(fovAngle, turretBase.transform.up) * (turretBase.transform.forward * sightDistance)) + turretBase.transform.position, Color.red);
        Debug.DrawLine(turretBase.transform.position, (Quaternion.AngleAxis(-fovAngle, turretBase.transform.up) * (turretBase.transform.forward * sightDistance)) + turretBase.transform.position, Color.red);
        Debug.DrawLine(turretHead.transform.position, (Quaternion.AngleAxis(turretMaxAngle, turretHead.transform.right) * (turretHead.transform.forward * sightDistance)) + turretHead.transform.position, Color.yellow);
        Debug.DrawLine(turretHead.transform.position, (Quaternion.AngleAxis(-turretMaxAngle, turretHead.transform.right) * (turretHead.transform.forward * sightDistance)) + turretHead.transform.position, Color.yellow);

        Debug.DrawLine(turretHead.transform.position, target.transform.position, Color.green);
    }
}
