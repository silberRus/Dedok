using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform endPoint;

    public int indObstacle;

    /// <summary>
    /// Перфабы из которых может создаваться
    /// </summary>
    public GameObject[] perfabs;
    
    private void Start()
    {
        WorldController.instance.OnPlatformMovement += TryAndDelPlatform;
    }
    
    private void TryAndDelPlatform()
    {
        if (transform.position.z < WorldController.instance.minZ)
        {
            if (indObstacle == -1)
                WorldController.instance.WorldBuilder.CreatePlatform();
            else
                WorldController.instance.WorldBuilder.CreateObstacle(indObstacle);
            Destroy(gameObject);
        }        
    }

    private void Update()
    {
        transform.position -= Vector3.forward * WorldController.instance.worldSpeed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        WorldController.instance.OnPlatformMovement -= TryAndDelPlatform;
    }
}
