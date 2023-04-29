using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour{
    private Animator anim;
    void Start(){
        anim = GetComponent<Animator>();
        AnimationEvent evt = new AnimationEvent();
        evt.time = anim.GetCurrentAnimatorStateInfo(0).length;
        evt.functionName = "DestroyGameObject";
        anim.runtimeAnimatorController.animationClips[0].AddEvent(evt);
    }
    void DestroyGameObject(){
        Destroy(gameObject);
    }
}