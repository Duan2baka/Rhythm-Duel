using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmController : MonoBehaviour{
    public GameObject prefab, emptyObject;
    public float firstRhythmSpawn;
    private float speed;
    public float timeGap;
    float counter, tot;
    public float movePeriod;
    private RectTransform bar;
    private GameObject trigger;
    private int cnt = 0;
    private bool OK, on;
    private FlowManager flowManager;
    private ManaManager manaManager;
    private MusicController musicController;
    GameObject[] Rhythms;
    GameObject tmp;
    Vector3 Pos;
    List<float> nums;
    void Start(){
        OK = false;
        on = false;
        tot = 0f;
        bar = GameObject.Find("RhythmBar").GetComponent<RectTransform>();   
        counter = 0f;
        trigger = GameObject.FindGameObjectWithTag("TriggerZone");
        flowManager = GameObject.Find("GameController").GetComponent<FlowManager>();
        manaManager = GameObject.Find("GameController").GetComponent<ManaManager>();
        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
        timeGap = musicController.timeGap;
        firstRhythmSpawn = musicController.firstRhythmSpawn;
        Pos = GameObject.FindGameObjectWithTag("spawn").transform.position;
        var tmp1 = Instantiate(emptyObject, Pos, Quaternion.identity);
        tmp1.GetComponent<RectTransform>().SetParent(bar, false);
        speed = (tmp1.transform.position.x - trigger.transform.position.x) / movePeriod;
        nums = new List<float>();
        //Debug.Log(trigger.transform.position.x);
    }

    void Update(){
        tot += Time.deltaTime;
        counter += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.Q)){
            Debug.Log("current: " + tot);
            nums.Add(tot);
            float sum = 0;
            for(int i = 1; i < nums.Count; i++){
                sum = sum + nums[i] - nums[i - 1];
            }
            if(nums.Count > 1)
            Debug.Log("average:" + sum / (nums.Count - 1));
        }
        if(!on){
            if(counter >= firstRhythmSpawn){
                on = true;
                counter = 0;
            }
            return;
        }
        if(counter >= timeGap){
            counter = 0f;
            cnt++;
            /*Pos = new Vector3(bar.rect.width, 0, 0);
            var tmp1 = Instantiate(prefab, Pos, Quaternion.identity);
            tmp1.GetComponent<RectTransform>().SetParent(bar, false);
            tmp1.GetComponent<RhythmPointManager>().init();
            tmp1.name = "RhythmPoint " + cnt;*/
            Pos = GameObject.FindGameObjectWithTag("spawn").transform.position;
            var tmp1 = Instantiate(prefab, Pos, Quaternion.identity);
            //tmp1.GetComponent<RectTransform>().SetParent(bar, false);
            //tmp1.GetComponent<RhythmPointManager>().init();
            tmp1.name = "RhythmPoint " + cnt;
        }
        Rhythms = GameObject.FindGameObjectsWithTag("RhythmPoint");
        OK = false;
        tmp = null;
        foreach(GameObject entity in Rhythms){
            entity.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
            bool flag = entity.GetComponent<Renderer>().bounds.Intersects(trigger.GetComponent<Renderer>().bounds);
            if(flag){
                OK = true;
                if(tmp == null) tmp = entity;
                else if(entity.transform.position.x < tmp.transform.position.x) tmp = entity;
            }/*
            if(Mathf.Abs(Vector3.Distance(trigger.transform.position, entity.transform.position)) <= OKthreshold){
                OK = true;
                if(tmp == null) tmp = entity;
                else if(entity.transform.position.x < tmp.transform.position.x) tmp = entity;
            }*/
            if(entity.transform.position.x < trigger.transform.position.x && !flag){
                Destroy(entity);
                flowManager.exhausted = true;
                flowManager.punish = true;
                // Debug.Log("Miss");
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
        //Debug.Log("Some Key Pressed");
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
