using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour{
    FloorController floorController;
    FlowManager flowManager;
    string target;
    int direction, T, dmg;
    public int currentX, currentY;
    public bool currentSide;
    private float timeGap, height;
    private Vector3 tmpVec, tmpTargetVec;
    GameObject tmp;
    public void init(int dir, string tag, int startX, int startY, bool startSide, int Dmg){
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        flowManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FlowManager>();
        target = tag;
        direction = dir;
        T = flowManager.getTimeStamp();
        currentX = startX;
        currentY = startY;
        currentSide = startSide;
        dmg = Dmg;
        tmp = floorController.get(startX, startY, startSide);
        if(!tmp.GetComponent<FloorStatus>().getStatus() || tmp.GetComponent<FloorStatus>().getBlocked()) return;
        transform.position = floorController.getPosition(startX, startY, startSide);
        timeGap = GameObject.FindGameObjectWithTag("GameController").GetComponent<RhythmController>().timeGap;
        height = tmp.GetComponent<BoxCollider2D>().size.y;
    }
    void Update(){
        if(!flowManager) return;
        if(T != flowManager.getTimeStamp()){
            T = flowManager.getTimeStamp();
            //StartCoroutine(MoveCoroutine(1, 1, true, 1));
        }
    }
    IEnumerator MoveCoroutine(int targetX, int targetY, bool targetSide, int depth){
        float t = 0;
        tmpVec = floorController.getPosition(currentX, currentY, currentSide);
        if(depth == 1){
            float time = timeGap * 0.8f * 0.2f;
            while (t < time){
                t += Time.deltaTime;
                transform.position = new Vector3(tmpVec.x, tmpVec.y + height * (t / time), tmpVec.z);
                yield return null;
            }
            StartCoroutine(MoveCoroutine(targetX, targetY, targetSide, depth + 1));
        }
        else if(depth == 2){
            Debug.Log(2);
            float time = timeGap * 0.8f * 0.6f;
            tmpTargetVec = floorController.getPosition(targetX, targetY, targetSide);
            while (t < time){
                t += Time.deltaTime;
                transform.position = new Vector3(tmpVec.x + (tmpTargetVec.x - tmpVec.x) * (t / time),
                tmpVec.y + (tmpTargetVec.y - tmpVec.y) * (t / time) + height, tmpVec.z);
                yield return null;
            }
            StartCoroutine(MoveCoroutine(targetX, targetY, targetSide, depth + 1));
        }
        else if(depth == 3){
            Debug.Log(3);
            float time = timeGap * 0.8f * 0.2f;
            tmpTargetVec = floorController.getPosition(targetX, targetY, targetSide);
            while (t < time){
                t += Time.deltaTime;
                transform.position = new Vector3(tmpTargetVec.x, tmpTargetVec.y + height * (1.0f - (t / time)), tmpTargetVec.z);
                yield return null;
            }
            StartCoroutine(MoveCoroutine(targetX, targetY, targetSide, depth + 1));
            floorController.get(currentX, currentY, currentSide).GetComponent<FloorStatus>().changeBlocked(-1);
            currentX = targetX;
            currentY = targetY;
            currentSide = targetSide;
            floorController.get(currentX, currentY, currentSide).GetComponent<FloorStatus>().changeBlocked(1);
        }
    }
}
