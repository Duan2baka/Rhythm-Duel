using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursedArmor : MonoBehaviour, Enemy{
    int idleCounter = 0;
    int X, Y, PlayerX, PlayerY;
    public GameObject handPrefab;
    public GameObject throwablePrefab, shovelPrefab;
    private GameObject player;
    PlayerMovement playerMovement;
    EnemyMovement enemyMovement;
    FloorController floorController;
    PositionController positionController;
    GameObject obj, tmp;
    int[] cooldown, randomlist;
    private int rnd, cnt;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        positionController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PositionController>();
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
            X = gameObject.GetComponent<EnemyMovement>().getX();
            Y = gameObject.GetComponent<EnemyMovement>().getY();
            PlayerX = playerMovement.getX();
            PlayerY = playerMovement.getY();
            rnd = Random.Range(1, 10);
            if(rnd <= 5){
                rnd = Random.Range(1, 4);
                if(rnd == 1 && cooldown[0] == 0){
                    cnt = 0;
                    for(int i = 1; i <= 3; i ++)
                        for(int j = 1; j <= 3; j ++)
                            if(floorController.isAccessable(i, j, true)) cnt ++;
                    if(cnt < 3) randomMove(X, Y);
                    else{
                        randomlist = new int[cnt];
                        for(int i = 0; i < cnt; i ++)
                            randomlist[i] = i;
                        randomlist.Shuffle();
                        cnt = 0;
                        for(int i = 1; i <= 3; i ++)
                            for(int j = 1; j <= 3; j ++){
                                if(!floorController.isAccessable(i, j, true)) continue;
                                for(int k = 0; k < 3; k ++)
                                    if(randomlist[k] == cnt){
                                        floorController.get(i, j, true).GetComponent<FloorStatus>().takeDamage(100);
                                        Vector3 position = floorController.getPositon(i, j, true);
                                        GameObject obj = Instantiate(shovelPrefab, position +
                                        new Vector3(0f, shovelPrefab.GetComponent<Renderer>().bounds.size.y / 2f, 0f), Quaternion.identity);
                                        Vector3 size = obj.GetComponent<Renderer>().bounds.size;
                                        float scale = floorController.get(i, j, true).GetComponent<Renderer>().bounds.size.x / size.x;
                                        obj.transform.localScale = new Vector3(scale, scale, scale);
                                        Destroy(obj, 0.3f);
                                    }
                                cnt ++;
                            }
                        cooldown[0] = 15;
                    }
                }
                else if(rnd == 2 && cooldown[1] == 0){

                    cooldown[1] = 5;
                }
                else if(rnd == 3 && cooldown[2] == 0){
                    cnt = 0;
                    for(int i = 1; i <= 3; i ++)
                        for(int j = 1; j <= 3; j ++)
                            if(floorController.isAccessable(i, j, true)) cnt ++;
                    if(cnt < 3) randomMove(X, Y);
                    else{
                        randomlist = new int[cnt];
                        for(int i = 0; i < cnt; i ++)
                            randomlist[i] = i;
                        randomlist.Shuffle();
                        cnt = 0;
                        for(int i = 1; i <= 3; i ++)
                            for(int j = 1; j <= 3; j ++){
                                if(!floorController.isAccessable(i, j, true)) continue;
                                for(int k = 0; k < 3; k ++)
                                    if(randomlist[k] == cnt){
                                        obj = Instantiate(throwablePrefab, transform.position, Quaternion.identity);
                                        tmp = floorController.get(i, j, true);
                                        obj.GetComponent<ThrowItem>().throwItem(transform.position,
                                        tmp.transform.Find("Position").position, tmp, 10, 1.1f);
                                        break;
                                    }
                                cnt ++;
                            }
                            
                        cooldown[2] = 10;
                    }
                }
                else if(rnd == 4 && cooldown[3] == 0){
                    obj = Instantiate(handPrefab, player.transform.position - new Vector3(
                    player.GetComponent<BoxCollider2D>().size.x / 2f,
                    - player.GetComponent<BoxCollider2D>().size.y / 2f, 0f), Quaternion.identity);
                    obj.GetComponent<HandController>().init(player);
                    Destroy(obj, 0.3f);
                    //Debug.Log(PlayerY);
                    if(floorController.isAccessable(PlayerX, PlayerY + 1, true)){
                        PlayerY = PlayerY + 1;
                        positionController.set(player, floorController.get(PlayerX, PlayerY, true));
                        playerMovement.setX(PlayerX);
                        playerMovement.setY(PlayerY);
                    }
                    cooldown[3] = 5;
                }
                else randomMove(X, Y);
            }
            else randomMove(X, Y);
        }
    }
    private void randomMove(int x, int y){
        Debug.Log("move");
        cnt = 0;
        for(int i = 1; i <= 3; i ++)
            for(int j = 1; j <= 3; j ++)
                if(floorController.isAccessable(i, j, false)) cnt ++;
        cnt --;
        int rnd = Random.Range(1, cnt);
        cnt = 0;
        for(int i = 1; i <= 3; i ++)
            for(int j = 1; j <= 3; j ++){
                if(!floorController.isAccessable(i, j, false)) continue;
                if(i == x && j == y) continue;
                cnt ++;
                if(cnt == rnd) enemyMovement.MoveTo(i, j);
            }

    }
}
/*
skill id                 skill effect                      skill cooldown       priority          additional       
    1     randomly break three floors in player's side             15                           idle for 3 turns
    2            Emit two energy waves for two rows                5                          
    3       throw three projectile for three random place          10                          idle for 2 turns
    4              push player to the right floor                  5               
*/