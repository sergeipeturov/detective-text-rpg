using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonScript : MonoBehaviour
{
    public void OnClick(int clickedButtonId)
    {
        Debug.Log(clickedButtonId);
        switch (clickedButtonId)
        {
            case 0:
                SceneManager.LoadScene("GameScene");
                break;
            case 1:
                break;
            case 2:
                Application.Quit();
                break;
            default:
                break;
        }
    }
}
