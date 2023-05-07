using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour{
    FloorController floorController;
    FlowManager flowManager;
    string target;
    int Timer, dmg, currentX, currentY;
    bool currentSide;
    int status;
    private float timeGap;
    GameObject tmp;
    public GameObject aimPrefab, bloodPrefab;
    public void init(string tag, int startX, int startY, bool startSide, int Dmg){
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        flowManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FlowManager>();
        target = tag;
        Timer = flowManager.getTimeStamp();
        currentX = startX;
        currentY = startY;
        currentSide = startSide;
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
            if(status == 0){
                tmp = Instantiate(aimPrefab, floorController.getPosition(currentX, currentY, currentSide), Quaternion.identity);
            }
            else{
                Destroy(tmp);
                Instantiate(bloodPrefab, floorController.getPosition(currentX, currentY, currentSide), Quaternion.identity);
                tmp = floorController.FindObjectOn_WithTag(currentX, currentY, currentSide, target);
                if(tmp) tmp.GetComponent<HealthController>().takeDamage(dmg);
                Destroy(gameObject);
            }
            status ++;
        }
    }
}
