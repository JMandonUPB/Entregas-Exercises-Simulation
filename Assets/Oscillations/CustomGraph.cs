using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGraph : MonoBehaviour
{

    [SerializeField] int m_totalPoints = 10;
    [SerializeField] float m_distanceFactor = 10;
    [SerializeField] float m_amplitude = 1;


    [SerializeField] GameObject prefab;
    GameObject[] myPointsArray;


    // Start is called before the first frame update
    void Start()
    {
        InitialSetup();
    }

    void Update()
    {
        GraphMyGraph();
    }

    void GraphMyGraph()
    {
        for (int count = 0; count < myPointsArray.Length; ++count)
        {
            float x = count * m_distanceFactor;
            float y = m_amplitude * Mathf.Sin(x + Time.time);
            myPointsArray[count].transform.localPosition = new Vector3(x, y, 0);
        }
    }

    void InitialSetup()
    {
        myPointsArray = new GameObject[m_totalPoints];
        for (int i = 0; i < m_totalPoints; ++i)
        {
            myPointsArray[i] = Instantiate(prefab, transform);
        }
    }
}
