using System.Collections;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public float minZ = -10f;
    public float worldSpeed;
    public WorldBuilder WorldBuilder;

    public delegate void TryDelAndCreateObj();
    public event TryDelAndCreateObj OnPlatformMovement;
    
    public static WorldController instance;

    public int level;

    void Awake()
    {
        if (WorldController.instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            WorldController.instance = this;
            DontDestroyOnLoad(gameObject);
        }    
    }

    private void Start()
    {
        StartCoroutine(OnPlatformMovementCorutine());
    }

    private void Update()
    {
        transform.position -= Vector3.forward * worldSpeed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        WorldController.instance = null;
    }
    
        IEnumerator OnPlatformMovementCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (OnPlatformMovement != null)
                OnPlatformMovement();
        }
    }
}
