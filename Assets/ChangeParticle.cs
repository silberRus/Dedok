using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParticle : MonoBehaviour
{
    public GameObject[] Partitions;

    private void ActivePartition(GameObject part)
    {
        for (int i = 0; i < Partitions.Length; i++)
        {
            bool setActive = part.Equals(Partitions[i]);
            Partitions[i].SetActive(setActive);
            if (setActive)
                Partitions[i].GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 120, 120, 220));

        for (int i = 0; i < Partitions.Length; i++)
        {
            if (GUILayout.Button(Partitions[i].name))
                ActivePartition(Partitions[i]);
        }
        GUILayout.EndArea();
    }
}
