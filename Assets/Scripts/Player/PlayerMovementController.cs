using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] GameObject PlayerCamera = null;
    [SerializeField] GameObject PlayerCapsule = null;

    [SerializeField] float movementSpeed = 10.0f;

    [SerializeField] float groundedDistance = 1.25f;
    [SerializeField] LayerMask groundedLayerMask;

    private Vector3 movementDirection = Vector3.zero;
    private Rigidbody rb = null;

    bool isGrounded = true;
    int availableAerialMoves = 2;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Health>().Initialize();
    }

    void Update()
    {
        CheckGrounded();

        if (isGrounded)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                movementDirection = PlayerCamera.transform.forward;
            }
        }
        else
        {
            PlayerCapsule.transform.rotation = Quaternion.FromToRotation(Vector3.up, PlayerCamera.transform.forward);
            if (Input.GetKeyUp(KeyCode.Space) && availableAerialMoves > 0)
            {
                movementDirection = PlayerCamera.transform.forward;
                availableAerialMoves--;
            }
            if (Input.GetKeyUp(KeyCode.W) && availableAerialMoves > 0)
            {
                movementDirection = PlayerCamera.transform.up;
                availableAerialMoves--;
            }
            if (Input.GetKeyUp(KeyCode.D) && availableAerialMoves > 0)
            {
                movementDirection = PlayerCamera.transform.right;
                availableAerialMoves--;
            }
            if (Input.GetKeyUp(KeyCode.S) && availableAerialMoves > 0)
            {
                movementDirection = -PlayerCamera.transform.up;
                availableAerialMoves--;
            }
            if (Input.GetKeyUp(KeyCode.A) && availableAerialMoves > 0)
            {
                movementDirection = -PlayerCamera.transform.right;
                availableAerialMoves--;
            }
        }

        rb.velocity = (movementDirection * movementSpeed * Time.deltaTime);
    }

    public void CheckGrounded()
    {
        Debug.DrawLine(PlayerCapsule.transform.position, -PlayerCapsule.transform.up * groundedDistance + PlayerCapsule.transform.position, Color.red);
        if (Physics.Raycast(PlayerCapsule.transform.position, -PlayerCapsule.transform.up, groundedDistance, groundedLayerMask))
        {
            isGrounded = true;
            availableAerialMoves = 2;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // This is determining when we've hit something to check if it is from the grounded layers. if it is, we rotate the player with it.
        if (groundedLayerMask == (groundedLayerMask | (1 << collision.gameObject.layer)))
        {
            var point = collision.contacts[0].point;
            var dir = -collision.contacts[0].normal;

            point -= dir;

            RaycastHit hitInfo;

            if (collision.collider.Raycast(new Ray(point, dir), out hitInfo, 2))
            {
                // This rotates the player to be aligned with the normal of whatever they land on.
                PlayerCapsule.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
                movementDirection = Vector3.zero;
                rb.velocity = Vector3.zero;
            }
        }
    }
}
