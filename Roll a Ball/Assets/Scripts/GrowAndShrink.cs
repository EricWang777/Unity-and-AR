using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowAndShrink : MonoBehaviour
{
    public float maxSize;
    public float minSize;
    public float speed;

    private float scaleX;
    private float scaleY;
    private float scaleZ;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
        
    }

    // Update is called once per frame
    void Update()
    {

        float ratio = (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f * (maxSize - minSize) + minSize;

        ratio *= slider.value;
        

        transform.localScale = new Vector3(ratio * scaleX, ratio * scaleY, scaleZ);



    }
}
