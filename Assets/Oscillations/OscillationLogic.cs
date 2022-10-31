using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillationLogic : MonoBehaviour
{
    [SerializeField]  float amplitud = 1.5f;

    [SerializeField] float timePeriod = 1;
    [SerializeField] bool XYMode;

    Vector3 myInitialPosition;

     void Start()
    {
        myInitialPosition = new Vector3(transform.position.x, transform.position.y);
    }
     void Update()
    {
        if (XYMode)
        {
            MyOscillationXYLogic();
        }
        else
        {
            MyOscillationXLogic();
        }
    }

    void MyOscillationXYLogic()
    {
        float movement = amplitud * Mathf.Sin(2f * Mathf.PI * (Time.time / timePeriod));
        transform.position = myInitialPosition + new Vector3(movement, movement, 0);
    }

    void MyOscillationXLogic()
    {
        float movement = Mathf.Sin(5f * Time.time) + Mathf.Cos(Time.time / 3f) + Mathf.Sin(Time.time / 13f);
        transform.position = myInitialPosition + new Vector3(movement, 0, 0);
    }

}
