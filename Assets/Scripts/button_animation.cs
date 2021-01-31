using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button_animation : MonoBehaviour
{

    private void OnMouseOver()
    {
        Button MenuButton = GetComponent<Button>();    // 対象のボタン
        MenuButton.animator.SetTrigger("Highlighted");
    }

    
}
