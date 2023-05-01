using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideSwordCard : MonoBehaviour, Card{
    public GameObject effectPrefab;
    public int cost = 3;
    private int dmg = 30;
    public Sprite img;
    private PlayerMovement playerMovement;
    private FloorController floorController;
    private GameObject tmp;
    public void init(){
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
    }
    private void tryCast(int x, int y, bool playerSide){
        if(x > 0 && x <= 3 && y > 0 && y <= 3){
            Vector3 position = floorController.getPosition(x, y, playerSide);
            // Debug.Log(effectPrefab.GetComponent<Renderer>().bounds.size.y);
            GameObject obj = Instantiate(effectPrefab, position + new Vector3(0f, effectPrefab.GetComponent<Renderer>().bounds.size.y / 2f, 0f), Quaternion.identity);
            obj.layer = 1;
            Vector3 size = obj.GetComponent<Renderer>().bounds.size;
            float scale = floorController.get(x, y, playerSide).GetComponent<Renderer>().bounds.size.x / size.x;
            obj.transform.localScale = new Vector3(scale, scale, scale);

            tmp = floorController.FindObjectOn(x, y, playerSide);
            if(tmp != null)
                tmp.GetComponent<HealthController>().takeDamage(dmg);
        }
    }
    public void Cast(ref int currentMana){
        int x = playerMovement.getX(), y = playerMovement.getY();
        if(y == 3){
            for(int i = -1; i <= 1; i ++)
                tryCast(x + i, 1, false);
        }
        else{
            for(int i = -1; i <= 1; i ++)
                tryCast(x + i, y + 1, true);
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
    public void SpawnEffect(Vector3 position){
        GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(effect, 0.3f);
    }
    public string getChipName(){
        return "Wide sword Card";
    }
    public string getDescription(){
        return "attack the column in front of you.";
    }
}
