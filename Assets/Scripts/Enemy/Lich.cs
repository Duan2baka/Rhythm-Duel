using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich : MonoBehaviour, Enemy{
    int idleCounter = 0;
    int X, Y, PlayerX, PlayerY;
    public GameObject knightPrefab, attackPawnPrefab, guardPawnPrefab, rookPrefab;
    private GameObject player;
    PlayerMovement playerMovement;
    EnemyMovement enemyMovement;
    FloorController floorController;
    PositionController positionController;
    CardPanelController cardPanelController;
    GameObject obj, tmp;
    int[] cooldown;
    int[] fx, fy;
    private int rnd, cnt;
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
        if(idleCounter != 0){
            idleCounter --;
        }
        else{
            X = gameObject.GetComponent<EnemyMovement>().getX();
            Y = gameObject.GetComponent<EnemyMovement>().getY();
            PlayerX = playerMovement.getX();
            PlayerY = playerMovement.getY();
            rnd = Random.Range(1, 10 + 1);
            if(rnd <= 7){
                rnd = Random.Range(1, 6 + 1);
                // Debug.Log(rnd);  
                if((rnd == 1  || rnd == 5) && cooldown[0] == 0){ /// 1
                    obj = Instantiate(knightPrefab, transform.position, Quaternion.identity);
                    rnd = Random.Range(1, 3 + 1);
                    obj.GetComponent<KnightController>().init(-1, "Player", rnd, 3, false, 10);
                    obj = Instantiate(knightPrefab, transform.position, Quaternion.identity);
                    rnd = Random.Range(1, 3 + 1);
                    obj.GetComponent<KnightController>().init(-1, "Player", rnd, 3, false, 10);
                    idleCounter = 2;
                    cooldown[0] = 4;
                }
                else if(rnd == 2 && cooldown[1] == 0){ /// 2
                    obj = Instantiate(attackPawnPrefab, transform.position, Quaternion.identity);
                    rnd = Random.Range(1, 3 + 1);
                    obj.GetComponent<AttackPawnController>().init(-1, "Player", rnd, 1, false, 10);
                    idleCounter = 2;
                    cooldown[1] = 5;
                }
                else if(rnd == 3 && cooldown[2] == 0){ /// 3
                    obj = Instantiate(guardPawnPrefab, transform.position, Quaternion.identity);
                    rnd = Random.Range(1, 3 + 1);
                    obj.GetComponent<GuardPawnController>().init(-1, "Player", rnd, 1, false, 10);
                    idleCounter = 2;
                    cooldown[2] = 5;
                }
                else if((rnd == 4 || rnd == 6) && cooldown[3] == 0){ /// 4
                    cnt = 0;
                    for(int i = 1; i <= 3; i ++)
                        for(int j = 1; j <= 3; j ++)
                            if(floorController.isAccessable(i, j, false)) cnt ++;
                    rnd = Random.Range(1, cnt + 1);
                    cnt = 0;
                    for(int i = 1; i <= 3; i ++)
                        for(int j = 1; j <= 3; j ++){
                            if(!floorController.isAccessable(i, j, false)) continue;
                            cnt ++;
                            if(rnd == cnt){
                                obj = Instantiate(rookPrefab, floorController.getPosition(i, j, true), Quaternion.identity);
                                obj.GetComponent<RookController>().init(-1, "Player", i, j, false, 10);
                            }
                        }
                    cnt = 0;
                    for(int i = 1; i <= 3; i ++)
                        for(int j = 1; j <= 3; j ++)
                            if(floorController.isAccessable(i, j, false)) cnt ++;
                    rnd = Random.Range(1, cnt + 1);
                    cnt = 0;
                    for(int i = 1; i <= 3; i ++)
                        for(int j = 1; j <= 3; j ++){
                            if(!floorController.isAccessable(i, j, false)) continue;
                            cnt ++;
                            if(rnd == cnt){
                                obj = Instantiate(rookPrefab, floorController.getPosition(i, j, true), Quaternion.identity);
                                obj.GetComponent<RookController>().init(-1, "Player", i, j, false, 10);
                            }
                        }
                    idleCounter = 2;
                    cooldown[3] = 4;
                }
                else randomMoveAdjacent(X, Y);
            }
            else{
                randomMoveAdjacent(X, Y);
                idleCounter = 1;
            }
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