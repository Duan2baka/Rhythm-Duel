using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour{
    private float throwTime;
    public GameObject effectPrefab;
    private GameObject tmp;
    private int dmg;
    
    public void throwItem(Vector3 startPoint, Vector3 endPoint, GameObject targetFloor,int Dmg, float timeScale){
        dmg = Dmg;
        //transform.localScale = new Vector3(5f, 5f, 5f);
        throwTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<RhythmController>().timeGap * timeScale;
        StartCoroutine(ThrowItemCoroutine(startPoint, endPoint, throwTime, targetFloor));
    }
    IEnumerator ThrowItemCoroutine(Vector3 startPos, Vector3 endPos, float time, GameObject targetFloor){
        float t = 0;
        float g = 10f;
        float vy = (endPos.y - startPos.y + 0.5f * g * time * time) / time;
        float vx = (endPos.x - startPos.x) / time;
        float vz = 0;
        /*
        Debug.Log("endPos:" + endPos);
        Debug.Log("startPos:" + startPos);
        Debug.Log("vy:" + vy);*/
        while (t < time){
            t += Time.deltaTime;

            float x = startPos.x + vx * t;
            float y = startPos.y + vy * t - 0.5f * g * t * t;
            float z = startPos.z + vz * t;

            transform.position = new Vector3(x, y, z);

            yield return null;
        }
        SpawnEffect(transform.position);
        tmp = targetFloor.GetComponent<FloorStatus>().GetObject();
        if(tmp != null){
            tmp.GetComponent<HealthController>().takeDamage(dmg);
        }
        Destroy(gameObject);
    }
    
    public void SpawnEffect(Vector3 position){
        GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(effect, 0.3f);
    }
}
