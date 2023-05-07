using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour{
    void Start(){
        if(MyData.boss1)
            GameObject.Find("LichCrossImage").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        if(MyData.boss2)
            GameObject.Find("GhostCrossImage").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        if(MyData.boss3)
            GameObject.Find("VampireCrossImage").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        if(MyData.boss4)
            GameObject.Find("CursedArmorCrossImage").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }
}
