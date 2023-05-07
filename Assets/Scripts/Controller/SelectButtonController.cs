using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectButtonController : MonoBehaviour{
    public void onLichClick(){
        SceneManager.LoadScene("LichScene");
    }
    public void onGhostClick(){
        SceneManager.LoadScene("GhostScene");
    }
    public void onVampireClick(){
        SceneManager.LoadScene("VampireScene");
    }
    public void onCursedArmorlick(){
        SceneManager.LoadScene("CursedArmorScene");
    }
    public void onBackClick(){
        SceneManager.LoadScene("MainUI");
    }
}
