using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        Quaternion myQuaternion = Quaternion.Euler(0, 0, 7.5f * Time.deltaTime);
        transform.rotation = transform.rotation * myQuaternion;
    }
}
