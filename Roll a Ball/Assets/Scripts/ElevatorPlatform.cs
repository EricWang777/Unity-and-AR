using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorPlatform : MonoBehaviour
{
    public GameObject axis;
    public float angularSpeed;
    public Slider slider;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - axis.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        offset = Quaternion.AngleAxis(angularSpeed * slider.value * Time.deltaTime, Vector3.forward) * offset;
        transform.position = axis.transform.position + offset;
    }
}
