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

    [SerializeField]
    private Camera camera;
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

        Vector3 currentPosition;
        if(Input.GetMouseButtonDown(0)){
            currentPosition = camera.ViewportToWorldPoint(Input.mousePosition);
            Debug.Log("x: " + currentPosition.x);
            Debug.Log("y: " + currentPosition.y);
        }
    }

}
