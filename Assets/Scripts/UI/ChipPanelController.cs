using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipPanelController : MonoBehaviour{
    public string description;
    public string chipName;
    public void set(Card card){
        transform.Find("icon").GetComponent<Image>().sprite = card.getSprite();
        chipName = card.getChipName();
        description = card.getDescription();
    }
}
