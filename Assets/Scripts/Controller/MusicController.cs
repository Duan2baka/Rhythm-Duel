using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour{
    public float offset;
    public float firstRhythmSpawn, timeGap;
    public AudioSource audioSource;
    private PauseController pauseController;
    bool flag = false;
    private void Start() {
        StartCoroutine(playerMusicAfter(offset));
        flag = false;
        pauseController = GameObject.FindGameObjectWithTag("GameController").GetComponent<PauseController>();
    }

    private void Update() {
        if(flag){
            if(!audioSource.isPlaying && !pauseController.getStatus())
                SceneManager.LoadScene("GameOver");
        }
    }
    IEnumerator playerMusicAfter(float time){
        while(true){
            float t = 0;
            while (t < time){
                t += Time.deltaTime;
                yield return null;
            }
            audioSource = gameObject.GetComponent<AudioSource>();
            if(audioSource){
                audioSource.Play();
                flag = true;
            }
            break;
        }
    }
}
