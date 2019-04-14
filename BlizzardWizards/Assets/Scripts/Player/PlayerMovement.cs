using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public int jumpHeight;

    public GameObject teleportPrefab;
    public Transform teleportParticleSystemPoint;
    public float teleportDistance = 3f;
    public float floorOverlapSphereRadius = 0.2f;

    private bool isGrounded;
    private bool jump;
    private float dashLimit;
    private float dashTime;
    //private float time;
    private float dashForce;
    private float flashLimit;
    private float flashTime;
    private bool isMoving;
    private bool isRunning;


    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    ParticleSystem teleportParticles;
    int floorMask; //para el apuntado
    int groundMask;//para el salto
    float camRayLength = 100f;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Floor");
        groundMask = LayerMask.GetMask("Ground");

        teleportParticles = Instantiate(teleportPrefab).GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 9f;
        jumpHeight = 300;
        isGrounded = true;
        dashLimit = 3;
        dashTime = dashLimit;
        flashLimit = 3;
        flashTime = flashLimit;
        dashForce = 200;
        isMoving = false;
        isRunning = false;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");   //Si lo cambiamos por GetAxis, parece que derrapa (se desliza)
        float v = Input.GetAxisRaw("Vertical");

        isMoving = Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0;
        anim.SetBool("moving", isMoving);

        Move(h, v);
        Turning();
        //Animating(h, v);

        isGrounded = Physics.OverlapSphere(transform.position, floorOverlapSphereRadius, groundMask).Length != 0 ? true : false;

        if (jump && isGrounded)
        {
            anim.SetTrigger("jump");
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
        flashTime = flashTime + Time.deltaTime;

        anim.SetBool("running", isRunning);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 15f;
            if (!isRunning)
            {
                isRunning = true;
            }
            
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 9f;
            if (isRunning)
            {
                isRunning = false;
            }
            
        }

        if (Input.GetMouseButtonDown(1) && dashTime >= dashLimit)
        {
            playerRigidbody.AddForce(transform.forward*dashForce);
            dashTime = 0;
        }

        if (Input.GetMouseButtonDown(2) && flashTime >= flashLimit && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            Vector3 displacement = Vector3.zero;

            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
            {
                displacement.z = 1f;
            }

            if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
            {
                displacement.z = -1f;
            }

            if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                displacement.x = 1f;
            }

            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                displacement.x = -1f;
            }

            if (displacement == Vector3.zero) return;

            displacement = displacement.normalized * teleportDistance;
            teleportParticles.transform.position = teleportParticleSystemPoint.position;
            teleportParticles.Play();

            transform.position = transform.position + displacement;

            flashTime = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Respawn"))
        {
            transform.position = Vector3.zero;
            dashTime = dashLimit;
            flashTime = flashLimit;
        }
    }
}
