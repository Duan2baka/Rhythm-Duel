using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{
    public int InitialX = 2;
    public int InitialY = 2;

    private int X;
    private int Y;
    private GameObject player;

    private RhythmController rhythmController;
    private FloorController floorController;
    private PositionController positionController;

    void Start(){
        X = InitialX;
        Y = InitialY;
        
        // Debug.Log(tmp.transform.position);
        transform.position = GameObject.Find("Floor 1" + InitialX + InitialY).transform.Find("Position").position + new Vector3(0, GetComponent<BoxCollider2D>().bounds.size.y / 2.0f, 0);

        rhythmController = GameObject.FindWithTag("GameController").GetComponent<RhythmController>();
        floorController = GameObject.FindWithTag("GameController").GetComponent<FloorController>();
        positionController = GameObject.FindWithTag("GameController").GetComponent<PositionController>();
        player = GameObject.FindWithTag("Player");
    }

    void Update(){/* cast card
        if(Input.GetMouseButtonDown(0)){

        }
        if(Input.GetMouseButtonDown(1)){
            
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            
        }
        if(Input.GetKeyDown(KeyCode.E)){
            
        }*/
        if(Input.GetKeyDown(KeyCode.A)){
            if(rhythmController.getInput()){
                if(floorController.isAccessable(X, Y - 1, true)){
                    // Debug.Log("A pressed");
                    Y = Y - 1;
                    positionController.set(player, floorController.get(X, Y, true));
                    rhythmController.operation(true);
                }
                else rhythmController.operation(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.W)){
            if(rhythmController.getInput()){
                if(floorController.isAccessable(X - 1, Y, true)){
                    X = X - 1;
                    positionController.set(player, floorController.get(X, Y, true));
                    rhythmController.operation(true);
                }
                else rhythmController.operation(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.D)){
            if(rhythmController.getInput()){
                if(floorController.isAccessable(X, Y + 1, true)){
                    Y = Y + 1;
                    positionController.set(player, floorController.get(X, Y, true));
                    rhythmController.operation(true);
                }
                else rhythmController.operation(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.S)){
            if(rhythmController.getInput()){
                if(floorController.isAccessable(X + 1, Y, true)){
                    X = X + 1;
                    positionController.set(player, floorController.get(X, Y, true));
                    rhythmController.operation(true);
                }
                else rhythmController.operation(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
            #else
                Application.Quit();
            #endif
        }
    }
}
