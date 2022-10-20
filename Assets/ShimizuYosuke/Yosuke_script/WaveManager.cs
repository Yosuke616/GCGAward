using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    //スコアを管理する変数
    private int nScore;
    //スコアを表示するためのテキスト変数
    private Text scoreText;

    //敵の数を管理する変数
    private int nEnemyNum;
    //このウェーブの敵の最大数を保存しておく
    private int nMaxEnemy;
    //Wave数の管理
    private int nWaveNum;
    //HeadShotの数
    private int nHeadShot;
    //倒した敵の数
    private int nBreakEnemyNum;
    //1wave目だった場合のみプレイヤーの初期位置を決める関数
    private bool bFirstPlayer;


    // Start is called before the first frame update
    void Start()
    {
        //スコアを0にする
        nScore = 0;
        //スコアのテキストを紐づける
        GameObject obj = transform.FindChild("Canvas").gameObject;
        scoreText = obj.transform.FindChild("Score").gameObject.GetComponent<Text>();

        //敵の数は初期数は3
        nMaxEnemy = nEnemyNum = 3;
        //ウェーブ数は1にする
        nWaveNum = 1;
        //ヘッドショットは0にする
        nHeadShot = 0;
        //倒した敵は0にする
        nBreakEnemyNum = 0;
        //最初の一回だった場合false
        bFirstPlayer = false;

    }

    // Update is called once per frame
    void Update()
    {
        //

        //スコアの表示
        scoreText.text = string.Format("{0}", nScore);

        //敵の数が0になったら初期化をする
        if (nEnemyNum <= 0) {
            MimicryStart();
        }


    }

    //プレイヤーが一回目だったかどうかを取得する
    public bool GetFirstPlayer() {
        return bFirstPlayer;
    }

    //プレイヤーが一回目だったかどうかを設定する
    public void SetFirstPlayer(bool firstplayer) {
        bFirstPlayer = firstplayer;
    }

    // スコアを加算させる
    public void AddScore(int Score)
    {
        nScore += Score;
    }

    // 最大敵の数を取得する関数
    public int GetEnemyNum() {
        return nMaxEnemy;
    }

    //最大敵を何体増やすか設定
    public void AddEnemyNum() {
        int add = Random.Range(1, 5);
        nMaxEnemy += add;
    }

    //次のウェーブ数にする
    public void NextWave() {
        nWaveNum++;
    }

    //ヘッドショット数をプラスする
    public void AddHeadShot() {
        nHeadShot++;
    }

    //倒した敵の数をプラスする
    public void AddBreakEnemy() {
        nBreakEnemyNum++;
    }

    //現時点の敵を倒した数をマイナスする
    public void DecEnemy() {
        nEnemyNum--;
    }

    //敵を全て倒したときに呼ぶ疑似初期化
    public void MimicryStart() {
        //ウェーブ数を追加
        NextWave();
        //敵の最大数を追加
        AddEnemyNum();

        //乱数を調整しなおす
        NumPower rNP = GameObject.Find("Spawn").GetComponent<NumPower>();
        rNP.SetList(nMaxEnemy);

        //敵を生成しなおす
        List<GameObject> RTGlist = new List<GameObject>();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy_Start_Pos");
        foreach (GameObject obj in gameObjects) {
            RTGlist.Add(obj);
        }

        foreach (GameObject obj in RTGlist) {
            obj.GetComponent<RayToGround>().ReCreateEnemy();
        }

    }
}
