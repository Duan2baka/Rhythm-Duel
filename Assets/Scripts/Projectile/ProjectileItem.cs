using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileItem : MonoBehaviour{
    private float moveTime;
    private GameObject tmp, obj;
    private FloorController floorController;
    private int dmg;
    public void throwItem(int startX, int startY, bool startSide,int Dmg, float timeScale, int direction, bool damageArea){
        dmg = Dmg;
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        tmp = floorController.get(startX, startY, startSide);
        if(!tmp.GetComponent<FloorStatus>().getStatus()) return;
        transform.position = floorController.getPosition(startX, startY, startSide);
        //transform.localScale = new Vector3(5f, 5f, 5f);
        moveTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<RhythmController>().timeGap * timeScale;
        StartCoroutine(ThrowItemCoroutine(startX, startY, startSide, moveTime, direction, damageArea));
    }
    IEnumerator ThrowItemCoroutine(int currentX, int currentY, bool currentSide, float time, int direction, bool damageArea){
        while(true){
            float t = 0;
            while (t < time){
                t += Time.deltaTime;
                yield return null;
            }
            if(direction == -1){
                currentY = currentY - 1;
                if(currentY <= 0){
                    if(!currentSide){
                        currentSide = true;
                        currentY = 3;
                    }
                    else break;
                }
            }
            else{
                currentY = currentY + 1;
                if(currentY > 3){
                    if(currentSide){
                        currentSide = false;
                        currentY = 1;
                    }
                    else break;
                }
            }
            tmp = floorController.get(currentX, currentY, currentSide);
            if(!tmp.GetComponent<FloorStatus>().getStatus()) break;
            if(currentSide == damageArea){
                obj = floorController.FindObjectOn(currentX, currentY, currentSide);
                if(obj){
                    obj.GetComponent<HealthController>().takeDamage(dmg);
                    break;
                }
            }
            transform.position = floorController.getPosition(currentX, currentY, currentSide);
        }
        Destroy(gameObject);
    }
}
