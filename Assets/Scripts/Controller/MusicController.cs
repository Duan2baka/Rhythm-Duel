using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour{
    public float offset;
    public float firstRhythmSpawn, timeGap;
    private void Start() {
        StartCoroutine(playerMusicAfter(offset));
    }
    IEnumerator playerMusicAfter(float time){
        while(true){
            float t = 0;
            while (t < time){
                t += Time.deltaTime;
                yield return null;
            }
            if(gameObject.GetComponent<AudioSource>()) gameObject.GetComponent<AudioSource>().Play();
            break;
        }
    }
}
