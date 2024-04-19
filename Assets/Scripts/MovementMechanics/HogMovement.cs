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
            StartCoroutine(StartDash());
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

        // Projectile shooting mechanic
        if (Input.GetMouseButtonDown(0))
        {
            // Shoot projectile
            SpawnFireball();
        }
    }


    IEnumerator StartDash()
     {
         isDashing = true;
         Vector3 initialPosition = transform.position;
         Vector3 targetPosition = transform.position  + (base.moveDirection * dashSpeedMultiplier * dashDuration);
         float elapsed = 0f;
         while (elapsed < dashDuration)
         {
             float t = elapsed / dashDuration;
             transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
             elapsed += Time.deltaTime;
             yield return null;
         }
         transform.position = targetPosition;
         isDashing = false;
     }
    

    /*MAJOR ISSUES WITH SPAWN FIREBALL AND START DASH TO BE FIXED AT LATER DATE LMAO */

    void SpawnFireball()
    {
        
        Vector3 movDir = new Vector3(hozInput, 0f, verInput).normalized; // normalized the vector of X and Z inputs 
       
        Quaternion desiredRotation = Quaternion.LookRotation(movDir, Vector3.up); // gets the desired rotation from move direction combined with the Y up vector 
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 5 * Time.deltaTime); //takes the current rotation, and moves it to the desired rotation i 
        


        Instantiate(fireballPrefab, transform.position, Quaternion.Slerp(transform.rotation, desiredRotation,.5f));
    }
}
