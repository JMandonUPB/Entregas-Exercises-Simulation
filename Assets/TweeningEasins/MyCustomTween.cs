using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCustomTween : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField, Range(0, 1)] private float myNormalizedTime;

    private float duration = 2.5f;

    private float myCurrentTime = 0;
    private Vector3 myInitialPosition;
    private Vector3 myFinalPosition;


    [SerializeField] private Color initialColor;
    [SerializeField] private Color targetColor;

    [SerializeField] SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {
        StartDaEpicTweeeen();
    }

    // Update is called once per frame
    void Update()
    {
        MainLogic();
    }

    private void StartDaEpicTweeeen()
    {
        myCurrentTime = 0f;
        myInitialPosition = transform.position;
        myFinalPosition = targetTransform.position;
    }

    private float EaseIn(float x)
    {
        return x * x;
    }

    void MainLogic()
    {
        myNormalizedTime = myCurrentTime / duration;
        transform.position = Vector3.Lerp(myInitialPosition, myFinalPosition, EaseIn(myNormalizedTime));
        spriteRenderer.color = Color.Lerp(initialColor, targetColor, EaseIn(myNormalizedTime));
        myCurrentTime += Time.deltaTime;
    }
}
