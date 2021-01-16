
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] GameObject[] Platforms;
    [SerializeField] GameObject[] Obstacles;

    /// <summary>
    /// Пустое препядствие (отсутствие препядствия)
    /// </summary>
    private readonly GameObject emptyObstacle;

    /// <summary>
    /// максимальное число объектов в одной структуре
    /// </summary>
    [SerializeField] int maxObjects = 10;

    /// <summary>
    /// Контейнер для платформ
    /// </summary>
    [SerializeField] private Transform platformContainer;

    /// <summary>
    /// Контейнеры для препятствий
    /// 0 - левый
    /// 1 - средний
    /// 2 - центральный
    /// </summary>
    [SerializeField] private Transform[] ObstacleContainers;

    /// <summary>
    /// Последнее припятствия
    /// 0 - левый
    /// 1 - средний
    /// 2 - центральный
    /// </summary>
    private Transform[] lastObstacles; 
    
    private Transform lastPlatform;

    private void Start()
    {
        for (int i = 0; i < maxObjects; i++)
        {
            CreatePlatform();
        }
        lastObstacles = new Transform[ObstacleContainers.Length];
        for (int i = 0; i < ObstacleContainers.Length; i++)
        {
            for (int m = 0; m < maxObjects; m++)
            {
                CreateObstacle(i);
            }            
        }
    }
    private Transform CreateObj(GameObject[] objs, Transform parent, Transform lastObj, int indObstacle, string Tag)
    {
        GameObject newObj = Instantiate(
                objs[Random.Range(0, objs.Length)],
                lastObj == null ? parent.position : lastObj.GetComponent<PlatformController>().endPoint.position,
                Quaternion.identity,
                parent);

        newObj.GetComponent<PlatformController>().indObstacle = indObstacle;
        return newObj.transform;
    }
    
    public void CreateObstacle(int ind)
    {
        lastObstacles[ind] = CreateObj(Obstacles, ObstacleContainers[ind], lastObstacles[ind], ind, "Danger");
    }

     public void CreatePlatform()
    {
        lastPlatform = CreateObj(Platforms, platformContainer, lastPlatform, -1, "Platform");
    }
}
