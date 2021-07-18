using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class penaScript : MonoBehaviour
{

    Vector3 worldPosition;
    void Update()
    {
       worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        faceMouse();
    }

    void faceMouse()
    {
        /*Quaternion rotation = Quaternion.LookRotation;
        // (worldPosition.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);*/
    }
}
