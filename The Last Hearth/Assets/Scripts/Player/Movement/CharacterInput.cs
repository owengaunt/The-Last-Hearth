using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public KeyCode jumpkey;
    public KeyCode crouchkey;
    public KeyCode sprintkey;
    public KeyCode walkkey;
    public KeyCode rightwalkkey;
    public KeyCode leftwalkkey;
    public KeyCode backwalkkey;


    public bool isMoving;

    public void Update()
    {
        if(Input.GetKey(jumpkey) || Input.GetKey(walkkey) || Input.GetKey(rightwalkkey) || Input.GetKey(leftwalkkey) || Input.GetKey(backwalkkey))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    } 

}