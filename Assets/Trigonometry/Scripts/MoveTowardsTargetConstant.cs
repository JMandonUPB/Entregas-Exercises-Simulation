using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsTargetConstant : MonoBehaviour
{
    Vector3 followerAcceleration;
    Vector3 followerVelocity;
    private float mySpeed = 3.5f;

    private void Update()
    {
        //main logic
        MyMovementVector();
        ApplyForcesAndVelocties();

        //debugging logic:
        Debug.Log("Current acceleration : " + followerAcceleration);
    }

    private void MyMovementVector()
    {
        followerVelocity = WorldMousePosition() - transform.position;
        followerVelocity.z = 0;
        
        Debug.Log("Velocity : " + followerVelocity);

        //experimenting n debugging
        //followerVelocity.Normalized()
        //this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        followerVelocity.Normalize();
        followerVelocity *= mySpeed;
        followerAcceleration *= 0;
    }
    void ApplyForcesAndVelocties()
    {
        followerVelocity += followerAcceleration * Time.deltaTime;
        this.transform.position += followerVelocity * Time.deltaTime;
        RotateZAngle(Mathf.Atan2(followerVelocity.y, followerVelocity.x) - Mathf.PI / 2f);
    }

    Vector3 WorldMousePosition()
    {
        Camera mainCamera = Camera.main;

        Vector3 myScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane);
        Vector4 myWorldPosition = Camera.main.ScreenToWorldPoint(myScreenPosition);

        //Debug.Log("myScreenPosition: "+ myScreenPosition)
        //Debug.Log("myScreenPosition: "+ myWorldPosition)
        return myWorldPosition;
    }

    void RotateZAngle(float angleRad)=> this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angleRad * Mathf.Rad2Deg);

}
