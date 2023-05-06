using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSliderController : MonoBehaviour{
    private GameObject player;
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<Slider>().maxValue = player.GetComponent<HealthController>().maxHP;
        gameObject.GetComponent<Slider>().value = player.GetComponent<HealthController>().maxHP;
    }
    void Update(){
        gameObject.GetComponent<Slider>().value = player.GetComponent<HealthController>().getHP();
    }
}
