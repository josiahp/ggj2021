using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(menuName = "NPC")]
public class NPC : ScriptableObject
{
    public GameObject Prefab;
    public Sprite Sprite;

    public float Scale = 1f;

    public AnimatorController AnimatorController;

    public GameObject Instantiate(Vector3 position, Quaternion rotation, GameObject parent) {
        var o = GameObject.Instantiate(Prefab, position, rotation);
        o.transform.localScale = new Vector3(Scale, Scale, 1f);
        o.GetComponent<SpriteRenderer>().sprite = Sprite;
        o.GetComponent<Animator>().runtimeAnimatorController = AnimatorController;
        o.name = this.name;
        o.transform.parent = parent.transform;
        return o;
    }
}
