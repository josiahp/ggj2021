using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public GameObject FadePanel;
    public bool FadeInAtStart = false;
    public float FadeInSeconds = 3f;

    private Image fadePanelImage;
    private float fadeTime;
    private float fadeTimeRemaining;

    public GameObject WinText;


    void Start() {
        fadePanelImage = FadePanel.GetComponent<Image>();

        if (FadeInAtStart) {
            FadeIn(FadeInSeconds);
        }
    }

    void Update() {
    }
    public void FadeIn(float seconds) {
        fadeTime = seconds;
        fadeTimeRemaining = seconds;
        StartCoroutine("DoFadeIn");
    }
    IEnumerator DoFadeIn() {
        while (fadeTimeRemaining > 0f) {
            fadeTimeRemaining -= Time.deltaTime;
            fadePanelImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, fadeTimeRemaining / fadeTime));
            yield return null;
        }
    }
    public void FadeOutToEnd(float seconds) {
        fadeTime = seconds;
        fadeTimeRemaining = seconds;
        StartCoroutine("DoFadeOutToEnd");
    }

    IEnumerator DoFadeOutToEnd() {
        while (fadeTimeRemaining > 0f) {
            fadeTimeRemaining -= Time.deltaTime;
            fadePanelImage.color = new Color(0f, 0f, 0f, 1f - Mathf.Lerp(0f, 1f, fadeTimeRemaining / fadeTime));
            yield return null;
        }
        SceneManager.LoadScene("End");
    }

    public void FadeOutToStart(float seconds) {
        fadeTime = seconds;
        fadeTimeRemaining = seconds;
        StartCoroutine("DoFadeOutToStart");
    }

    IEnumerator DoFadeOutToStart() {
        while (fadeTimeRemaining > 0f) {
            fadeTimeRemaining -= Time.deltaTime;
            fadePanelImage.color = new Color(0f, 0f, 0f, 1f - Mathf.Lerp(0f, 1f, fadeTimeRemaining / fadeTime));
            yield return null;
        }
        SceneManager.LoadScene("Game");
    }

    public void StartGame() {
        FadeOutToStart(3f);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void ShowWinText() {
        WinText.SetActive(true);
    }
}

