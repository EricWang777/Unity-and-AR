using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // private variable
    Rigidbody rb; // rigidbody of player
    int count; // the count of collections been made
    float time; // time pass during the entire game
    Plane plane; // the ground (math)
    Vector3 direction; // the velocity direction
    Vector3 spawnPoint;
    bool onGround;
    private float readyTime = 1.0f;

    
    

    
    // public variable
    public float speed;
    public Text countText;
    public Text winText;
    public float jumpVelocity;

    public void Respawn()
    {
        rb.velocity = Vector3.zero;
        readyTime = 1.0f;
        transform.position = spawnPoint;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        time = GameManager.time;
        setCountText();
        winText.text = "";
        plane = new Plane(Vector3.up, Vector3.zero);
        direction = Vector3.zero;
        //jumpVelocity = Mathf.Abs(Physics.gravity.y) * timeToJumpApex;
        spawnPoint = transform.position;
        onGround = false;
        
        
    }

    void FixedUpdate()
    {

    }

    private void Update()
    {
        
        if(transform.position.y < -1.0f) // fall out of the platform
        {
            Respawn();
        }
        time += Time.deltaTime;
        readyTime -= Time.deltaTime;
        setCountText();

        if (readyTime > 0f) return;
        move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick up"))
        {
            other.gameObject.SetActive(false);
            count++;
        }

    }

    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Selectable"))
        {
            onGround = true;
        }
        else if (collision.gameObject.CompareTag("Pass"))
        {
            // Next level
            GameManager.time = time;
            GameManager.count += count;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Selectable"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    /*
     * Helper Function
     */
    void setCountText()
    {
        countText.text = "Count: " + count.ToString() + "\n" +
                         "Time: " + time.ToString("0.00");

    }


    private void move()
    {
        if (!onGround) return;

        if(Input.touchCount > 0)
        {
            
            Touch touch = Input.GetTouch(0);
            if (touch.tapCount < 2) // simple move
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                //float enter = 0.0f;
                //if (plane.Raycast(ray, out enter))
                //{
                //    Vector3 hitPoint = ray.GetPoint(enter);
                //    direction = hitPoint - transform.position;
                //    Debug.DrawRay(transform.position, hitPoint - transform.position, Color.yellow);
                //    rb.velocity = direction * speed;
                //}

                RaycastHit[] hits;
                hits = Physics.RaycastAll(ray);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.transform.gameObject.CompareTag("Ground") || hit.transform.gameObject.CompareTag("Selectable"))
                    {
                        direction = hit.point - transform.position;
                        Debug.DrawRay(transform.position, hit.point - transform.position, Color.yellow);
                        rb.velocity = direction * speed;
                        break;
                    }
                }
          

            }
            else if(touch.tapCount == 2 && touch.phase == TouchPhase.Began)
            {
                rb.velocity += Vector3.up * jumpVelocity;
            }
            
        }
        
    }

    
    

}
