using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    private MyVector2D prevPosition;
    private MyVector2D acceleration;
    private MyVector2D velocity;
    MyVector2D forceCalculated = new MyVector2D(0, 0);


    [SerializeField] private CustomGravity myCustomGravityTarget;
    [SerializeField] public float localMass = 1f;


void Start()
    {
        prevPosition = transform.position;
    }

    private void Update()
    {

        SupportMovementLogic();
        MoveMySphere();
        DrawMyVectors();
    }
    void SupportMovementLogic()
    {
        acceleration = new MyVector2D(0, 0);
        MyVector2D targetDirectionVector = myCustomGravityTarget.transform.position - transform.position;
        forceCalculated = targetDirectionVector.normalized * (myCustomGravityTarget.localMass / targetDirectionVector.magnitude * targetDirectionVector.magnitude);
        acceleration = forceCalculated / localMass;
    }
    public void MoveMySphere()
    {
        
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        prevPosition = prevPosition + velocity * Time.fixedDeltaTime;
        Debug.Log("Velocity magnitude is " + velocity.magnitude);
        if (velocity.magnitude > 1.6f) 
        {
            velocity.Normalized(); 
            velocity = velocity * 1.3f;
            Debug.Log("Increasing vel");
        }

        else { Debug.Log("Dont decrease vel"); }
        transform.position = prevPosition;
    }
    void DrawMyVectors()
    {
        prevPosition.Draw(Color.blue);
        forceCalculated.Draw(prevPosition, Color.yellow);
        velocity.Draw(prevPosition, Color.red);
        acceleration.Draw(prevPosition, Color.green);
    }
}
