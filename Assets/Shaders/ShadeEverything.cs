using UnityEngine;

public class ApplyShaderToEverything : MonoBehaviour
{
    public Material shaderMaterial; 

    void Start()
    {
        
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        
        foreach (Renderer renderer in renderers)
        {
            
            renderer.material = shaderMaterial;
        }
    }
}
