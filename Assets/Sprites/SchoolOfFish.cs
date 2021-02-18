using UnityEngine;
using System;
using System.Collections;

public class SchoolOfFish : MonoBehaviour
{

    [SerializeField] private float interval;

    private string[] triggers = { "SwimLeft", "SwimRight" };
    private int triggerIndex = 0;

    void Start()
    {
        StartCoroutine(Swim());
    }

    private IEnumerator Swim() {
        string trigger = triggers[triggerIndex];
        int startFromChild = 0;
        while (true) {
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild((startFromChild + i) % transform.childCount).GetComponent<Animator>().SetTrigger(trigger);
                yield return new WaitForSeconds(0.15f);
            }
            triggerIndex++;
            triggerIndex = triggerIndex % triggers.Length;
            trigger = triggers[triggerIndex];
            startFromChild = UnityEngine.Random.Range(0, transform.childCount-1);
            yield return new WaitForSeconds(interval);
        }
    }

}
