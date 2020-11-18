using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    CharacterInput characterinput;

    public Camera axeCam;

    public bool inventoryIsClosed;
    public bool looking;

    public float mouseSensitivity = 500f;

    public Transform playerbody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        characterinput = GetComponent<CharacterInput>();

        Cursor.lockState = CursorLockMode.Locked;

        inventoryIsClosed = false;

        looking = true; 
    }

    // Update is called once per frame
    void Update()
    {
        if (looking == true)
        {
            LookAround();
        }
        else
        {
            looking = false;
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventoryIsClosed == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                inventoryIsClosed = false;
                looking = true;
                axeCam.cullingMask = (1 << LayerMask.NameToLayer("Axe"));
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                inventoryIsClosed = true;
                looking = false;
                axeCam.cullingMask = (1 << LayerMask.NameToLayer("Nothing"));
            }

        }
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 70f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerbody.Rotate(Vector3.up * mouseX);

    }


}

