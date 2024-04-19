using UnityEngine;
using UnityEngine.EventSystems;

public class BasePlayerMovement : MonoBehaviour
{
    // Movement variables
    protected float verInput;
    protected float hozInput;
    protected float currentSpeed;
    [Header("BaseMovement")]
    public float baseSpeed = 1f; // Initial base speed
    public float sprintMultiplier = 2.5f;
    protected Vector3 moveDirection;
    
    Vector3 gravity;

    // Start is called before the first frame update
    void Start()
    {
        gravity = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
       
        HandleInput();
        MoveCharacter();
        RotateCharacter();
        ApplyGravity();
    }

    protected void HandleInput()
    {
        // Get input from arrow keys or WASD
        verInput = Input.GetAxis("Vertical");
        hozInput = Input.GetAxis("Horizontal");
    }

    protected virtual void MoveCharacter() 
    {
        // sprint speed
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? baseSpeed * sprintMultiplier : baseSpeed;

        // movement direction
        Vector3 moveDirection = new Vector3(verInput , 0f, -hozInput).normalized;

        // move the character
        transform.Translate(moveDirection * currentSpeed * Time.deltaTime);
    }
    protected virtual void RotateCharacter()
    {
        Vector3 movDir = new Vector3(hozInput, 0f, verInput).normalized; // normalized the vector of X and Z inputs 
        if (movDir.magnitude > 0.001f)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movDir, Vector3.up); // gets the desired rotation from move direction combined with the Y up vector 
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 5 * Time.deltaTime); //takes the current rotation, and moves it to the desired rotation i 
        }
    }
    protected void ApplyGravity()
    {
        // Apply gravity force
        transform.Translate(gravity * Time.deltaTime);
    }

    protected void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("InvisWall"))
        {
            transform.Translate(-moveDirection * currentSpeed * Time.deltaTime);
        }
    }
    public float getCurrentSpeed()
    {
        return currentSpeed;
    }
}
