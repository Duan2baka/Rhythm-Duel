using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Card{
    public void Cast(ref int currentMana);
    public int getCost();
    public Sprite getSprite();
    public void init();
    public string getChipName();
    public string getDescription();
}
