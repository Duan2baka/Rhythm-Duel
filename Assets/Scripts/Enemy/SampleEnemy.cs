using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleEnemy : MonoBehaviour, Enemy{
    //private int action = -1;
    int idleCounter = 0;
    int X, Y, PlayerX, PlayerY;
    int[] cooldown;
    PlayerMovement playerMovement;
    EnemyMovement enemyMovement;
    void Start(){
        idleCounter = 0;
        cooldown = new int[5]{0, 0, 0, 0, 0};
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        enemyMovement = gameObject.GetComponent<EnemyMovement>();
    }
    public void resetState(){
        idleCounter = 0;
    }
    public void takeAction(){
        for(int i = 0; i <= 4; i ++)
            if(cooldown[i] != 0) cooldown[i] --;
        if(idleCounter != 0){
            idleCounter --;
            Debug.Log("enemyMovement: idle");
        }
        else{
            X = gameObject.GetComponent<EnemyMovement>().getX();
            Y = gameObject.GetComponent<EnemyMovement>().getY();
            PlayerX = playerMovement.getX();
            PlayerY = playerMovement.getY();
            int rnd = Random.Range(1, 10);
            if(rnd <= 6){
                if(Y == 1 && PlayerY == 3 && X == PlayerX){
                    Debug.Log("use skill 3");
                    return;
                }
                if(X == PlayerX && cooldown[4] == 0){
                    Debug.Log("use skill 4");
                    cooldown[4] = 5;
                    idleCounter = 1;
                    return;
                }
                if(cooldown[2] == 0){
                    Debug.Log("use skill 2");
                    cooldown[2] = 20;
                    idleCounter = 4;
                    return;
                }
                if(cooldown[1] == 0){
                    Debug.Log("use skill 1");
                    cooldown[1] = 5;
                    return;
                }
            }
            if(X != PlayerX){
                Debug.Log("moveX");
                enemyMovement.Move((X - PlayerX > 0) ? -1 : 1, 0);
                return;
            }
            if(Y > 1){
                Debug.Log("moveY");
                enemyMovement.Move(0, -1);
            }
        }
    }
    /*   skill id                 skill effect                      skill cooldown       priority          additional       
            1          randomly break a floor in player's side             5                           
            2            Deals damage to each column in turn              20                2           idle for 4 turns
            3           Deal massive damage to player in front             0                1                
            4     Deal damage on a row in next turn(idle for 1 turn)       5                2      cast when player is within range

        decision flow:
        start=>start: Start action
        cond1=>condition: player in front?
        skill3=>operation: use skill 3
        cond2=>condition: skill 4 can be used?
        skill4=>operation: use skill 4
        cond3=>condition: skill 2 can be used?
        skill2=>operation: use skill2
        cond4=>condition: skill 1 can be used?
        skill1=>operation: use skill1
        cond5=>condition: on same row?
        movement1=>operation: Move vertically
        movement2=>operation: Approach the player


        end=>end: End action

        start->cond1
        cond1(yes)->skill3
        cond1(no)->cond2
        cond2(yes)->skill4
        cond2(no)->cond3
        cond3(yes)->skill2
        cond3(no)->cond4
        cond4(yes)->skill1
        cond4(no)->cond5
        cond5(no)->movement1
        cond5(yes)->movement2

        skill4->end
        skill3->end
        skill2->end
        skill1->end
        movement1->end
        movement2->end
    */
}
