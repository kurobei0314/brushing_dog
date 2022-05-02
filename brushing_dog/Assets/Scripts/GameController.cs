using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

     //ゲームの状態を管理する
    public enum GameState{
        COUNTDOWN,
        MAIN,
        GAMEOVER
    }
    GameState currentGameState;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.MAIN;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState == GameState.MAIN){
            PlayerControll();
        }        
    }

    void PlayerControll(){

        if(Input.GetMouseButtonDown(0)){
            Debug.Log("wa----------i");
        }
    }
}
