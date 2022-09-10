using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSimulation : MonoBehaviour
{
    [SerializeField] private bool isFrictionOn;
    [SerializeField] private bool isGravityOn; //unused

    private float MyCustomDampingFactor = 0.05f;
    [Range(0f, 1f), SerializeField] private float frictionCoeff = 0.75f;

    [SerializeField] private MyVector2D blockAcceleration;
    [SerializeField] private MyVector2D blockVelocity;
    [SerializeField] private float blockMass = 1f;

    

    private float gravityValue = -10f;
    private MyVector2D position;

    Camera camera;

    void Start()
    {
        position = transform.position;
        camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    private void FixedUpdate() //assures that it is constantly executed at a predefined rate
    {
        FloatingAndFrictionSupportLogic();
        MoveBlockLogic();
        DrawMyVectors();
    }
    public void MoveBlockLogic()
    {
        blockVelocity = blockVelocity + blockAcceleration * Time.fixedDeltaTime;
        position = position + blockVelocity * Time.fixedDeltaTime;

        if (Mathf.Abs(position.x) > camera.orthographicSize)
        {
            position.x = Mathf.Sign(position.x) * camera.orthographicSize;

            blockVelocity *= -MyCustomDampingFactor;
        }
        if (Mathf.Abs(position.y) > camera.orthographicSize)
        {
            position.y = Mathf.Sign(position.y) * camera.orthographicSize;

            blockVelocity *= -MyCustomDampingFactor;
        }

        //experimenting, ignore
        //if (Mathf.Abs(position.z) > camera.orthographicSize)
        //{
        //    position.z = Mathf.Sign(position.z) * camera.orthographicSize;

        //    blockVelocity *= -MyCustomDampingFactor * 0.6;
        //}


        transform.position = position;
    }

    private void ApplyMyForces(MyVector2D force)
    {
        blockAcceleration = force / blockMass;
    }

    //void MoveMyBlockTesting() unused for now
    //{
    //    if (Mathf.Abs(position.y) > camera.orthographicSize)
    //    {
    //        position.y = Mathf.Sign(position.y) * camera.orthographicSize;
    //        blockVelocity *= -DampingFactor;
    //        Debug.Log("Moving at : " + blockVelocity);
    //    }
    //}

    void FloatingAndFrictionSupportLogic()
    {
        blockAcceleration = new MyVector2D(0, 0);

        float myWeightScalar = gravityValue * blockMass;

        MyVector2D weight = new MyVector2D(0, myWeightScalar);

        MyVector2D friction = new MyVector2D(0, 0); // calculated later

        ApplyMyForces(weight);

        if (isFrictionOn && transform.localPosition.y <= 4.17)//water distance
        {
            float frontArea = transform.localScale.x;
            float velocityMagnitude = blockVelocity.magnitude;
            float myScalar = -0.5f * 1.5f * velocityMagnitude * velocityMagnitude * frontArea * 1;
            MyVector2D frictionWater = myScalar * blockVelocity.normalized;

            ApplyMyForces(frictionWater);
        }
        else
        {
            friction = frictionCoeff * -myWeightScalar * blockVelocity.normalized * -1;
            ApplyMyForces(weight + friction);


            weight.Draw(position, Color.red);
        }

    }

    void DrawMyVectors()
    {
        position.Draw(Color.blue);

        blockVelocity.Draw(position, Color.blue);
        blockAcceleration.Draw(position, Color.black);
    }
}
