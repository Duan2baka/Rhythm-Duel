using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodRowController : MonoBehaviour{
    FloorController floorController;
    FlowManager flowManager;
    string target;
    int direction, Timer, dmg, currentX, currentY, tmpX, tmpY;
    bool currentSide, tmpSide;
    int status;
    private float timeGap;
    GameObject tmp;
    public GameObject bloodController;
    public void init(string tag, int startX, int startY, bool startSide, int Dmg, int dir){
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        flowManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FlowManager>();
        target = tag;
        Timer = flowManager.getTimeStamp();
        currentX = startX;
        currentY = startY;
        currentSide = startSide;
        direction = dir;
        dmg = Dmg;
        timeGap = GameObject.FindGameObjectWithTag("GameController").GetComponent<RhythmController>().timeGap;
        status = 0;
    }
    void Update(){
        if(!flowManager) return;
        // Debug.Log(gameObject.name + "T: " + T + " time stamp:" + flowManager.getTimeStamp());
        // Debug.Log(floorController.get(1,2,false).GetComponent<FloorStatus>().getBlocked());
        int T = flowManager.getTimeStamp();
        if(Timer != T){
            Timer = T;
            
            tmpX = currentX;
            tmpY = currentY + direction * status;
            tmpSide = currentSide;
            if(tmpY <= 0)
                if(tmpSide == false){
                    tmpSide = true;
                    tmpY += 3;
                }
                else{
                    Destroy(gameObject);
                    return;
                }
            else if(tmpY > 3)
                if(tmpSide == true){
                    tmpSide = false;
                    tmpY -= 3;
                }
                else{
                    Destroy(gameObject);
                    return;
                }
            if(tmpX > 3 || tmpX <= 0){
                Destroy(gameObject);
                return;
            }

            tmp = Instantiate(bloodController, Vector3.zero, Quaternion.identity);
            tmp.GetComponent<BloodController>().init("Player", tmpX, tmpY, tmpSide, 10);
            status ++;
        }
    }
}
