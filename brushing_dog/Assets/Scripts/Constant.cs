using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInfo{

    // どのくらいの距離をスワイプと判定するかの距離
    public static readonly float SWAP_DISTANCE = 1.0f;

    // positionを保持するときのデフォルトのポジションの距離
    public static readonly Vector3 DEFAULT_POSITION = new Vector3(1000,1000,1000);

    // ゲームのプレイ時間
    public static readonly float GAMETIMES = 20.0f;

    // ゲーム開始のカウント時間
    public static readonly float START_GAME_COUNTER = 4.0f;
}

