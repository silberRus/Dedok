using UnityEngine;

public class MoveObj : MonoBehaviour
{
    void Update()
    {
        transform.position -= Vector3.forward * WorldController.instance.worldSpeed * Time.deltaTime;
    }
}
