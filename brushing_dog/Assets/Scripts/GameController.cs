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
    Vector3 pastPosition = GameInfo.DEFAULT_POSITION;

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
        // クリック(タップ)した瞬間に初期化させる
        if(Input.GetMouseButtonDown(0)){
            currentPosition = camera.ViewportToWorldPoint(Input.mousePosition);
            pastPosition = currentPosition;
        }

        // クリック(タップ)している間の処理
        if(Input.GetMouseButton(0)){

            currentPosition = camera.ViewportToWorldPoint(Input.mousePosition);
            CheckTouchDog(currentPosition);

            // スワイプした距離が一定の距離以下の場合、無視する
            if(Vector3.Distance(currentPosition, pastPosition) < GameInfo.SWAP_DISTANCE){
                pastPosition = currentPosition;
                return;
            }

            // TODO：スワイプした時の処理を書く
            // スコアに反映、毛を発生させるとか？

            pastPosition = currentPosition;
        }

        // ボタン(指)を離した瞬間、処理を終了させる
        if(Input.GetMouseButtonUp(0)){
            pastPosition = GameInfo.DEFAULT_POSITION;
        }
    }

    // 今のマウスポインタが犬に触れているかを確認する
    void CheckTouchDog(Vector3 mouse_position){
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        float maxDistance = 10;
        // //レイヤーマスク作成(デフォルトは0)
        // int layerMask = 0;
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxDistance, Physics.AllLayers);
        if (hit.collider == null) return; 
        // Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
        GameObject targetObject = hit.collider.gameObject;
        // Debug.Log("-------------------------------");
        // Debug.Log(targetObject.tag);
        // Debug.Log("-------------------------------");
    }
}
