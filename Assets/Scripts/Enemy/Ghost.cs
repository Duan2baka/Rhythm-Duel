using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, Enemy{
    int idleCounter = 0;
    int X, Y, PlayerX, PlayerY;
    public GameObject recyclePrefab;
    public GameObject throwablePrefab, shovelPrefab, lilGhostPrefab;
    private GameObject player;
    PlayerMovement playerMovement;
    EnemyMovement enemyMovement;
    FloorController floorController;
    PositionController positionController;
    CardPanelController cardPanelController;
    GameObject obj, tmp;
    int[] cooldown, randomlist;
    int[] fx, fy;
    private int rnd, cnt;
    void Start(){
        fx = new int[5]{0, 1, 0, -1, 0};
        fy = new int[5]{1, 0, -1, 0, 0};
        player = GameObject.FindGameObjectWithTag("Player");
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        positionController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PositionController>();
        cardPanelController = GameObject.FindGameObjectWithTag("CardPanel").GetComponent<CardPanelController>();
        idleCounter = 0;
        cooldown = new int[5]{0, 0, 0, 0, 0};
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
    }
    public void takeAction(){
        for(int i = 0; i <= 4; i ++)
            if(cooldown[i] != 0) cooldown[i] --;
        if(idleCounter != 0){
            idleCounter --;
            //Debug.Log("enemyMovement: idle");
        }
        else{
            // ****** get the position

            X = gameObject.GetComponent<EnemyMovement>().getX();
            Y = gameObject.GetComponent<EnemyMovement>().getY();
            PlayerX = playerMovement.getX();
            PlayerY = playerMovement.getY();

            //**************************
            rnd = Random.Range(1, 10 + 1);
            if(rnd <= 5){
                rnd = Random.Range(1, 4 + 1);
                // Debug.Log(rnd);  
                if(rnd == 1 && cooldown[0] == 0){ /// 1
                }
                else if(rnd == 2 && cooldown[1] == 0){ /// 2
                    cardPanelController.shuffleHand();
                    obj = Instantiate(recyclePrefab, player.transform.position - new Vector3(
                    0f, - player.GetComponent<BoxCollider2D>().size.y, 0f), Quaternion.identity);
                    obj.GetComponent<RecycleController>().init(player);
                    Destroy(obj, 0.3f);
                    cooldown[1] = 10;
                }
                else if(rnd == 3 && cooldown[2] == 0){ /// 3
                    obj = Instantiate(lilGhostPrefab, transform.position, Quaternion.identity);
                    obj.GetComponent<ProjectileItem>().throwItem(X,
                    1, true, 10, 1.1f, 1, true);
                    cooldown[2] = 6;
                    idleCounter = 1;
                }
                else if(rnd == 4 && cooldown[3] == 0){ /// 4
                
                }
                else randomMoveAdjacent(X, Y);
            }
            else randomMove(X, Y);
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
skill id                 skill effect                      skill cooldown       priority          additional       
    1     spawn 3 snakes                                         15                           idle for 3 turns
    2            reshuffle player's chips                          10                          
    3  spawn a ghost from left to right, deal damage to player     6                          idle for 1 turns
    4              push player to the right floor                  5               
*/