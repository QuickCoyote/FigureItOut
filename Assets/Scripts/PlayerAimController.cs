using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [SerializeField] GameObject PlayerCamera = null;
    [SerializeField] float horizontalSensitivity = 2.0f;
    [SerializeField] float verticalSensitivity = 2.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontalValue = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        float verticalValue = -Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;

        PlayerCamera.transform.eulerAngles = new Vector3(PlayerCamera.transform.eulerAngles.x + verticalValue, PlayerCamera.transform.eulerAngles.y + horizontalValue, 0.0f);
    }
}
