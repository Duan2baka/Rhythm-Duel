using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour{
    private Transform tmp;
    void Start(){
        
    }

    void Update(){}
    public void set(GameObject obj, GameObject pos){
        tmp = pos.transform.Find("Position");
        // Debug.Log(tmp.transform.position);
        obj.transform.position = tmp.position/* + new Vector3(0, obj.GetComponent<BoxCollider2D>().bounds.size.y / 2.0f, 0)*/;
        /*BoxCollider2D boxCollider2D = obj.GetComponent<BoxCollider2D>();
        float bottomY = boxCollider2D.bounds.min.y + boxCollider2D.bounds.extents.y;
        Vector3 newPosition = tmp.position + new Vector3(0, bottomY, 0);
        obj.transform.position = newPosition;*/
    }
}
