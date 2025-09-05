using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController reference;

    //Movement Variables
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    GameObject model;
    [SerializeField]
    Collider myCollider;
    [SerializeField]
    float moveAcceleration = 500;
    [SerializeField]
    float maxSpeed = 500;
    [SerializeField]
    float jumpForce = 50;
    [SerializeField]
    float sensitivity = 250;


    //Input systems
    InputAction moveAction;
    InputAction jumpAction;
    InputAction secondaryAction;

    void Awake()
    {
        reference = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        secondaryAction = InputSystem.actions.FindAction("Secondary");

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update() {

        //Jumping
        if (jumpAction.triggered && IsGrounded()) {
            rb.AddForce(jumpForce*Vector3.up,ForceMode.Impulse);
        }

        if (secondaryAction.IsPressed()) {
            //Turn player
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime, Space.Self);
        }

    }


    void FixedUpdate()
    {
        //Movement
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 moveValue = new Vector3(moveInput.x, 0, moveInput.y);

        if (rb.linearVelocity.magnitude < maxSpeed) {
            rb.AddForce(transform.rotation*(moveAcceleration*1000*Time.deltaTime*moveValue),ForceMode.Force);
        }
    }


    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y + 1.1f);
    }


}