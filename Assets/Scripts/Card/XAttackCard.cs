using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XAttackCard : MonoBehaviour, Card{

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
        enemyMovement = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>();
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
    }
    private void tryCast(int x, int y){
        if(x > 0 && x <= 3 && y > 0 && y <= 3){
            Vector3 position = floorController.getPositon(x, y, false);
            // Debug.Log(effectPrefab.GetComponent<Renderer>().bounds.size.y);
            GameObject obj = Instantiate(effectPrefab, position + new Vector3(0f, effectPrefab.GetComponent<Renderer>().bounds.size.y / 2f, 0f), Quaternion.identity);
            obj.layer = 1;
            Vector3 size = obj.GetComponent<Renderer>().bounds.size;
            float scale = floorController.get(x, y, false).GetComponent<Renderer>().bounds.size.x / size.x;
            obj.transform.localScale = new Vector3(scale, scale, scale);

            tmp = floorController.FindObjectOn(x, y, false);
            if(tmp != null){
                tmp.GetComponent<HealthController>().takeDamage(dmg);
            }

        }
    }
    public void Cast(ref int currentMana){
        int x = playerMovement.getX(), y = playerMovement.getY();
        int ex = enemyMovement.getX(), ey = enemyMovement.getY();
        if(x == ex){
            tryCast(ex, ey);
            int fx = 1, fy = 1;
            for(int i = 1; i <= 2; i ++){
                fx *= -1;
                for(int j = 1; j <= 2; j++){
                    fy *= -1;
                    tryCast(ex + fx, ey + fy);
                }
            }
        }
        else{
            tryCast(x, y);
            int fx = 1, fy = 1;
            for(int i = 1; i <= 2; i ++){
                fx *= -1;
                for(int j = 1; j <= 2; j++){
                    fy *= -1;
                    tryCast(x + fx, y + fy);
                }
            }
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
}
