using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVelocityFlipper : MonoBehaviour
{
    private int counter = 0;

    private MyVector2D velocity;
    private MyVector2D acceleration;
    private MyVector2D position;
    private MyVector2D displacement;
    
    [Range(0f, 1f)] [SerializeField] private float myDampFactor = .957f;
    private Camera camera;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //save initial position value

        position = new MyVector2D(transform.position.x, transform.position.y);
    }

    private void FixedUpdate() //movement logic capped at 60 times a seccond assures consistency
    {
        VelocityFlipperMovementLogic();
    }
    void Update() //its best to calculate / read input in update, as it checks it far more precisely and constantly
    {
        DrawVectors();
        UserInputAndCounter();
    }

    public void VelocityFlipperMovementLogic()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;

        position = position + velocity * Time.fixedDeltaTime;

        if (Mathf.Abs(position.x) > camera.orthographicSize)
        {
            velocity.x = velocity.x * -1;
            position.x = Mathf.Sign(position.x) * camera.orthographicSize;
            velocity *= myDampFactor;
        }
        if (Mathf.Abs(position.y) > camera.orthographicSize)
        {
            velocity.y = velocity.y * -1;
            position.y = Mathf.Sign(position.y) * camera.orthographicSize;
            velocity *= myDampFactor;
        }
        transform.position = new Vector3(position.x, position.y);
    }

    void DrawVectors()
    {
        position.Draw(Color.cyan);
        acceleration.Draw(position, Color.white);
        displacement.Draw(position, Color.black);
    }

    void UserInputAndCounter() //horrible way to do it, we could use an array or something, but im a lil lazy!
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (counter ==0)
            {
                acceleration = new MyVector2D(0, -9.8f);
            }
            else if (counter == 1)
            {
                acceleration = new MyVector2D(9.8f, 0);

            }
            else if (counter==2)
            {
                acceleration = new MyVector2D(0, 9.8f);

            }
            else if (counter ==3)
            {
                acceleration = new MyVector2D(-9.8f, 0);
                
            }

            if (counter>=3)
            {
                counter = 0;
            }
            else counter++;

            velocity *= 0;
        }
    }
}
