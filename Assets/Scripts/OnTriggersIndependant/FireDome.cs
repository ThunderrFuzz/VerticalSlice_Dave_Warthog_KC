/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDome : MonoBehaviour
{
    public Vector3 growth;
    
    public float targetScale;
    public GameObject spherePrefab;
    private GameObject newSphere;
    private bool halfwayReached = false;
    bool hasSpawned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // scales the firedome
        transform.localScale += growth * Time.deltaTime;
        checkHalfway(gameObject);
        if (newSphere != null)
        {
            newSphere.transform.localScale += growth * Time.deltaTime;
            checkHalfway(newSphere);
        }
        
        if(transform.localScale.x == targetScale)
        {
            Destroy(gameObject);
        }
        
    }

    public void spawnNewSphere()
    {
        newSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity, transform);
        
        
    }

    void checkHalfway(GameObject sphere)
    {

        float currentScale = sphere.transform.localScale.x;
        if (currentScale >= targetScale / 2 )
        {
            spawnNewSphere();
            hasSpawned = true;

        }
        else { return; }

    }
    IEnumerator spawnBoolReset(System.Action action)
    {
        action();
        yield return new WaitForSeconds(1f);
        
    }


}*/


/*using UnityEngine;
using System.Collections;
using UnityEngine.ProBuilder.Shapes;

public class FireDome : MonoBehaviour
{
    public Vector3 growth;
    public float targetScale;
    public GameObject spherePrefab;

    private GameObject currentSphere;
    bool hasSpawned;
    // Start is called before the first frame update
    void Start()
    {
        currentSphere = gameObject;
        hasSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentSphere.transform.localScale += growth * Time.deltaTime;
        if (currentSphere != null)
        {
            

            if (currentSphere.transform.localScale.x >= targetScale / 2)
            {
                spawnNewSphere();
                
            }
            if(currentSphere.transform.localScale.x >= targetScale)
            {
                Destroy(currentSphere);
                hasSpawned = false;
            }
        }
    }
    void spawnNewSphere()
    {
        

        //spawns new sphere
        if (!hasSpawned) {
            hasSpawned = true;
            currentSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity, transform);
            
        }
        //StartCoroutine(destroySphereWhenGrown(currentSphere));
    }

    IEnumerator destroySphereWhenGrown(GameObject sphere)
    {
        
        yield return new WaitUntil(() => sphere.transform.localScale.x >= targetScale);

        // wait until the sphere has reached the target size then destroy it using lamda expression 
        Destroy(sphere);
    }
}


*/

/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireDome : MonoBehaviour
{
    public Vector3 growth;
    public float targetScale;
    public GameObject spherePrefab;

    private List<GameObject> spheres = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Initially, spawn a single sphere
        SpawnNewSphere();
    }

    // Update is called once per frame
    void Update()
    {
        // Loop through all spawned spheres
        for (int i = 0; i < spheres.Count; i++)
        {
            // Increase the scale of each sphere
            spheres[i].transform.localScale += growth * Time.deltaTime;

            // If a sphere reaches halfway, spawn a new one
            if (spheres[i].transform.localScale.x >= targetScale / 2 && !spheres[i].GetComponent<Renderer>().isVisible)
            {
                SpawnNewSphere();
            }

            // If a sphere reaches target scale, remove it from the list and destroy it
            if (spheres[i].transform.localScale.x >= targetScale)
            {
                Destroy(spheres[i]);
                spheres.RemoveAt(i);
                i--; // Adjust the index since we removed an element from the list
            }
        }
    }

    void SpawnNewSphere()
    {
        // Instantiate a new sphere
        GameObject newSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity, transform);
        spheres.Add(newSphere);
    }
}*/


/*using UnityEngine;
using System.Collections.Generic;

public class FireDome : MonoBehaviour
{
    public Vector3 growth;
    public float targetScale;
    public GameObject spherePrefab;

    private List<GameObject> spheres = new List<GameObject>();

    void Start()
    {
        SpawnNewSphere();
    }

    void Update()
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            spheres[i].transform.localScale += growth * Time.deltaTime;

            if (spheres[i].transform.localScale.x >= targetScale / 2)
            {
                SpawnNewSphere();
            }

            if (spheres[i].transform.localScale.x >= targetScale)
            {
                Destroy(spheres[i]);
                spheres.RemoveAt(i);
                i--;
            }
        }
    }

    void SpawnNewSphere()
    {
        GameObject newSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity, transform);
        spheres.Add(newSphere);
    }
}
*/

