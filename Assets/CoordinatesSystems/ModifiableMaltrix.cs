using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiableMaltrix : MonoBehaviour
{
    [Header("Matrix transform options")]
    [SerializeField]
    Vector3 position;
    [SerializeField]
    Vector3 rotation;
    [SerializeField]
    Vector3 scale;

    [Header("Matrix drawing options")]
    [SerializeField]
    float myStepSize = 0.15f;
    [SerializeField]
    int maxSteps = 50;
    [SerializeField]
    bool drawXY = true;
    [SerializeField]
    bool drawZX = true;
    [SerializeField]
    bool drawYZ = true;
    [SerializeField]
    Transform targetObject;
    // Internal variables
    private Matrix4x4 matrix;
    private Vector3 otherObjectInitialPosition;


    private void Start()
    {
        otherObjectInitialPosition = targetObject.position;
    }

    private void Update()
    {
        matrix = Matrix4x4.TRS(position, Quaternion.Euler(rotation), scale);

        UpateOtherObject();
        DrawBase();
        DrawPlanes();
    }

    private void UpateOtherObject()
    {
        if (targetObject == null) return;

        targetObject.position = otherObjectInitialPosition;
        targetObject.position = matrix.MultiplyPoint3x4(targetObject.position);
    }

    private void DrawBase()
    {
        Vector3 pos = matrix.GetColumn(3);

        Debug.DrawRay(pos, matrix.GetColumn(0), Color.yellow);
        Debug.DrawRay(pos, matrix.GetColumn(1), Color.black);
        Debug.DrawRay(pos, matrix.GetColumn(2), Color.cyan);
    }

    private void DrawPlanes()
    {
        Vector3 pos = matrix.GetColumn(3);

        Vector3 xAxis = matrix.GetColumn(0);
        Vector3 yAxis = matrix.GetColumn(1);
        Vector3 zAxis = matrix.GetColumn(2);

        if (drawXY)
        {
            DrawMyGrid(pos, xAxis, yAxis, scale.x, scale.y);
        }

        if (drawZX) 
        {
            DrawMyGrid(pos, zAxis, xAxis, scale.z, scale.x);
        }

        if (drawYZ)
        {
            DrawMyGrid(pos, yAxis, zAxis, scale.y, scale.z);
        }
    }

    private void DrawMyGrid(Vector3 pos, Vector3 xAxis, Vector3 yAxis, float scaleX, float scaleY)
    {
        for (int i = 1; i <= maxSteps; ++i)
        {
            // Logic for drawing the x to y grid
            Debug.DrawRay(pos + xAxis * myStepSize * i, yAxis.normalized * myStepSize * maxSteps * Mathf.Abs(scaleY));
            Debug.DrawRay(pos + yAxis * myStepSize * i, xAxis.normalized * myStepSize * maxSteps * Mathf.Abs(scaleX));
        }
    }
}
