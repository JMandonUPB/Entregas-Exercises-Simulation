using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSphereForces : MonoBehaviour
{
    private float gravityValue = -10f;

    private Camera camera;

    private MyVector2D position;
    private MyVector2D acceleration;
    private MyVector2D velocity;

    [SerializeField] private MyVector2D customWind;
    [SerializeField] private float mass = 1f;

    [Range(0f, 1f), SerializeField] private float dampingFactor = 0.95f;
    [Range(0f, 1f), SerializeField] private float frictioncoefficient = 0.95f;

    private MyVector2D weight;
    void Start()
    {
        position = transform.position;
        camera = GameObject.Find("Camera").GetComponent<Camera>(); //not ideal way to capture da cam
    }

    private void FixedUpdate()
    {
        MovementSupportingLogic();
        MoveSphere();
        DrawVectors();
    }
    public void MoveSphere()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        if (Mathf.Abs(position.x) > camera.orthographicSize)
        {
            position.x = Mathf.Sign(position.x) * camera.orthographicSize;
            velocity *= -dampingFactor;
        }

        if (Mathf.Abs(position.y) > camera.orthographicSize)
        {
            position.y = Mathf.Sign(position.y) * camera.orthographicSize;
            velocity *= -dampingFactor;
        }

        transform.position = position;
    }

    private void ApplyCustomForce(MyVector2D force)
    {
        acceleration = force / mass;
    }

    void DrawVectors()
    {
        acceleration.Draw(position, Color.blue);
        weight.Draw(position, Color.cyan);


        position.Draw(Color.yellow);
        velocity.Draw(position, Color.cyan);
    }

    void MovementSupportingLogic()
    {
        weight = new MyVector2D(0, (gravityValue * mass));
        acceleration = new MyVector2D(0, 0);

        MyVector2D friction = new MyVector2D(0, 0);

        friction = frictioncoefficient * -(gravityValue * mass) * velocity.normalized * -1;


        MyVector2D myAddedForces = weight + friction;
        Debug.Log("my added counter forces value: "+ myAddedForces);
        ApplyCustomForce(myAddedForces);

    }
}
