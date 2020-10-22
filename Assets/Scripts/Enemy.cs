using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public bool hasRecievedRelayInfo = false;
    public float fovAngle = 45f;
    public float sightDistance = 25f;
    public float maxMemoryTime = 5.0f;

    public bool remembersTarget = false;

    public GameObject target = null;

    private float memoryTime = 5.0f;

    public void UpdateMemoryTime()
    {
        memoryTime -= Time.deltaTime;

        if(memoryTime <= 0)
        {
            remembersTarget = false;
        }
    }

    public bool CanSeeTarget()
    {
        Vector3 newVector = target.transform.position - transform.position;

        float angle = Vector3.Angle(transform.forward, newVector);

        if (angle < fovAngle && angle > -fovAngle)
        {
            if (newVector.magnitude <= sightDistance)
            {
                RaycastHit hitInfo;
                Physics.Raycast(transform.position, newVector, out hitInfo, sightDistance);

                if (hitInfo.transform != null)
                {
                    if (hitInfo.transform.gameObject == target)
                    {
                        remembersTarget = true;
                        memoryTime = maxMemoryTime;
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
