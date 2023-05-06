using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySliderController : MonoBehaviour{
    private GameObject player;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<Slider>().maxValue = GameObject.FindGameObjectWithTag("MainEnemy").transform.parent.gameObject.GetComponent<HealthController>().maxHP;
        gameObject.GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("MainEnemy").transform.parent.gameObject.GetComponent<HealthController>().maxHP;
    }
    void Update(){
        gameObject.GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("MainEnemy").transform.parent.gameObject.GetComponent<HealthController>().getHP();
    }
}
