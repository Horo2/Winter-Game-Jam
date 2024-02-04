using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float SensX;
    public float SensY;

    public Transform orientation;

    float XRotation;
    float YRotation;

    bool focused;

    [SerializeField]
    private Vector3 focusedPosition; // Position when camera is focused
    [SerializeField]
    private Vector3 unfocusedPosition; // Position when camera is unfocused
    [SerializeField]
    private Vector3 focusedRotation; // Position when camera is focused
    [SerializeField]
    private float transitionSpeed = 5f; // Speed of transition between positions

    private bool canSwitchFocus = true;
    private float holdTime = 0f; // Time the C key has been held down
    private float holdThreshold = 0.5f; // Threshold in seconds to trigger focus switch


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;   
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && canSwitchFocus)
        {
            holdTime += Time.deltaTime; // Increment the timer

            if (holdTime > holdThreshold) // Check if the hold time exceeds the threshold
            {
                SwitchCamFocus(); // Switch focus
                canSwitchFocus = false;
                holdTime = 0f; // Reset the timer to prevent repeated switches
            }
        }
        else if (Input.GetKeyUp(KeyCode.F)) // Reset the timer if the C key is released
        {
            canSwitchFocus = true;
            holdTime = 0f;
        }

        if (!focused)
        {
            CamfollowMouse();
        }
        Vector3 targetPosition = focused ? focusedPosition : unfocusedPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
        //orientation.rotation = Quaternion.Lerp(orientation.rotation, focusedRotation, Time.deltaTime * transitionSpeed);

    }

    private void CamfollowMouse()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;
        
        YRotation += mouseX;

        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, 90f, -90f);
        YRotation = Mathf.Clamp(YRotation, -25f, 25f);

        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        orientation.rotation = Quaternion.Euler(0, YRotation, 0);

    }

    private void SwitchCamFocus()
    {
        if (!focused)
        {
            focused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            orientation.rotation = Quaternion.Euler(focusedRotation);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            focused = false;
        }
       
    }



}
