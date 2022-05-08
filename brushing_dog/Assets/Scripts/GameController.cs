using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NCMB;

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
    Text timeText;

    [SerializeField]
    GameObject startTime;

    [SerializeField]
    GameObject[] hair;

    [SerializeField]
    GameObject result_background;

    [SerializeField]
    GameObject ResultText;

    [SerializeField]
    GameObject[] hair_block;

    [SerializeField]
    GameObject tweetButton;

    [SerializeField]
    GameObject rankingButton;

    [SerializeField]
    GameObject replayButton;

    float GameTimes;
    float start_game_counter;
    int ResultFlg;

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentGameState(GameState.COUNTDOWN);
        InstantiateGame();
    }

    void InstantiateGame(){
        ScoreManager.instance.score = 0.0f;
        ResultText.SetActive(false);
        for (int i=0; i<3;i++){
            hair_block[i].SetActive(false);
        }
        GameTimes = GameInfo.GAMETIMES;
        ResultFlg = 0;
        start_game_counter = GameInfo.START_GAME_COUNTER;
        startTime.SetActive(true);
        tweetButton.SetActive(false);
        rankingButton.SetActive(false);
        replayButton.SetActive(false);
        tweetButton.GetComponent<Button>().onClick.AddListener (TweetButtonClick);
        rankingButton.GetComponent<Button>().onClick.AddListener (RankingButtonClick);
        replayButton.GetComponent<Button>().onClick.AddListener (ReplayButtonClick);
        Vector3 result_pos = result_background.transform.position;
        result_pos = new Vector3 (-19.18f, 0.0f, 0.0f);
        result_background.transform.position = result_pos;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameState == GameState.COUNTDOWN){
            AudioManager.Instance.PlayBGM("Main");
            StartGameCounter();
        }
        else if(currentGameState == GameState.MAIN){
            PlayerControll();
            GameTimeCounter();
            DisplayHairBlock();
            PlayDogSE();
        }
        else if (currentGameState == GameState.GAMEOVER){
            ResultControll();
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
            
            ScoreManager.instance.score += (Vector3.Distance(currentPosition, pastPosition)/10000.0f);

            // 一定のスコアごとに抜け毛を発生させる
            if((int)ScoreManager.instance.score % 10 == 0){
                AudioManager.Instance.PlaySE("brushing");
                MakeHair(currentPosition);
            }
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

        if(GameTimes < 0){
            SetCurrentGameState(GameState.GAMEOVER);
            AudioManager.Instance.StopBGM();
        } 
    }

    void SetCurrentGameState(GameState status){
        currentGameState = status;
    }

    float TimeCounter(float time){
        time -= Time.deltaTime;
        return time;
    }

    void MakeHair(Vector3 mouse_position){

        float random = Random.Range (0.0f, 1.0f);

        if(random < 0.4f){
            GameObject tmp = Instantiate(hair[0], camera.ScreenToWorldPoint(mouse_position), Quaternion.identity);
            tmp.transform.Rotate(new Vector3 (0.0f,0.0f,Random.Range (0.0f, 360.0f)));
        }
        else if(random < 0.8f){
            GameObject tmp = Instantiate(hair[1], camera.ScreenToWorldPoint(mouse_position), Quaternion.identity);
            tmp.transform.Rotate(new Vector3 (0.0f,0.0f,Random.Range (0.0f, 360.0f)));
        }
        else{
            GameObject tmp = Instantiate(hair[2], camera.ScreenToWorldPoint(mouse_position), Quaternion.identity);
            tmp.transform.Rotate(new Vector3 (0.0f,0.0f,Random.Range (0.0f, 360.0f)));
            AudioManager.Instance.PlaySE("spon2");
        }
    }

    void ResultControll(){
        if(ResultFlg == 0){
            StartCoroutine("ResultAnimation");
            ResultFlg = 1;
        }
    }

    private IEnumerator ResultAnimation() {
        AudioManager.Instance.PlaySE("StartGameOver");
        result_background.transform.DOMove (
            new Vector3(0.0f, 0.0f, 1.0f), //移動後の座標
            0.5f         //時間
        );
        yield return new WaitForSeconds (1.0f);
        AudioManager.Instance.PlayBGM("GameOver");
        Text TextContent = ResultText.GetComponent<Text>();
        TextContent.text = (int)ScoreManager.instance.score + "mg \nとれました";

        ResultText.SetActive(true);
        tweetButton.SetActive(true);
        rankingButton.SetActive(true);
        replayButton.SetActive(true);
    }

    void DisplayHairBlock(){

        if(ScoreManager.instance.score > 2000){
            hair_block[2].SetActive(true);
        }
        else if(ScoreManager.instance.score > 1500){
            hair_block[1].SetActive(true);
        } 
        else if(ScoreManager.instance.score > 1000){
            hair_block[0].SetActive(true);
        }

    }
    void PlayDogSE(){
        if(Random.Range (0.0f, 1.0f) < 0.002f){
            AudioManager.Instance.PlaySE("dog1b");
        }
    }

    void StartGameCounter(){

        if(1.0f > start_game_counter){
            SetCurrentGameState(GameState.MAIN);
            startTime.SetActive(false);        
        }
        else {
            //時間をカウントする
            start_game_counter = TimeCounter(start_game_counter);
            //時間を表示する
            Text startTimeText = startTime.GetComponent<Text>();
            startTimeText.text = ((int)start_game_counter).ToString();
            // AudioManager.Instance.PlaySE("Count");
        }
    }

    void TweetButtonClick(){
        naichilab.UnityRoomTweet.Tweet ("brushing_dog", " <換毛期> "+(int)ScoreManager.instance.score+"mgの抜け毛が取れたよ", "unityroom", "unity1week");
    }

    void RankingButtonClick(){
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking ((int)ScoreManager.instance.score);
    }

    void ReplayButtonClick(){
        SetCurrentGameState(GameState.COUNTDOWN);
        InstantiateGame();
    }
}
