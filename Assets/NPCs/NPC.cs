using UnityEngine;

[CreateAssetMenu(menuName = "NPC")]
public class NPC : ScriptableObject
{
    public GameObject Prefab;


    public GameObject Instantiate(Vector3 position, Quaternion rotation, GameObject parent) {
        var o = GameObject.Instantiate(Prefab, position, rotation);
        o.name = this.name;
        o.transform.parent = parent.transform;
        return o;
    }
}
