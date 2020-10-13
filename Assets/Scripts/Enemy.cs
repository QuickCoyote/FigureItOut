using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public bool hasRecievedRelayInfo = false;
    public float fovAngle = 45f;
    public float sightDistance = 25f;

    public GameObject target = null;

    public bool CanSeeTarget()
    {
        Vector3 newVector = target.transform.position - transform.position;

        float angle = Vector3.Angle(transform.forward, target.transform.position);

        if (angle <= fovAngle && angle >= -fovAngle)
        {
            if (newVector.magnitude <= sightDistance)
            {
                // Now we need to raycast to see if we can see the target
                RaycastHit hitInfo;
                Physics.Raycast(transform.position, newVector.normalized, out hitInfo, sightDistance);

                if (hitInfo.transform != null)
                {
                    if (hitInfo.transform.gameObject == target)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        return false;
    }
}
