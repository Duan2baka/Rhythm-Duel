using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour{
    private FloorController floorController;
    void Start(){
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
    }
    public GameObject getMouseObject(){
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null) {
            GameObject hitObject = hit.collider.gameObject;
            if(hitObject.tag == "Floor"){
                // Debug.Log("on " + hitObject.name);
                return hit.collider.gameObject;
            }
            if(hitObject.tag == "Enemy"){
                int x = hitObject.GetComponent<EnemyMovement>().getX();
                int y = hitObject.GetComponent<EnemyMovement>().getY();
                GameObject floor = floorController.get(x, y, false);
                // Debug.Log("on " + floor.name);
                return floor;
            }
        }
        return null;
    }
}
