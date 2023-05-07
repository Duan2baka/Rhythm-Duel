using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : MonoBehaviour, Enemy{
    int idleCounter = 0;
    int X, Y, PlayerX, PlayerY;
    public GameObject batPrefab, spawnCircle;
    private GameObject player;
    PlayerMovement playerMovement;
    EnemyMovement enemyMovement;
    FloorController floorController;
    PositionController positionController;
    CardPanelController cardPanelController;
    GameObject obj, tmp, spawnedCircle;
    int[] cooldown;
    int[] fx, fy;
    private int rnd, cnt, spawnPeriod;
    bool flag;
    void Start(){
        flag = false;
        fx = new int[5]{0, 1, 0, -1, 0};
        fy = new int[5]{1, 0, -1, 0, 0};
        player = GameObject.FindGameObjectWithTag("Player");
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        positionController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PositionController>();
        cardPanelController = GameObject.FindGameObjectWithTag("CardPanel").GetComponent<CardPanelController>();
        idleCounter = 0;
        cooldown = new int[5]{10, 0, 0, 0, 0};
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
    }
    public void takeAction(){
        for(int i = 0; i <= 4; i ++)
            if(cooldown[i] != 0) cooldown[i] --;
        if(spawnPeriod > 0){
            spawnPeriod --;
            if(spawnPeriod % 2 == 1){
                rnd = Random.Range(1, 4);
                obj = Instantiate(batPrefab, transform.position, Quaternion.identity);
                obj.GetComponent<ProjectileItem>().throwItem(rnd,
                3, false, 10, 1.1f, -1, "Player");
            }
            if(spawnPeriod == 0){
                Destroy(spawnedCircle, 0.3f);
            }
        }
        else if(idleCounter != 0){
            idleCounter --;
        }
        else{
            X = gameObject.GetComponent<EnemyMovement>().getX();
            Y = gameObject.GetComponent<EnemyMovement>().getY();
            PlayerX = playerMovement.getX();
            PlayerY = playerMovement.getY();
            rnd = Random.Range(1, 10 + 1);/*
            if(rnd <= 5){
                rnd = Random.Range(1, 4 + 1);
                // Debug.Log(rnd);  
                if(rnd == 1 && cooldown[0] == 0){ /// 1
                    playerMovement.setReverse(3);
                    idleCounter = 3;
                    cooldown[0] = 10;
                }
                else if(rnd == 2 && cooldown[1] == 0){ /// 2
                    spawnPeriod = 8;
                    spawnedCircle = Instantiate(spawnCircle, transform.position - new Vector3(
                    0f, - gameObject.GetComponent<BoxCollider2D>().size.y / 2f, 0f)
                    , Quaternion.identity);
                    spawnedCircle.GetComponent<MagicCircleController>().init(gameObject);
                    cooldown[1] = 20;
                }
                else if(rnd == 3 && cooldown[2] == 0){ /// 3
                }
                else if(rnd == 4 && cooldown[3] == 0){ /// 4
                }
                else randomMoveAdjacent(X, Y);
            }
            else{
                randomMoveAdjacent(X, Y);
                idleCounter = 1;
            }*/
            
                    spawnPeriod = 8;
                    spawnedCircle = Instantiate(spawnCircle, transform.position - new Vector3(
                    0f, - gameObject.GetComponent<BoxCollider2D>().size.y / 2f, 0f)
                    , Quaternion.identity);
                    spawnedCircle.GetComponent<MagicCircleController>().init(gameObject);
                    cooldown[1] = 20;
        }
    }
    private void randomMove(int x, int y){
        // Debug.Log("move");
        cnt = 0;
        for(int i = 1; i <= 3; i ++)
            for(int j = 1; j <= 3; j ++)
                if(floorController.isAccessable(i, j, false)) cnt ++;
        cnt --;
        int rnd = Random.Range(1, cnt + 1);
        cnt = 0;
        for(int i = 1; i <= 3; i ++)
            for(int j = 1; j <= 3; j ++){
                if(!floorController.isAccessable(i, j, false)) continue;
                if(i == x && j == y) continue;
                cnt ++;
                if(cnt == rnd) enemyMovement.MoveTo(i, j);
            }
    }
    private void randomMoveAdjacent(int x, int y){
        // Debug.Log("move");
        cnt = 0;
        for(int i = 0; i < 4; i ++)
            if(floorController.isAccessable(x + fx[i], y + fy[i], false)) cnt ++;
        int rnd = Random.Range(1, cnt + 1);
        cnt = 0;
        for(int i = 0; i < 4; i ++){
            if(!floorController.isAccessable(x + fx[i], y + fy[i], false)) continue;
            cnt ++;
            if(cnt == rnd) enemyMovement.MoveTo(x + fx[i], y + fy[i]);
        }
    }
}
/*
skill id                 skill effect                           skill cooldown        idle       
    1                  spawn a knight                                15                3 
    2            reshuffle player's chips                            10                10          
    3  spawn a ghost from left to right, deal damage to player       6                 3
    4 get invisible and heal when invisible, ends when take damage   5                 1
*/