using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject ObjSpawn;
    [SerializeField] private float timeSpawn = 1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObj), 1f, 10f);        
    }

    private void SpawnObj()
    {
        GameObject obj = Instantiate(ObjSpawn, this.gameObject.transform.position, this.gameObject.transform.rotation);
        //obj.GetComponent<MoveObj>().Prop = Prop;
    }
}
