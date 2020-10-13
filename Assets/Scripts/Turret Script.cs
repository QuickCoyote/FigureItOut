using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] GameObject target = null;

    [SerializeField] GameObject turretBase = null;
    [SerializeField] GameObject turretHead = null;
    [SerializeField] float turretBaseRotationRate = 10.0f;
    [SerializeField] float turretHeadRotationRate = 10.0f;
    [SerializeField] float turretMaxAngle = 45.0f;

    private Quaternion qTurret = Quaternion.identity;
    private Quaternion qGun = Quaternion.identity;
    private Quaternion qGunStart = Quaternion.identity;

    void Start()
    {
        qGunStart = turretHead.transform.localRotation;
    }

    void Update()
    {
        if((transform.position - GlobalManager.Instance.playerAimController.transform.position).magnitude < sightRange || )
        {

        }

        if(target != null)
        {
            float distanceToPlane = Vector3.Dot(transform.up, target.transform.position - transform.position);
            Vector3 planePoint = target.transform.position - transform.up * distanceToPlane;

            qTurret = Quaternion.LookRotation(planePoint - transform.position, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qTurret, turretBaseRotationRate * Time.deltaTime);

            Vector3 v3 = new Vector3(0.0f, distanceToPlane, (planePoint - transform.position).magnitude);
            qGun = Quaternion.LookRotation(v3);

            if (Quaternion.Angle(qGunStart, qGun) <= turretMaxAngle)
                turretHead.transform.localRotation = Quaternion.RotateTowards(turretHead.transform.localRotation, qGun, turretHeadRotationRate * Time.deltaTime);
            else
                Debug.Log("Target beyond gun range");
        }
    }
}
