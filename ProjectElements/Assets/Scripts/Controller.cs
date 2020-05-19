using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody rb;
    public static bool isGrounded;
    bool touchingGround = false;
    public float smoothnessX;
    public float smoothnessValue;
    public float speed;
    public float desiredVelocity;
    public float jumpSmooth;
    public float fallGravity;
    private int jumpAcum;
    private double jumpRef;
    private Vector3[] groundRays;
    public static int lifes;
    public static double inmuRef;
    [SerializeField]
    private float maxSpeed;
    private bool canJump;
    RigidbodyConstraints defaultConstraints;
    Vector3 animationLocalPos;
    Animation actualAnim;
    Vector3 deltaPos;
    int truncateAcum = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb.constraints = defaultConstraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
        isGrounded = false;
        jumpRef = Time.realtimeSinceStartup;
        inmuRef = 0;

        groundRays = new Vector3[3];
        groundRays[0] = Vector3.down;
        groundRays[1] = Vector3.Normalize(Vector3.down + Vector3.left);
        groundRays[2] = Vector3.Normalize(Vector3.down + Vector3.right);

        lifes = 4;
        canJump = true;
        deltaPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude > maxSpeed && truncateAcum < 2)
        {
            truncateAcum++;
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        else if (truncateAcum >= 2)
        {
            rb.velocity = Vector3.zero;
            truncateAcum = 0;
            transform.position = deltaPos;
        }
        else truncateAcum = 0;
        if (Vector2.Distance(transform.position, deltaPos) < maxSpeed) deltaPos = transform.position;
        else { transform.position = deltaPos; rb.velocity = Vector3.zero; }
    }

    private void FixedUpdate()
    {
        if (touchingGround)
        {
            for (int i = 0; i < 2; i++)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position - new Vector3(0.35f - i * (0.35f * 2), 0, 0), groundRays[0], out hit, 1);

                if (hit.transform != null && hit.transform.gameObject.layer == 11)
                {
                    isGrounded = true;
                    break;
                }
                else isGrounded = false;
            }
        }
        else isGrounded = false;

        if (Input.GetKey(KeyCode.E))
        {
            for (int i = 0; i < 2; i++)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, groundRays[i + 1], out hit, 1);

                if (hit.transform != null && hit.transform.gameObject.layer == 12)
                {
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    rb.useGravity = false;
                    transform.parent.parent = hit.transform;
                    animationLocalPos = transform.localPosition;
                    transform.parent.GetChild(1).GetChild(0).gameObject.SetActive(false);
                    transform.parent.GetChild(1).GetChild(1).gameObject.SetActive(true);
                    actualAnim = hit.transform.parent.GetComponent<Animation>();
                    actualAnim.Play();
                    break;
                }
            }
        }
        else rb.constraints = defaultConstraints;

        
        Vector3 nextVelocity = rb.velocity;

        //if (!isGrounded && jumpAcum == 0)
        //{
        //    nextVelocity.y -= speed * 0.3f;
        //    rb.AddForce(Vector3.down * fallGravity, ForceMode.Acceleration);
        //    rb.velocity = nextVelocity.normalized * desiredVelocity;
        //}
        //else 

        if (!isGrounded)
        {
            canJump = false;
            //nextVelocity.y -= speed * 30.0f;
            rb.AddForce(Vector3.down * fallGravity, ForceMode.Acceleration);
            if(rb.velocity.y < 0.0f) rb.AddForce(Vector3.down * fallGravity, ForceMode.Impulse);
            //rb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0).normalized * speed / 10.0f, ForceMode.Acceleration);
            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0).normalized * speed *0.75f * Time.deltaTime;
        }
        else
            transform.position += new Vector3(Input.GetAxis("Horizontal"), 0).normalized * speed * Time.deltaTime; //rb.AddForce(new Vector3(Input.GetAxis("Horizontal"), 0).normalized * speed, ForceMode.Acceleration);

        applyJump();
        touchingGround = false;
    }

    private void LateUpdate()
    {
        if (actualAnim == null) return;
        if (actualAnim.isPlaying) transform.localPosition = animationLocalPos;
        else
        {
            Vector3 auxPos = transform.position;
            Vector3 auxSMoothPos = transform.parent.GetChild(1).position;
            transform.parent.parent = null;
            transform.parent.eulerAngles = Vector3.zero;
            transform.position = auxPos;
            transform.parent.GetChild(1).position = auxSMoothPos;
            rb.constraints = defaultConstraints;
            rb.useGravity = true;
            transform.parent.GetChild(1).GetChild(0).gameObject.SetActive(true);
            transform.parent.GetChild(1).GetChild(1).gameObject.SetActive(false);
            actualAnim.transform.GetChild(0).gameObject.layer = 11;
            actualAnim.enabled = false;
            actualAnim = null;
        }
    }

    void applyJump()
    {
        //RaycastHit hit1, hit2;
        //Physics.Raycast(transform.position, Vector3.right, out hit1, 0.5f);
        //Physics.Raycast(transform.position, Vector3.left, out hit2, 0.5f);

        //if ((hit1.transform != null && hit1.transform.gameObject.layer == 10) || (hit2.transform != null && hit2.transform.gameObject.layer == 10))
        //{
        //    jumpAcum = 0;
        //    return;
        //}
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpSmooth, ForceMode.Impulse);
            rb.AddForce(rb.velocity * (jumpSmooth / 5.0f) + new Vector3(Input.GetAxis("Horizontal"), 0).normalized * speed * 2, ForceMode.Impulse);
            isGrounded = false;
            //if(jumpAcum == 0) jumpRef = Time.realtimeSinceStartup;
            if (!isGrounded) canJump = false;
        }
    }

    public void restartlife()
    {
        deltaPos = transform.position;
    }

    void OnCollisionStay(Collision theCollision)
    {
        if (theCollision.gameObject.layer == 11)
        {
            touchingGround = true;
        }
    }
}
