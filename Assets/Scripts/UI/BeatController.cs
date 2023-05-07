using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatController : MonoBehaviour{
    private MusicController musicController;
    private RhythmController rhythmController;
    bool on;
    float counter;
    Animator animator;
    void Start(){
        musicController = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicController>();
        rhythmController = GameObject.FindGameObjectWithTag("GameController").GetComponent<RhythmController>();
        on = false;
        counter = 0f;
        animator = GetComponent<Animator>();
    }
    void Update(){
        counter += Time.deltaTime;
        if(!on){
            if(counter >= musicController.firstRhythmSpawn + rhythmController.movePeriod - 0.4f){
                on = true;
                counter = 0;
            }
            return;
        }
        if(counter >= musicController.timeGap){
            counter = 0f;
            animator.SetTrigger("beat");
        }
    }
}
