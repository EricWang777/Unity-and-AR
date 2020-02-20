using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float minMovementDistance = 200f;
    public float rotateTime = 1.5f;
    public GameObject fakeTopCamera;

    private Transform originalTransform;
    private Vector3 originalOffset;

    private Vector3 offset;

    private Vector3 fingerUpPosition;
    private Vector3 fingerDownPosition;
    private bool rotate;
    private float rotateTimer;
    private int swipeDirection;
    private bool topView;



    // Start is called before the first frame update
    void Start()
    {
        topView = false;
        originalTransform = transform;

        swipeDirection = 0;
        rotate = false;
        offset = transform.position - player.transform.position;
        originalOffset = offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!topView)
        {
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform.position);
        }
        
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
        
        

        if (rotateTimer >= 0)
        {
            
            transform.RotateAround(player.transform.position, new Vector3(0.0f, 1.0f, 0.0f), swipeDirection * 50 * Time.deltaTime / rotateTime);
            transform.LookAt(player.transform);
            offset = transform.position - player.transform.position;
            rotateTimer -= Time.deltaTime;
        }


    }

    private void DetectSwipe()
    {
        if (Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x) > minMovementDistance * 2)
        {
            if (fingerDownPosition.x > fingerUpPosition.x)
            {
                swipeDirection = 1;
            }
            else
            {
                swipeDirection = -1;
            }

            // Rotating camera
            rotateTimer = rotateTime;
        }
        else if (fingerDownPosition.y - fingerUpPosition.y > minMovementDistance)
        {
            if (!topView)
            {
                transform.SetPositionAndRotation(fakeTopCamera.transform.position, fakeTopCamera.transform.rotation);
                topView = true;
                
            }
            else
            {
                transform.SetPositionAndRotation(originalTransform.position, originalTransform.rotation);
                offset = originalOffset;
                topView = false;
            }
            
        }
    }

    


    
}


