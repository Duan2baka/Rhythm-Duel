using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmController : MonoBehaviour{
    public GameObject prefab;
    public float timeGap = 1f;
    public float speed = 10f;
    public float OKthreshold = 50f;
    float counter;

    private RectTransform  bar;
    private GameObject trigger;
    private int cnt = 0;
    private bool OK;
    private FlowManager flowManager;
    private ManaManager manaManager;

    GameObject[] Rhythms;
    GameObject tmp;
    Vector3 Pos;

    void Start(){
        OK = false;
        bar = GameObject.Find("RhythmBar").GetComponent<RectTransform>();   
        counter = 0f;
        trigger = GameObject.FindGameObjectWithTag("TriggerZone");
        flowManager = GameObject.Find("GameController").GetComponent<FlowManager>();
        manaManager = GameObject.Find("GameController").GetComponent<ManaManager>();
    }

    void Update(){
        counter += Time.deltaTime;
        if(counter > timeGap){
            counter = 0f;
            cnt++;
            Pos = new Vector3(bar.rect.width, 0, 0);
            var tmp1 = Instantiate(prefab, Pos, Quaternion.identity);
            tmp1.GetComponent<RectTransform>().SetParent(bar, false);
            tmp1.GetComponent<RhythmPointManager>().init();
            tmp1.name = "RhythmPoint " + cnt;
        }
        Rhythms = GameObject.FindGameObjectsWithTag("RhythmPoint");
        OK = false;
        tmp = null;
        foreach(GameObject entity in Rhythms){
            entity.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            
            Rect rect1 = entity.GetComponent<RectTransform>().rect;
            Rect rect2 = trigger.GetComponent<RectTransform>().rect;
            if(rect1.Overlaps(rect2)){
                OK = true;
                if(tmp == null) tmp = entity;
                else if(entity.transform.position.x < tmp.transform.position.x) tmp = entity;
            }/*
            if(Mathf.Abs(Vector3.Distance(trigger.transform.position, entity.transform.position)) <= OKthreshold){
                OK = true;
                if(tmp == null) tmp = entity;
                else if(entity.transform.position.x < tmp.transform.position.x) tmp = entity;
            }*/
            
            
            if(entity.transform.position.x < trigger.transform.position.x - OKthreshold){
                Destroy(entity);
                flowManager.exhausted = true;
                flowManager.punish = true;
                Debug.Log("Miss");
            }
        }
    }
    public bool getInput(){/*
        Rhythms = GameObject.FindGameObjectsWithTag("RhythmPoint");
        tmp = null;
        foreach(GameObject entity in Rhythms){
            if(entity.GetComponent<RhythmPointManager>().getStatus()) continue;
            Debug.Log("Entity: " + entity.name + "status: " + entity.GetComponent<RhythmPointManager>().getStatus());
            if(tmp == null) tmp = entity;
            else if(entity.transform.position.x < tmp.transform.position.x)
                tmp = entity;
        }
        if(tmp == null) return false;
        Debug.Log(tmp.name);
        tmp.GetComponent<RhythmPointManager>().disable();
        tmp.GetComponent<Image>().enabled = false;
        Debug.Log(tmp.transform.position);
        if(Mathf.Abs(Vector3.Distance(trigger.transform.position, tmp.transform.position)) <= OKthreshold){
            tmp.GetComponent<RhythmPointManager>().disable();
            tmp.GetComponent<Image>().enabled = false;
            // Debug.Log("OK " + tmp.GetComponent<RhythmPointManager>().getStatus());
            return true;
        }
        // Debug.Log("Miss");
        tmp.GetComponent<RhythmPointManager>().disable();
        tmp.GetComponent<Image>().enabled = false;
        return false;*/
        Debug.Log("Some Key Pressed");
        Destroy(tmp);
        if(OK){
            flowManager.exhausted = true;
            OK = false;
            Debug.Log("OK");
            return true;
        }
        Debug.Log("Miss");
        flowManager.punish = true;
        flowManager.exhausted = true;
        return false;
    }
    public void operation(bool valid){
        if(!valid){
            flowManager.punish = true;
            flowManager.exhausted = true;
        }
    }
}
