using UnityEngine;

public class WorldBuilder : MonoBehaviour
{    
    /// <summary>
    /// Пустое препятствие (отсутствие препятствия)
    /// </summary>
    [SerializeField] GameObject emptyConteiner;

    /// <summary>
    /// максимальное число объектов в одной структуре
    /// </summary>
    [SerializeField] int maxObjects = 10;

    /// <summary>
    /// Чем больше тем реже будут выпадать препятствия
    /// </summary>
    [SerializeField] int Rate = 100;

    [SerializeField] private Transform platformContainer;
    [SerializeField] private Transform[] ObstacleContainers;
    [SerializeField] private Transform[] SurpiseContainers;

    [SerializeField] GameObject[] Platforms;
    [SerializeField] GameObject[] Obstacles;
    [SerializeField] GameObject[] Surprises;

    private Transform[] lastObstacles;
    private Transform[] lastSurprises;
    private Transform lastPlatform;

    delegate void CreateObjMore(int i);

    private void Start()
    {
        for (int i = 0; i < maxObjects; i++)
        {
            CreatePlatform();
        }
        
        lastObstacles = new Transform[ObstacleContainers.Length];
        CreateObjsMore(CreateObstacle, ObstacleContainers.Length);

        lastSurprises = new Transform[SurpiseContainers.Length];
        CreateObjsMore(CreateSurprise, SurpiseContainers.Length);
    }

    private void CreateObjsMore(CreateObjMore funCreate, int roadsNum)
    {
        for (int i = 0; i < roadsNum; i++)
        {
            for (int m = 0; m < maxObjects; m++)
            {
                funCreate(i);
            }
        }
    }

    private Transform CreateObj(GameObject obj, Transform parent, Transform lastObj, int indObstacle, string Tag)
    {
        GameObject newObj = Instantiate(
                obj,
                lastObj == null ? parent.position : lastObj.GetComponent<PlatformController>().endPoint.position,
                Quaternion.identity,
                parent);

        newObj.GetComponent<PlatformController>().indObstacle = indObstacle;
        return newObj.transform;
    }
    
    public void CreateObstacle(int ind)
    {
        lastObstacles[ind] = CreateObj(
            Random.Range(WorldController.instance.level, Rate) > WorldController.instance.level ? emptyConteiner : Obstacles[Random.Range(0, Obstacles.Length)],
            ObstacleContainers[ind], lastObstacles[ind], ind, "Danger");
    }

    public void CreateSurprise(int ind)
    {
        lastSurprises[ind] = CreateObj(
            Random.Range(WorldController.instance.level, Rate) > WorldController.instance.level ? emptyConteiner : Surprises[Random.Range(0, Surprises.Length)],
            SurpiseContainers[ind], lastSurprises[ind], ind, "Surpise");
    }

    public void CreatePlatform()
    {
        lastPlatform = CreateObj(Platforms[Random.Range(0, Platforms.Length)], platformContainer, lastPlatform, -1, "Platform");
    }
}
