using UnityEngine;
using UnityEngine.AI;

public class HyeanaSpawn : MonoBehaviour
{
    [Header("Total of hyena")]
    public int hyenaSpawnCount;
    [Header("Hyena Prefab")]
    public GameObject hyenaPrefab;
    private Vector3[] spawnPoints;
    private bool hasSpawned = false;

    [Header("Spawn location objects")]
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;


    void Update()
    {
        // Update the destination of hyenas to follow the player's movement every frame
        if(hasSpawned) UpdateHyenaDestinations();
    }

    void UpdateHyenaDestinations()
    {
        // Find the current position of the player
        Vector3 playerPosition = FindAnyObjectByType<DaveMovement>().gameObject.transform.position;

        // Update the destination of each hyena
        foreach (GameObject hyena in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            NavMeshAgent hyenaNavMesh = hyena.GetComponent<NavMeshAgent>();
            hyenaNavMesh.SetDestination(playerPosition);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if player enteres triger zone, populate spawnpoints
            PopulateSpawnPoints();
            //then spawn the hyenas 
            Spawn();
        }
    }

    void Spawn()
    {
        hasSpawned = true;
        //Spawn hyenas until max count is met
        for (int i = 0; i < hyenaSpawnCount; i++)
        {
            
            GameObject hyena = Instantiate(hyenaPrefab, spawnPoints[i], Quaternion.identity);
            /*NavMeshAgent hyenaNavMesh = hyena.GetComponent<NavMeshAgent>();
            hyenaNavMesh.SetDestination(FindAnyObjectByType<DaveMovement>().gameObject.transform.position);*/
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        //sets bounds box
        Bounds bounds = collider.bounds;
        //random range between the X and Z postions of the 
        Vector3 randomPoint = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x), 
            bounds.min.y+1,
            Random.Range(bounds.min.z, bounds.max.z)
        );
         
        return randomPoint;
    }

    void PopulateSpawnPoints()
    {
        // Get colliders of spawn point objects
        Collider collider1 = spawnPoint1.GetComponent<Collider>();
        Collider collider2 = spawnPoint2.GetComponent<Collider>();

        // Populate spawnPoints array
        spawnPoints = new Vector3[hyenaSpawnCount];

        for (int i = 0; i < hyenaSpawnCount; i++)
        {
            // Alternate between using each collider option
            Collider collider = (i % 2 == 0) ? collider1 : collider2;
            //Add value to spawn points
            spawnPoints[i] = GetRandomPointInCollider(collider);
        }
    }
}
