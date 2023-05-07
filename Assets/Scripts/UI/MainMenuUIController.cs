using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour{
    public void onPlayClick(){
        SceneManager.LoadScene("selection");
    }
    public void onTutorialClick(){
        SceneManager.LoadScene("Tutorial");
    }
    public void onExitClick(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
