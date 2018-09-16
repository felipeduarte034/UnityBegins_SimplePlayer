using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class MySimplePlayer : MonoBehaviour
{
    [SerializeField] float moveForce = 5f;
    [SerializeField] float jumpForce = 250f;
    [SerializeField] float speedCam = 120f;
    [SerializeField] KeyCode keyJump = KeyCode.Space;
    [SerializeField] Camera cam;

    float v;
    float h;
    float deltaMouseX;
    float deltaMouseY;
    bool inGround;
    Rigidbody rb;
    Quaternion quatY;
	Quaternion quatCamX;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //mousePos = Camera.main.Scree(Input.mousePosition);
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        deltaMouseX = Input.GetAxis("Mouse X");
        deltaMouseY = Input.GetAxis("Mouse Y");

        transform.Translate(h * moveForce * Time.deltaTime, 0f, v * moveForce * Time.deltaTime);

        quatY = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + (deltaMouseX * speedCam * Time.deltaTime), 0f);
        transform.rotation = quatY;

		quatCamX = Quaternion.Euler(cam.transform.localEulerAngles.x + (-deltaMouseY * speedCam * Time.deltaTime),0f,0f);
		cam.transform.localRotation = quatCamX;
    }

    void FixedUpdate()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(keyJump) && inGround)
            rb.AddForce(transform.up * jumpForce);
    }

    void OnTriggerEnter(Collider other)
    {
        inGround = true;
    }
    void OnTriggerExit(Collider other)
    {
        inGround = false;
    }
}
