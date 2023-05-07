using UnityEngine;

public class PauseAfterAnimation : MonoBehaviour{
    private Animator anim;
    void Start(){
        anim = GetComponent<Animator>();
        AnimationEvent evt = new AnimationEvent();
        evt.time = anim.GetCurrentAnimatorStateInfo(0).length;
        evt.functionName = "PauseAnimation";
        anim.runtimeAnimatorController.animationClips[0].AddEvent(evt);
    }
    void PauseAnimation(){
        gameObject.GetComponent<Animator>().enabled = false;
    }
}