using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarFunExperiment : MonoBehaviour
{
    [Header("Dark Hole parameters")]
    [SerializeField] float myRadius;
    [SerializeField] float myAngleDeg;
    [SerializeField] float myAngularSpeed;
    [SerializeField] float myRadialSpeed;

    private void Update()
    {
        UpdateDarkHolePosition();
        UpdateDarkHoleRadiusAngle();
    }

    void UpdateDarkHoleRadiusAngle()
    {
        myAngleDeg += myAngularSpeed * Time.deltaTime;
        myRadius += myRadialSpeed * Time.deltaTime;
    }
    private Vector3 PolarToCartesianConverter(float radius, float angle)
    {
        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        //experimenting stuff
        //Debug.Log("float x: " + x);
        //Debug.Log("float x: " + y);

        return new Vector3(x, y, 0);
    }
    void UpdateDarkHolePosition() => this.transform.position = PolarToCartesianConverter(myRadius, myAngleDeg);

}
