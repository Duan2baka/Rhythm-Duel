using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, Enemy{
    int idleCounter = 0;
    int X, Y, PlayerX, PlayerY;
    public GameObject recyclePrefab;
    public GameObject magicCirclePrefab, snakePrefab, lilGhostPrefab;
    private GameObject player;
    private bool is_invisible;
    PlayerMovement playerMovement;
    EnemyMovement enemyMovement;
    FloorController floorController;
    PositionController positionController;
    CardPanelController cardPanelController;
    GameObject obj, tmp;
    int[] cooldown, randomlist;
    int[] fx, fy;
    private int rnd, cnt, startHP = -1;
    void Start(){
        startHP = -1;
        is_invisible = false;
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
    private void Update() {
        if(is_invisible && startHP != gameObject.GetComponent<HealthController>().getHP()){
            is_invisible = false;
            startHP = -1;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
    public void takeAction(){
        for(int i = 0; i <= 4; i ++)
            if(cooldown[i] != 0) cooldown[i] --;
        if(idleCounter != 0){
            idleCounter --;
        }
        else{
            if(is_invisible){
                gameObject.GetComponent<HealthController>().heal(3);
                startHP = gameObject.GetComponent<HealthController>().getHP();
            }

            X = gameObject.GetComponent<EnemyMovement>().getX();
            Y = gameObject.GetComponent<EnemyMovement>().getY();
            PlayerX = playerMovement.getX();
            PlayerY = playerMovement.getY();

            rnd = Random.Range(1, 10 + 1);
            if(rnd <= 5){
                rnd = Random.Range(1, 4 + 1);
                // Debug.Log(rnd);  
                if(rnd == 1 && cooldown[0] == 0){ /// 1
                    rnd = Random.Range(1, 2 + 1);
                    obj = Instantiate(magicCirclePrefab, transform.position - new Vector3(
                    0f, - gameObject.GetComponent<BoxCollider2D>().size.y / 2f, 0f)
                    , Quaternion.identity);
                    obj.GetComponent<MagicCircleController>().init(gameObject);
                    
                    if(rnd == 1){
                        obj = Instantiate(snakePrefab, transform.position, Quaternion.identity);
                        obj.GetComponent<ProjectileItem>().throwItem(1,
                        3, false, 10, 1.1f, -1, "Player");
                        obj = Instantiate(snakePrefab, transform.position, Quaternion.identity);
                        obj.GetComponent<ProjectileItem>().throwItem(3,
                        3, false, 10, 1.1f, -1, "Player");
                        obj = Instantiate(snakePrefab, transform.position, Quaternion.identity);
                        obj.GetComponent<ProjectileItem>().throwItem(2,
                        1, false, 10, 1.1f, -1, "Player");
                    }
                    else{
                        obj = Instantiate(snakePrefab, transform.position, Quaternion.identity);
                        obj.GetComponent<ProjectileItem>().throwItem(1,
                        1, false, 10, 1.1f, -1, "Player");
                        obj = Instantiate(snakePrefab, transform.position, Quaternion.identity);
                        obj.GetComponent<ProjectileItem>().throwItem(3,
                        1, false, 10, 1.1f, -1, "Player");
                        obj = Instantiate(snakePrefab, transform.position, Quaternion.identity);
                        obj.GetComponent<ProjectileItem>().throwItem(2,
                        3, false, 10, 1.1f, -1, "Player");
                    }
                    cooldown[0] = 10;
                    idleCounter = 5;
                }
                else if(rnd == 2 && cooldown[1] == 0){ /// 2
                    cardPanelController.shuffleHand();
                    obj = Instantiate(recyclePrefab, player.transform.position - new Vector3(
                    0f, - player.GetComponent<BoxCollider2D>().size.y, 0f), Quaternion.identity);
                    obj.GetComponent<RecycleController>().init(player);
                    Destroy(obj, 0.5f);
                    idleCounter = 2;
                    cooldown[1] = 10;
                }
                else if(rnd == 3 && cooldown[2] == 0){ /// 3
                    obj = Instantiate(lilGhostPrefab, transform.position, Quaternion.identity);
                    obj.GetComponent<ProjectileItem>().throwItem(X,
                    1, true, 10, 1.1f, 1, "Player");
                    cooldown[2] = 6;
                    idleCounter = 3;
                }
                else if(rnd == 4 && cooldown[3] == 0){ /// 4
                    is_invisible = true;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.1f);
                    startHP = gameObject.GetComponent<HealthController>().getHP();
                    cooldown[3] = 20;
                    idleCounter = 3;
                }
                else randomMoveAdjacent(X, Y);
            }
            else{
                randomMove(X, Y);
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
    1     spawn 3 snakes                                             15                5 
    2            reshuffle player's chips                            10                10          
    3  spawn a ghost from left to right, deal damage to player       6                 3
    4 get invisible and heal when invisible, ends when take damage   5                 1
*/