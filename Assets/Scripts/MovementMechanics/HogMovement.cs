using System.Collections;
using UnityEngine;

public class HogMovement : BasePlayerMovement
{
    // Custom mechanic variables
    bool isDashing = false;
    float dashSpeedMultiplier = 25f;
    float dashDuration = .5f;
    public GameObject fireballPrefab;
    float originalBaseSpeed;
    public float jumpForce;
    public int jumpLimit;

    void Start()
    {
        originalBaseSpeed = base.baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for custom mechanics
        HogCustom();
        // Dash 
        

        // Call the base class method to handle basic movement
        base.HandleInput();
        base.MoveCharacter();
        base.RotateCharacter();
        
        base.ApplyGravity();
    }

    void HogCustom()
    {
        // Dash 
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isDashing)
        {
            StartCoroutine(StartDash(() => isDashing = false));
        }
        // Sprint 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = originalBaseSpeed * sprintMultiplier;
        }
        else
        {
            currentSpeed = originalBaseSpeed;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            
            Jump();
        }

        // Projectile shooting mechanic
        if (Input.GetMouseButtonDown(0))
        {
            // Shoot projectile
            SpawnFireball();
        }
    }

    


    IEnumerator StartDash(System.Action action)
    {
        isDashing = true;
        Vector3 initialPosition = transform.position;
        //cant use base.MoveDirection here as its not calculated outside of moving
        Vector3 DashDirection = new Vector3(verInput, 0f, -hozInput).normalized; 
        Vector3 targetPosition = transform.position + (DashDirection * dashSpeedMultiplier * dashDuration);

        float elapsed = 0f;
        while (elapsed < dashDuration)
        {
            
            elapsed += Time.deltaTime;
            float t = elapsed / dashDuration;
            
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            yield return null;
        }
       
        action();
    }


    /*MAJOR ISSUES WITH SPAWN FIREBALL AND START DASH TO BE FIXED AT LATER DATE LMAO */

    void SpawnFireball()
    {

        Vector3 movDir = new Vector3(hozInput, 0f, verInput).normalized; // normalized the vector of X and Z inputs 

        Quaternion desiredRotation = Quaternion.LookRotation(movDir, Vector3.up).normalized; // gets the desired rotation from move direction combined with the Y up vector 
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 5 * Time.deltaTime); //takes the current rotation, and moves it to the desired rotation i 



        Instantiate(fireballPrefab, transform.position, Quaternion.Slerp(transform.rotation, desiredRotation, .5f));
    }

    protected void Jump()
    {
        
        if (jumpLimit >= 1)
        {

            transform.Translate(Vector3.up * jumpForce * Time.deltaTime); // adds jumpforce on y axis up
            jumpLimit--;

        }

    }

}
