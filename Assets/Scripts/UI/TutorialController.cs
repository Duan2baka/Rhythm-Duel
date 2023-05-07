using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour{
    int index;
    public Sprite[] tmp;
    public Image img;
    public Button previous;
    public Button next;
    void Start(){
        index = 0;
        check();
    }
    public void OnNextClick(){
        index ++;
        check();
    }
    public void OnPreviousClick(){
        index --;
        check();
    }
    public void OnBackClick(){
        SceneManager.LoadScene("MainUI");
    }
    public void check(){
        img.sprite = tmp[index];
        if(index == 0) previous.interactable = false;
        else previous.interactable = true;
        if(index == tmp.Length - 1) next.interactable = false;
        else next.interactable = true;
    }
}
