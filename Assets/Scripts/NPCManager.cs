using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC Manager")]
public class NPCManager : ScriptableObject
{
    public List<NPC> npcDefinitions;
    private List<GameObject> NPCs;

    public List<Vector3> spawnPoints;
    private List<Vector3> availableSpawnPoints;

    public uint limit;

    void OnEnable()
    {
        NPCs = new List<GameObject>();
        availableSpawnPoints = new List<Vector3>(spawnPoints);
    }

    public void SpawnNPCs(GameObject parent)
    {
        if (limit == 0) { return; }
        while (limit > NPCs.Count && availableSpawnPoints.Count > 0)
        {
            var sp = GetRandomAvailableSpawnPoint();
            SpawnRandomNPC(parent, sp, Quaternion.identity);
            availableSpawnPoints.Remove(sp);
        }
    }

    public GameObject SpawnRandomNPC(GameObject parent, Vector3 p, Quaternion r)
    {
        if (npcDefinitions.Count == 0) { return null; }

        var npc = npcDefinitions[Mathf.FloorToInt(Random.Range(0, npcDefinitions.Count))];
        var npcInst = npc.Instantiate(p, r, parent);

        NPCs.Add(npcInst);
        return npcInst;
    }

    public GameObject GetRandomNPC()
    {
        if (NPCs.Count == 0) { return null; }

        return NPCs[Mathf.FloorToInt(Random.Range(0, NPCs.Count))];
    }

    public Vector3 GetRandomAvailableSpawnPoint()
    {
        if (availableSpawnPoints.Count == 0)
        {
            Debug.LogWarning("GetRandomAvailableSpawnPoint called with no remaining spawn points. Returning {0,0,0}");
            return Vector3.zero;
        }

        return availableSpawnPoints[Mathf.FloorToInt(Random.Range(0, availableSpawnPoints.Count))];
    }
}
