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
        voice = 0;
        for (int i = 0; i < blueVoiceNoteContainer.transform.childCount; i++) {
            blueVoiceNotes.Add(blueVoiceNoteContainer.transform.GetChild(i).gameObject);
        }
    }

    public bool CanUseVoice() {
        return voice < blueVoiceNoteContainer.transform.childCount;
    }

    public void UseVoice() {
        voice = Mathf.Clamp(voice+1, -1, blueVoiceNoteContainer.transform.childCount);
        UpdateVoiceIcons();
    }

    public void RecoverVoice() {
        voice = Mathf.Clamp(voice-1, -1, blueVoiceNoteContainer.transform.childCount);
        UpdateVoiceIcons();
    }

    public void UpdateVoiceIcons()
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
