using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatingFloor : MonoBehaviour
{
    public GameObject axis;
    public float angularSpeed;
    public float friction;

    public Slider slider;
    private Vector3 scale = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.RotateAround(axis.transform.position, Vector3.up, angularSpeed * slider.value * Time.deltaTime);
        //offset = Quaternion.AngleAxis(angularSpeed * Time.deltaTime, Vector3.up) * offset;
        //transform.position = axis.transform.position + offset;
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject player = collision.gameObject;
        Vector3 force = (transform.position - player.transform.position) * friction * angularSpeed * slider.value;
        force = Quaternion.Euler(0, -90, 0) * force;
        player.GetComponent<Rigidbody>().AddForce(force);
    }

  
}
