using System.Collections;
using UnityEngine;
using Cinemachine;

public class CutsceneTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera dollyCamera; // dolly Cinemachine camera
    private GameObject player;
    private CinemachineDollyCart dollyCart;
    private Transform originalParent; 
    private DaveMovement daveMovement;
    private bool isInCutscene = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        daveMovement = player.GetComponent<DaveMovement>();
        dollyCart = FindObjectOfType<CinemachineDollyCart>();
        // Store original parent
        originalParent = player.transform.parent;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            daveMovement.enabled = false;
            isInCutscene = true;
            // Set dolly cart speed
            dollyCart.m_Speed = 38f;
            // Make player a child of the dolly cart
            player.transform.SetParent(dollyCart.transform, true);
            // Start updating player position
            StartCoroutine(UpdatePlayerPosition());
            // Wait for the duration of the cutscene
            float cutsceneDuration = GetCutsceneDuration();
            Invoke("EndCutscene", cutsceneDuration);
        }
    }

    private float GetCutsceneDuration()
    {
        // duration for the dolly cart to reach the desired waypoint
        float waypoint17Distance = Vector3.Distance(dollyCart.m_Path.EvaluatePositionAtUnit(17, CinemachinePathBase.PositionUnits.PathUnits), dollyCart.transform.position);
        float speed = dollyCart.m_Speed > 0 ? dollyCart.m_Speed : 1f;
        return waypoint17Distance / speed;
    }
    private void EndCutscene()
    {
        isInCutscene = false;
        // Remove player from being a child of the dolly cart
        player.transform.SetParent(originalParent, true);
        // re-enable player control using DaveMovement script
        daveMovement.enabled = true;
        // reset player rotation to its default
        player.transform.rotation = originalParent.rotation;
    }
    IEnumerator UpdatePlayerPosition()
    {
        // players cutscene start pos
        Vector3 initialPlayerPosition = player.transform.position;
        while (isInCutscene)
        {
            // normalized time along the path based on the current pos of the dolly cart
            float normalizedTime = Vector3.Distance(dollyCart.transform.position, dollyCart.m_Path.EvaluatePositionAtUnit(17, CinemachinePathBase.PositionUnits.PathUnits)) / GetCutsceneDuration();
            // Interpolate the curr player pos with its initial pos and the curr dolly cart pos
            player.transform.position = Vector3.Lerp(initialPlayerPosition, dollyCart.transform.position, normalizedTime);
            yield return null;
        }
    }


}
