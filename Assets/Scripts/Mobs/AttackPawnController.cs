using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPawnController : MonoBehaviour, Movement{
    FloorController floorController;
    FlowManager flowManager;
    string target;
    int direction, Timer, dmg, top, tmpX, tmpY, status;
    bool tmpSide, flag;
    float timeGap, height;
    public int currentX, currentY;
    public bool currentSide;
    int[] dirList;
    GameObject tmp;
    public GameObject aimPrefab;
    private Vector3 tmpVec, tmpTargetVec;
    public void init(int dir, string tag, int startX, int startY, bool startSide, int Dmg){
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        flowManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FlowManager>();
        target = tag;
        direction = dir;
        Timer = flowManager.getTimeStamp();
        currentX = startX;
        currentY = startY;
        currentSide = startSide;
        dmg = Dmg;
        tmp = floorController.get(startX, startY, startSide);
        if(!tmp.GetComponent<FloorStatus>().getStatus() || tmp.GetComponent<FloorStatus>().getBlocked()) Destroy(gameObject);
        floorController.get(currentX, currentY, currentSide).GetComponent<FloorStatus>().changeBlocked(1);
        transform.position = floorController.getPosition(startX, startY, startSide);
        timeGap = GameObject.FindGameObjectWithTag("GameController").GetComponent<RhythmController>().timeGap;
        height = tmp.GetComponent<BoxCollider2D>().size.y;
        dirList = new int[3];
        status = 0;
    }
    void TryMove(int dx, int dy){
        tmpX = currentX + dx;
        tmpY = currentY + dy;
        tmpSide = currentSide;
        if(tmpY <= 0)
            if(tmpSide == false){
                tmpSide = true;
                tmpY += 3;
            }
            else return;
        else if(tmpY > 3)
            if(tmpSide == true){
                tmpSide = false;
                tmpY -= 3;
            }
            else return;
        if(tmpX > 3 || tmpX <= 0) return;
        if(floorController.isAccessable(tmpX, tmpY, tmpSide))
            dirList[top ++] = dx;
        else return;
        if(floorController.FindObjectOn_WithTag(tmpX, tmpY, tmpSide, target))
            flag = true;
    }
    void Update(){
        if(!flowManager) return;
        // Debug.Log(gameObject.name + "T: " + T + " time stamp:" + flowManager.getTimeStamp());
        // Debug.Log(floorController.get(1,2,false).GetComponent<FloorStatus>().getBlocked());
        int T = flowManager.getTimeStamp();
        if(Timer != T){
            Timer = T;
            if(status == 0)
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
            else if(status == 1){
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
                top = 0; flag = false;
                TryMove(-1, direction);
                if(!flag) TryMove(1, direction);
                //Debug.Log(top);
                //Debug.Log(dirList);
                if(top == 0){ Destroy(gameObject); return; }
                if(flag){
                    tmpX = currentX + dirList[top - 1];
                    tmpY = currentY + direction;
                    tmpSide = currentSide;
                }
                else{
                    int rnd = Random.Range(0, top);
                    tmpX = currentX + dirList[rnd];
                    tmpY = currentY + direction;
                    tmpSide = currentSide;
                }
                if(tmpY <= 0 && tmpSide == false){
                    tmpSide = true;
                    tmpY += 3;
                }
                else if(tmpY > 3 && tmpSide == true){
                    tmpSide = false;
                    tmpY -= 3;
                }
                StartCoroutine(MoveCoroutine(tmpX, tmpY, tmpSide, 1));
            }
            else if(status == 4){
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);
            }
            else if(status >= 5){
                Destroy(gameObject);
                //Debug.Log("boom" + currentX + " " + currentY + " " + currentSide);
                floorController.get(currentX, currentY, currentSide).GetComponent<FloorStatus>().takeDamage(100, target);
            }
            status ++;
            //StartCoroutine(MoveCoroutine(1, 1, true, 1));
        }
    }

    
    IEnumerator MoveCoroutine(int targetX, int targetY, bool targetSide, int depth){
        float t = 0;
        //Debug.Log("current:" + currentX + " " + currentY + " " + currentSide + " target: "+ targetX + " " + targetY + " " + targetSide);
        tmpVec = floorController.getPosition(currentX, currentY, currentSide);
        tmpTargetVec = floorController.getPosition(targetX, targetY, targetSide);
        if(depth == 1){
            float time = timeGap * 0.8f * 0.2f;
            tmp = Instantiate(aimPrefab, tmpTargetVec, Quaternion.identity);
            Destroy(tmp, timeGap * 0.8f * 0.8f);
            while (t < time){
                t += Time.deltaTime;
                transform.position = new Vector3(tmpVec.x, tmpVec.y + height * (t / time), tmpVec.z);
                yield return null;
            }
            StartCoroutine(MoveCoroutine(targetX, targetY, targetSide, depth + 1));
        }
        else if(depth == 2){
            float time = timeGap * 0.8f * 0.6f;
            while (t < time){
                t += Time.deltaTime;
                transform.position = new Vector3(tmpVec.x + (tmpTargetVec.x - tmpVec.x) * (t / time),
                tmpVec.y + (tmpTargetVec.y - tmpVec.y) * (t / time) + height, tmpVec.z);
                yield return null;
            }
            StartCoroutine(MoveCoroutine(targetX, targetY, targetSide, depth + 1));
        }
        else if(depth == 3){
            float time = timeGap * 0.8f * 0.2f;
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
            tmp = floorController.FindObjectOn_WithTag(currentX, currentY, currentSide, target);
            if(tmp){
                tmp.GetComponent<HealthController>().takeDamage(dmg);
                Destroy(gameObject);
            }
        }
    }
    private void OnDestroy() {
        if(floorController) floorController.get(currentX, currentY, currentSide).GetComponent<FloorStatus>().changeBlocked(-1);
    }
    public int getX(){
        return currentX;
    }
    public int getY(){
        return currentY;
    }
    public bool getSide(){
        return currentSide;
    }
}
