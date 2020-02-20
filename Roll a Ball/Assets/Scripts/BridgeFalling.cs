using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeFalling : MonoBehaviour
{
    public float fallingTime;


    float currentAlpha;
    bool startFalling;
    Vector3 origin;
    float time;

    // Start is called before the first frame update
    void Start()
    {

        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        time = fallingTime;
        startFalling = false;

        origin = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (startFalling)
        {
            fallingTime -= Time.deltaTime;
            if (fallingTime < 0.0f)
            {
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().isKinematic = false;
            }

            if (fallingTime < -5.0f)
            {
                Respawn();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            startFalling = true;
        }
    }

    private void Respawn()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        fallingTime = time;
        transform.position = origin;
        startFalling = false;
    }
}
