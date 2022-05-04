using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    float GameTimes;

    [SerializeField]
    Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.MAIN;
        ScoreManager.instance.score = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState == GameState.MAIN){
            PlayerControll();
            GameTimeCounter();
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

            currentPosition = Input.mousePosition;

            // マウスポインタが犬に触れていないときは無視する
            if(!(CheckTouchDog(currentPosition))){
                pastPosition = camera.ViewportToWorldPoint(currentPosition);
                return;
            } 
            // スワイプした距離が一定の距離以下の場合、無視する
            if(Vector3.Distance(currentPosition, pastPosition) < GameInfo.SWAP_DISTANCE){
                pastPosition = camera.ViewportToWorldPoint(currentPosition);
                return;
            }

            // TODO：スワイプした時の処理を書く
            // 毛を発生させるとか？
            ScoreManager.instance.score += (Vector3.Distance(currentPosition, pastPosition)/100.0f);

            // // 一定のスコアごとに抜け毛を発生させる
            // if(ScoreManager.instance.score % 10 == 0){

            // }
            Debug.Log("score: "+ ScoreManager.instance.score);

            pastPosition = camera.ViewportToWorldPoint(currentPosition);
        }

        // ボタン(指)を離した瞬間、処理を終了させる
        if(Input.GetMouseButtonUp(0)){
            pastPosition = GameInfo.DEFAULT_POSITION;
        }
    }

    // 今のマウスポインタが犬に触れているかを確認する
    bool CheckTouchDog(Vector3 mouse_position){
        Ray ray = camera.ScreenPointToRay(mouse_position);
        float maxDistance = 10;
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, maxDistance, Physics.AllLayers);

        // 直線でぶつかっているものがなかったら犬に触れていない
        if (hit.collider == null) return false; 
        GameObject targetObject = hit.collider.gameObject;

        // タグが犬かどうかを判別する
        if (targetObject.tag == "dog") return true;
        else return false;
    }

    void GameTimeCounter(){

        //時間をカウントする
        GameTimes = TimeCounter(GameTimes);

        //時間を表示する
        timeText.text = ((int)GameTimes).ToString();

        //3秒前の音
        if( 0 < GameTimes && GameTimes < 4){
            if ((int)GameTimes <= 3 && 3 < (int)GameTimes+1){
                // AudioManager.Instance.PlaySE("Count");
            }
        }

        if(GameTimes < 0) SetCurrentGameState(GameState.GAMEOVER);
    }

    void SetCurrentGameState(GameState status){
        currentGameState = status;
    }

    float TimeCounter(float time){

        time -= Time.deltaTime;
        return time;
    }

    void MakeHair(){

    }
}
