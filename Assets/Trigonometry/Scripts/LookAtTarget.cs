using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    Vector3 myMouseVectorPos = Vector3.zero;

    void Update()
    {
        UpdateMainMovLogic();
    }
    void UpdateMainMovLogic()
    {
        myMouseVectorPos = WorldMousePosition();
        LookAtMouseTarget(myMouseVectorPos);
    }
    void LookAtMouseTarget(Vector3 myMousePosition)
    {
        Vector3 currentPosition = new Vector2(transform.position.x, transform.position.y);
        //Debug.Log("testing up value" + up);
        //Vector3 up = myMousePosition + currentPosition + new Vector3(0,0,1);

        Vector3 forward = myMousePosition - currentPosition;
        float radians = Mathf.Atan2(forward.y, forward.x) - Mathf.PI / 2;

        RotateZAngle(radians);
    }
    void RotateZAngle(float angleRad)
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angleRad * Mathf.Rad2Deg);
    }
    Vector4 WorldMousePosition()
    {
        Camera mainCamera = Camera.main;

        Vector3 myScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane);
        Vector4 myWorldPosition = Camera.main.ScreenToWorldPoint(myScreenPosition);
        return myWorldPosition;
    }

  
}
