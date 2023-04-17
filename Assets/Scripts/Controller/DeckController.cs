using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckController : MonoBehaviour{
    public List<GameObject> startList;
    public emptyCard emptyCard;
    public Card tmpCard;
    public GameObject drawArea;
    private List<Card> drawList;
    private List<Card> discardList;
    void Start(){
        drawArea = GameObject.Find("DrawArea");
        drawList = new List<Card>();
        if(startList != null){
            startList.Shuffle();
            for(int i = 0; i < startList.Count; i ++)
                drawList.Add(startList[i].GetComponent<Card>());
        }
        discardList = new List<Card>();
    }
    public Card peak(){
        return drawList[0];
    }
    void deck_update(){
        /// need to be further implemented
        drawArea.transform.Find("RemainingCard").GetComponent<Text>().text = "" + drawList.Count;
    }
    public Card draw(){
        if(drawList.Count == 0){ /// need to be further tested
            discardList.Shuffle();
            while(discardList.Count > 0){
                drawList.Add(discardList[0]);
                discardList.RemoveAt(0);
            }
        }
        tmpCard = drawList[0];
        drawList.RemoveAt(0);
        discardList.Add(tmpCard);
        deck_update();
        return tmpCard;
    }
}
