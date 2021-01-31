using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{   
    public GameObject[] NPCs;
    public GameObject mom;
    private GameObject[] spawnPoints;
    private GameObject[] momPoints;
    private GameObject worldMom;
    private GameObject[] worldNPCs;

    private void Shuffle() {  
        int n = NPCs.Length;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n+1);
            GameObject value = NPCs[k];  
            NPCs[k] = NPCs[n];
            NPCs[n] = value;  
        }  
    }

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("NPCSpawnPoint");
        momPoints = GameObject.FindGameObjectsWithTag("MomSpawnPoint");

        foreach (GameObject spawn in spawnPoints) {
            spawn.transform.parent = this.transform;
        }
        foreach (GameObject spawn in momPoints) {
            spawn.transform.parent = this.transform;
        }

        int k = Mathf.CeilToInt(Random.Range(0, momPoints.Length - 1));
        worldMom = Instantiate(mom, momPoints[k].transform.position, Quaternion.identity);

        List<GameObject> npcPoints = new List<GameObject>();
        npcPoints.AddRange(momPoints);
        npcPoints.RemoveAt(k);
        npcPoints.AddRange(spawnPoints);

        worldNPCs = new GameObject[NPCs.Length];
        int i = 0;
        Shuffle();
        foreach(GameObject NPC in NPCs) {
            int pos = Mathf.CeilToInt(Random.Range(0, npcPoints.Count - 1));
            worldNPCs[i] = Instantiate(NPC, npcPoints[pos].transform.position, Quaternion.identity);
            i++;
            npcPoints.RemoveAt(pos);
        }
    }
}
