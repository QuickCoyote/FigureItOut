using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [SerializeField] GameObject PlayerCamera = null;
    [SerializeField] float horizontalSensitivity = 2.0f;
    [SerializeField] float verticalSensitivity = 2.0f;
    [SerializeField] float verticalClampAngle = 85.0f;

    [SerializeField] RectTransform crosshair1 = null;
    [SerializeField] RectTransform crosshair2 = null;

    [SerializeField] TextMeshProUGUI distanceTextUGUI = null;

    private float verticalValue = 0.0f;
    float horizontalValue = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        horizontalValue += Input.GetAxis("Mouse X") * horizontalSensitivity;
        verticalValue += -Input.GetAxis("Mouse Y") * verticalSensitivity;

        verticalValue = Mathf.Clamp(verticalValue, -verticalClampAngle, verticalClampAngle);

        PlayerCamera.transform.rotation = Quaternion.Euler(verticalValue, horizontalValue, 0.0f);

        if(Input.GetKey(KeyCode.UpArrow))
        {
            AdjustHeight(1);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            AdjustHeight(-1);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            AdjustWidth(1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            AdjustWidth(-1);
        }

        RaycastHit result;

        if(Physics.Raycast(transform.position, PlayerCamera.transform.forward, out result))
        {
            float distance = (transform.position - result.point).magnitude;

            distance = Mathf.Ceil(distance);

            distanceTextUGUI.text = distance + "m";
        }

        UpdateSensitivity();
    }

    public void AdjustWidth(float value)
    {
        crosshair1.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, crosshair1.rect.width + value);
        crosshair2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, crosshair2.rect.width + value);
    }

    public void AdjustHeight(float value)
    {
        crosshair1.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, crosshair1.rect.height + value);
        crosshair2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, crosshair2.rect.height + value);
    }

    public void UpdateSensitivity()
    {
        horizontalSensitivity = PlayerPrefs.GetFloat("HorizontalSensitivity");
        verticalSensitivity = PlayerPrefs.GetFloat("VerticalSensitivity");
    }
}
