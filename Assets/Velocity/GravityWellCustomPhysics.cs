using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWellCustomPhysics : MonoBehaviour
{
    [SerializeField] private Transform gravityWellSourceTransform;

    private MyVector2D position;
    [SerializeField] private MyVector2D velocity;
    private MyVector2D acceleration;

    private Camera camera;


    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        GravityWellMovementLogic();
    }
    void Update()
    {
        MovementSupportLogic();
        DrawVectors();
    }

    public void GravityWellMovementLogic()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime*1.76f;
        position = position + velocity * Time.fixedDeltaTime * 1.76f;
        //transform.position = new Vector3(position.x, position.y, position.z);
        transform.position = new Vector3(position.x, position.y);
    }

    void MovementSupportLogic()
    {
        position = new MyVector2D(transform.position.x, transform.position.y);
        MyVector2D blackHolePosition = new MyVector2D(gravityWellSourceTransform.position.x, gravityWellSourceTransform.position.y);
        Debug.Log("acceleration " + acceleration.magnitude);
        acceleration = blackHolePosition - position;
    }

    void DrawVectors()
    {
        position.Draw(Color.blue);
        acceleration.Draw(Color.cyan);
        velocity.Draw(Color.yellow);

    }
}
