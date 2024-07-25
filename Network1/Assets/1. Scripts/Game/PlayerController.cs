using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    MainCanvasManager mainCanvasManager;


    [SerializeField] GameObject cameraHolder;
    [SerializeField] GameObject other;

    [Header("Stat")]
    [SerializeField] float mouseSensitivity;

    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;

    [SerializeField] float jumpForce;

    [SerializeField] float smoothTime;



    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;
    PhotonView photonView;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (photonView.IsMine)
        {
            mainCanvasManager = MainCanvasManager.Instance;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            int othercount = other.transform.childCount;
            for (int i = 0; i < othercount; i++)
            {
                Destroy(other.transform.GetChild(i).gameObject);
            }

        }
        else
        {
            Camera _cam = GetComponentInChildren<Camera>();

            Destroy(_cam.gameObject);
            Destroy(rb);
        }
    }


    private void Update()
    {
        if (photonView.IsMine)
        {
            mouseLook();

            move();

            jump();

            mainCanvasManager.SetTestTitle($"{((int)rb.velocity.x)}, {((int)rb.velocity.y)}, {((int)rb.velocity.z)}");

        }


    }


    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    private void jump()
    {
        bool isPressedDownJumpKey = Input.GetKeyDown(KeyCode.Space);
        if (isPressedDownJumpKey && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    private void move()
    {
        bool isPressedRunKey = Input.GetKey(KeyCode.LeftShift);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(horizontal, 0, vertical).normalized;

        float _speed = isPressedRunKey ? sprintSpeed : walkSpeed;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * _speed, ref smoothMoveVelocity, smoothTime);
    }

    private void mouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(Vector3.up * mouseX * mouseSensitivity);


        verticalLookRotation += mouseY * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }


}
