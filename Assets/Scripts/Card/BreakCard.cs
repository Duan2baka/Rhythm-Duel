using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCard : MonoBehaviour, Card{
    public int cost = 5;
    public Sprite img;
    private PlayerMovement playerMovement;
    private EnemyMovement enemyMovement;
    private FloorController floorController;
    public GameObject effectPrefab;
    
    public void init(){
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        enemyMovement = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>();
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
    }
    public void Cast(ref int currentMana){
        int x = playerMovement.getX(), y = playerMovement.getY();
        int ex = enemyMovement.getX(), ey = enemyMovement.getY();
        if(y < 3){
            floorController.get(x, y + 1, true).GetComponent<FloorStatus>().takeDamage(100);
            Vector3 position = floorController.getPositon(x, y + 1, true);
            // Debug.Log(effectPrefab.GetComponent<Renderer>().bounds.size.y);
            GameObject obj = Instantiate(effectPrefab, position + new Vector3(0f, effectPrefab.GetComponent<Renderer>().bounds.size.y / 2f, 0f), Quaternion.identity);
            obj.layer = 1;
            Vector3 size = obj.GetComponent<Renderer>().bounds.size;
            float scale = floorController.get(x, y + 1, true).GetComponent<Renderer>().bounds.size.x / size.x;
            obj.transform.localScale = new Vector3(scale, scale, scale);
            Destroy(obj, 0.3f);
        }
        else{
            floorController.get(x, 1, false).GetComponent<FloorStatus>().takeDamage(100);
            Vector3 position = floorController.getPositon(x, 1, false);
            // Debug.Log(effectPrefab.GetComponent<Renderer>().bounds.size.y);
            GameObject obj = Instantiate(effectPrefab, position + new Vector3(0f, effectPrefab.GetComponent<Renderer>().bounds.size.y / 2f, 0f), Quaternion.identity);
            obj.layer = 1;
            Vector3 size = obj.GetComponent<Renderer>().bounds.size;
            float scale = floorController.get(x, 1, false).GetComponent<Renderer>().bounds.size.x / size.x;
            obj.transform.localScale = new Vector3(scale, scale, scale);

            Destroy(obj, 0.3f);
        }
        currentMana -= cost;
        return;
    }
    public int getCost(){
        return cost;
    }
    public Sprite getSprite(){
        return img;
    }
}