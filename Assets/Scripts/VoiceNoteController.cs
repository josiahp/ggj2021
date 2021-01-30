using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceNoteController : MonoBehaviour
{
    public GameObject blueVoiceNoteContainer;
    private List<GameObject> blueVoiceNotes;
    private int voice;
    // Start is called before the first frame update
    void Start()
    {
        blueVoiceNotes = new List<GameObject>();
        for (int i = 0; i < blueVoiceNoteContainer.transform.childCount; i++) {
            blueVoiceNotes.Add(blueVoiceNoteContainer.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateVoiceIcons()
    {
        for (int i = 0; i < blueVoiceNotes.Count; i++)
        {
            if (voice <= i)
            {
                blueVoiceNotes[i].SetActive(true);
            }
            else
            {
                blueVoiceNotes[i].SetActive(false);
            }
        }
    }
}
