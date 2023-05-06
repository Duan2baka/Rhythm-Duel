using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShot : MonoBehaviour, Card{
    public GameObject effectPrefab;
    public int cost = 3;
    private int dmg = 10;
    public Sprite img;
    private PlayerMovement playerMovement;
    private EnemyMovement enemyMovement;
    private FloorController floorController;
    private GameObject tmp;
    public void init(){
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        enemyMovement = GameObject.FindGameObjectWithTag("MainEnemy").transform.parent.gameObject.GetComponent<EnemyMovement>();
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
    }
    private void tryCast(int x, int y, bool playerSide){
        if(x > 0 && x <= 3 && y > 0 && y <= 3){
            Vector3 position = floorController.getPosition(x, y, playerSide);
            // Debug.Log(effectPrefab.GetComponent<Renderer>().bounds.size.y);
            GameObject obj = Instantiate(effectPrefab, position + new Vector3(0f, effectPrefab.GetComponent<Renderer>().bounds.size.y / 2f, 0f), Quaternion.identity);
            Vector3 size = obj.GetComponent<Renderer>().bounds.size;
            float scale = floorController.get(x, y, playerSide).GetComponent<Renderer>().bounds.size.x / size.x;
            obj.transform.localScale = new Vector3(scale, scale, scale);

            tmp = floorController.FindObjectOn_WithTag(x, y, playerSide, "Enemy");
            if(tmp != null)
                tmp.GetComponent<HealthController>().takeDamage(dmg);
        }
        else{
            if(y <= 0 && !playerSide && x > 0 && x <= 3){
                Vector3 position = floorController.getPosition(x, y + 3, true);
                // Debug.Log(effectPrefab.GetComponent<Renderer>().bounds.size.y);
                GameObject obj = Instantiate(effectPrefab, position + new Vector3(0f, effectPrefab.GetComponent<Renderer>().bounds.size.y / 2f, 0f), Quaternion.identity);
                Vector3 size = obj.GetComponent<Renderer>().bounds.size;
                float scale = floorController.get(x, y + 3, true).GetComponent<Renderer>().bounds.size.x / size.x;
                obj.transform.localScale = new Vector3(scale, scale, scale);

                tmp = floorController.FindObjectOn_WithTag(x, y + 3, true, "Enemy");
                if(tmp != null)
                    tmp.GetComponent<HealthController>().takeDamage(dmg);
            }
            else if(y > 3 && playerSide && x > 0 && x <= 3){
                Vector3 position = floorController.getPosition(x, y - 3, false);
                // Debug.Log(effectPrefab.GetComponent<Renderer>().bounds.size.y);
                GameObject obj = Instantiate(effectPrefab, position + new Vector3(0f, effectPrefab.GetComponent<Renderer>().bounds.size.y / 2f, 0f), Quaternion.identity);
                Vector3 size = obj.GetComponent<Renderer>().bounds.size;
                float scale = floorController.get(x, y - 3, false).GetComponent<Renderer>().bounds.size.x / size.x;
                obj.transform.localScale = new Vector3(scale, scale, scale);

                tmp = floorController.FindObjectOn_WithTag(x, y - 3, false, "Enemy");
                if(tmp != null)
                    tmp.GetComponent<HealthController>().takeDamage(dmg);
            }
        }
    }
    public void Cast(ref int currentMana){
        int x = playerMovement.getX(), y = playerMovement.getY();
        bool playerSide = true;
        int targetX = x, targetY = y;
        bool targetSide = false;
        for(int yi = y + 1; ; yi ++){
            if(yi > 3){
                if(playerSide){
                    playerSide = false;
                    yi = 1;
                }
                else break;
            }
            if(floorController.FindObjectOn_WithTag(x, yi, playerSide, "Enemy")){
                targetX = x;
                targetY = yi;
                targetSide = playerSide;
                break;
            }
        }
        tryCast(targetX, targetY, targetSide);
        tryCast(targetX, targetY + 1, targetSide);
        currentMana -= cost;
        return;
    }
    public int getCost(){
        return cost;
    }
    public Sprite getSprite(){
        return img;
    }
    public void SpawnEffect(Vector3 position){
        GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(effect, 0.3f);
    }
    public string getChipName(){
        return "Bubble shot";
    }
    public string getDescription(){
        return "Shoot an enemy and the grid behind him.";
    }
}
