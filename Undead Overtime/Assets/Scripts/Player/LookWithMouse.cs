#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWithMouse : MonoBehaviour
{

    [SerializeField] Joystick joystickRight;

    public Transform playerBody;

    float xRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        JoyStickCamera();
    }

    private void JoyStickCamera()
    {
        float horizontal = joystickRight.Horizontal * 50f;
        float vertical = joystickRight.Vertical * 50f;

        horizontal *= Time.deltaTime;
        vertical *= Time.deltaTime;

        xRotation -= vertical;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * horizontal);

    }
}
