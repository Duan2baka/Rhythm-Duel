using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCard : MonoBehaviour, Card{
    public int cost = 4;
    public Sprite img;
    private GameObject player;
    private EnemyMovement enemyMovement;
    private PlayerMovement playerMovement;
    private FloorController floorController;
    private MouseController mouseController;
    private GameObject tmp;
    public GameObject itemPrefab;
    private float throwTime;
    public void init(){
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        enemyMovement = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>();
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        mouseController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MouseController>();
    }
    public void Cast(ref int currentMana){
        int x = playerMovement.getX(), y = playerMovement.getY();
        int ex = enemyMovement.getX(), ey = enemyMovement.getY();
        tmp = mouseController.getMouseObject();
        if(tmp != null){
            Debug.Log(tmp.name);
            GameObject item = Instantiate(itemPrefab, player.transform.Find("middle").transform.position, Quaternion.identity);
            item.GetComponent<ThrowItem>().throwItem(player.transform.Find("middle").transform.position, tmp.transform.Find("Position").position, tmp);
        }
        else{
            GameObject item = Instantiate(itemPrefab, player.transform.Find("middle").transform.position, Quaternion.identity);
            item.GetComponent<ThrowItem>().throwItem(player.transform.Find("middle").transform.position, floorController.getPositon(x, y, false), floorController.get(x, y, false));
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
