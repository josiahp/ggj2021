using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{   
    public GameObject[] NPCs;
    public GameObject mom;

    public int untilMom;
    private int givenOut = 0;
    private GameObject[] spawnPoints;
    private GameObject[] momPoints;
    private GameObject worldMom;
    [HideInInspector] public List<GameObject> worldNPCs;

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
        worldMom.SetActive(false);

        List<GameObject> npcPoints = new List<GameObject>();
        npcPoints.AddRange(momPoints);
        npcPoints.RemoveAt(k);
        npcPoints.AddRange(spawnPoints);

        worldNPCs = new List<GameObject>();
        Shuffle();
        foreach(GameObject NPC in NPCs) {
            int pos = Mathf.CeilToInt(Random.Range(0, npcPoints.Count - 1));
            worldNPCs.Add(Instantiate(NPC, npcPoints[pos].transform.position, Quaternion.identity));
            npcPoints.RemoveAt(pos);
        }
    }

    public GameObject randomNPC() {
        Debug.Log(worldNPCs.Count);
        int k = Random.Range(0, worldNPCs.Count - 1);
        //Debug.Log(k);
        GameObject npc = worldNPCs[k];
        givenOut++;
        worldNPCs.RemoveAt(k);
        if (givenOut >= untilMom) {
            worldMom.SetActive(true);
            worldNPCs.Add(worldMom);
        }
        return npc;
    }
}
