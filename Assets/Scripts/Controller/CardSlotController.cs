using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlotController : MonoBehaviour{
    private Text text;
    private bool isActivated;
    private Image image;
    void Start(){
        isActivated = false;
        text = transform.Find("Text").GetComponent<Text>();
        image = gameObject.GetComponent<Image>();
        text.enabled = image.enabled = false;
    }
    public bool get_availablity(){
        return isActivated;
    }
    public void set_activite(bool flag){
        isActivated = flag;
        text.enabled = image.enabled = flag;
    }
    public void set(Sprite img, int mana){
        image.sprite = img;
        text.text = "" + mana;
    }
}
