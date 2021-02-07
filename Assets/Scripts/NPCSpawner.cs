using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCSpawner : MonoBehaviour
{
    public NPCManager npcManager;
    public NPCManager mamaManager;
    void Start()
    {
        npcManager.SpawnNPCs(gameObject);
        mamaManager.SpawnNPCs(gameObject);

    }
}
