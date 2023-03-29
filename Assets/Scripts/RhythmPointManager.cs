using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmPointManager : MonoBehaviour
{
    private bool disabled;
    public void disable(){
        disabled = true;
    }
    public bool getStatus(){
        return disabled;
    }
    public void init(){
        disabled = false;
    }

}
