using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public int jumpHeight;

    private bool isGrounded;
    private bool jump;
    private float dashLimit;
    private float dashTime;
    //private float time;
    private float dashForce;
    

    Vector3 movement;
    //Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        jumpHeight = 300;
        isGrounded = true;
        dashLimit = 3;
        dashTime = dashLimit;
        //time = 0;
        dashForce = 400;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);
        Turning();
        //Animating(h, v);

        if (jump && isGrounded)
        {
            jump = false;
            playerRigidbody.AddForce(Vector3.up * jumpHeight);
        }
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning ()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    /*void Animating (float h, float v)
    {
        float forwardAmount = Mathf.Abs(h) + Mathf.Abs(v);
        anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
    }*/

    void Update()
    {
        dashTime = dashTime + Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 3f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 2f;
        }

        if (Input.GetMouseButtonDown(1) && dashTime >= dashLimit)
        {
            playerRigidbody.AddForce(transform.forward*dashForce);
            dashTime = 0;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        else if (other.gameObject.tag == "Respawn")
        {
            transform.position = new Vector3(0, 0, 0);
            dashTime = dashLimit;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }

        
    }
}