/*using UnityEngine;
using System.Collections.Generic;

public class FireDome : MonoBehaviour
{
    public Vector3 growth;
    public float targetScale;
    public GameObject spherePrefab;

    private List<GameObject> spheres = new List<GameObject>();
    private bool canSpawnSphere = true;
    private float spawnCooldown = 1.0f; // Set a cooldown of 1 second

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            spheres[i].transform.localScale += growth * Time.deltaTime;

            if (spheres[i].transform.localScale.x >= targetScale)
            {
                Destroy(spheres[i]);
                spheres.RemoveAt(i);
                i--;
            }
        }

        // Check if we can spawn a new sphere
        if (canSpawnSphere)
        {
            SpawnNewSphere();
            canSpawnSphere = false;
            Invoke("ResetSpawnCooldown", spawnCooldown); // Reset spawn cooldown after 1 second
        }
    }

    void ResetSpawnCooldown()
    {
        canSpawnSphere = true;
    }

    void SpawnNewSphere()
    {
        GameObject newSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        spheres.Add(newSphere);
    }
}
*/






/*
    enlarge sphere continously 
    when shpere has reached target size destroy it 
        set sphere = newsphere here 


    if shpere is 1/2 target size, spawn second sphere.
    enlarge second sphere 
     
    increment total spheres 

 
 
 */


/*
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab; // Prefab of the sphere to spawn
    public float targetSize;      // Target size for the spheres
    public float growthRate;     // Rate at which spheres grow (per second)

    private int totalSpheres = 1;                  // Total number of spheres spawned

    void Start()
    {
        // Spawn the initial sphere
        
    }

    void Update()
    {
        // Get all active spheres (assuming you have a way to manage them)
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");

        foreach (GameObject sphere in spheres)
        {
            // Enlarge the sphere
            sphere.transform.localScale += Vector3.one * growthRate * Time.deltaTime;

            // Check for destroying the sphere when it reaches target size
            if (sphere.transform.localScale.x >= targetSize)
            {
                Destroy(sphere);
                continue; // Skip to the next sphere in the loop
            }

            // Check for spawning a second sphere at half target size
            if (sphere.transform.localScale.x >= targetSize / 2f && totalSpheres < 2)
            {
                SpawnSphere();
                totalSpheres++;
            }
        }
    }

    private void SpawnSphere()
    {
        // Instantiate a new sphere at the desired position (adjust as needed)
        GameObject newSphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        newSphere.transform.localScale = Vector3.one; // Set initial scale to 1
        newSphere.tag = "Sphere"; // Add a tag for easy identification (optional)
    }
}

//THIS ONE ALMOST WORKS GOOGLE GEMINI AI WAS BARD 


*/





















using UnityEngine;
using static UnityEngine.GridBrushBase;

public class SphereSpawner : MonoBehaviour
{
    public GameObject smallSpherePrefab; // Prefab of the small sphere
    public GameObject largeSpherePrefab; // Prefab of the large sphere
    public float targetSize;      // Target size for the spheres
    public float growthRate;     // Rate at which spheres grow (per second)
    public float rotationSpeed;
    
    private int totalSpheres = 1;                  // Total number of spheres spawned
    private bool hasLargeSpawned = false;        // Flag to track large sphere spawning

    void Start()
    {
        // Spawn the initial small sphere
        //SpawnSphere(smallSpherePrefab);
    }
    void Update()
    {

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
       /* 
        // Get all active spheres
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");

        foreach (GameObject sphere in spheres)
        {
            // Enlarge the sphere
            sphere.transform.localScale += Vector3.one  * growthRate * Time.deltaTime;

            // Check for destroying the sphere when it reaches target size
            if (sphere.transform.localScale.x >= targetSize)
            {
                Destroy(sphere);
                continue; // Skip to the next sphere in the loop
            }

            // Check for spawning a large sphere at half target size
            if (sphere.transform.localScale.x >= targetSize / 2f && !hasLargeSpawned)
            {
                SpawnSphere(largeSpherePrefab);
                hasLargeSpawned = true;
            }
        }*/
    }

    private void SpawnSphere(GameObject prefab)
    {
        // Instantiate a new sphere at the desired position (adjust as needed)
        GameObject newSphere = Instantiate(prefab, transform.position, Quaternion.identity);
        newSphere.transform.localScale = Vector3.one; // Set initial scale to 1
        newSphere.tag = "Sphere"; // Add a tag for easy identification (optional)
        totalSpheres++;
    }
}
