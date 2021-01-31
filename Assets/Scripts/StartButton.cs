using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    private void OnMouseOver()
    {
        Button MenuButton = GetComponent<Button>();    // 対象のボタン
        MenuButton.animator.SetTrigger("Highlighted");
    }

    private void OnMouseDown()
 {
     Debug.Log("click");
     SceneManager.LoadScene("Game");
 }

    
}
