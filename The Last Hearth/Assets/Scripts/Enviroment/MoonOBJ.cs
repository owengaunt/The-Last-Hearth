using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOBJ : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 7 * Time.deltaTime);
        transform.RotateAround(Vector3.zero, Vector3.right, 5f * Time.deltaTime);
    }
}
