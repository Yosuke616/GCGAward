using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    //スコアを管理する変数
    private int nScore;

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
    //プレイヤーオブジェクト
    [SerializeField] private GameObject Player;
    //敵オブジェクト
    [SerializeField] private GameObject WalkEnemy;
    [SerializeField] private GameObject NineEnemy;
    [SerializeField] private GameObject EighteenEnemy;

    //敵の種類の保存
    private int rnd = 0;

    // ステージ情報オブジェクト
    [Header("スコア→敵の数→ウェーブでUIを格納")]
    [SerializeField] private TextMeshProUGUI[] StageUIText;

    private bool bChangeBGM = false;

    private void Awake()
    {
        //60fps
        Application.targetFrameRate = 60;
    }


    // Start is called before the first frame update
    void Start()
    {
        //スコアを0にする
        nScore = 0;

        //敵の数は初期数は3
        nMaxEnemy = nEnemyNum = 0;
        //ウェーブ数は1にする
        nWaveNum = 1;
        //ヘッドショットは0にする
        nHeadShot = 0;
        //倒した敵は0にする
        nBreakEnemyNum = 0;

    }

    // Update is called once per frame
    void Update()
    {

        //スコアの表示
        StageUIText[0].text = string.Format("{0}", nScore);
        //敵数の表示
        StageUIText[1].text = string.Format("{0}",nEnemyNum);
        //ウェーブ数の表示
        StageUIText[2].text = string.Format("{0}",nWaveNum);
        //最大的数の表示
        //MaxEnemy.text = string.Format("{0}",nMaxEnemy);

        //ボタンでエネミーをぶち殺す
        //if (Input.GetKeyUp(KeyCode.F12)) {
        //    BreakTheEnemy();
        //}

        //敵の数が0になったら初期化をする
        if (nEnemyNum <= 0) {
            MimicryStart();
        }

        //実際の敵の数と数値が違っていたら消す
        GameObject[] tags = GameObject.FindGameObjectsWithTag("Enemy");

        //Debug.Log(tags.Length);
        //Debug.Log(nEnemyNum);

        if (tags.Length > nEnemyNum) {
            GameObject obj = GameObject.FindGameObjectWithTag("Enemy");
            Destroy(obj);
        }
    }

    //ヘッドショットの数を取得する
    public int GetHeadShot() {
        return nHeadShot;
    }

    //倒した敵の数を取得する
    public int GetBreakEnemy() {
        return nBreakEnemyNum;
    }

    public int GetScore() {
        return nScore;
    }

    //ウェーブ数を取得する
    public int GetWave() {
        return nWaveNum;
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
        int add = Random.Range(0, 2);
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

    //一回目だけマックスの数も設定する
    public void FirstEnemyNum(int cnt) {
       nEnemyNum += cnt;
        nMaxEnemy = nEnemyNum;
    }

    //実際に生み出されていた敵の数を格納する為の関数
    public void SetEnemyNum(int cnt) {
        nEnemyNum += cnt;
    }

    //敵の数を0にする関数
    public void SetEnemyNum0() {
        nEnemyNum = 0;
    }

    //プレイヤーのオブジェクトを送る
    public GameObject GetPlayerObj()
    {
        return Player;
    }

    //歩く敵オブジェクトを送る
    public GameObject GetWalkEnemyObj() {
        return WalkEnemy;
    }

    //90敵オブジェクトを送る
    public GameObject GetNineEnemyObj() {
        return NineEnemy;
    }

    //180敵オブジェクトを送る
    public GameObject GetEighteenEnemyObj() {
        return EighteenEnemy;
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
        AutoEnemy AE = GameObject.Find("Spawn").GetComponent<AutoEnemy>();
        
        foreach (GameObject obj in AE.GetList()) {
            obj.GetComponent<RayToGround>().ReCreateEnemy();
        }

    }

    //敵を全てぶち殺す関数
    public void BreakTheEnemy() {
        List<GameObject> enemylist = new List<GameObject>();

        GameObject[] tags = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in tags) {
            enemylist.Add(obj);
        }

        foreach (GameObject obj in enemylist) {
            Destroy(obj);
        }
        enemylist.Clear();

        tags = GameObject.FindGameObjectsWithTag("Enemy_Start_Pos");
        foreach (GameObject obj in tags) {
            enemylist.Add(obj);
        }

        foreach (GameObject obj in enemylist) {
            Destroy(obj);
        }
        enemylist.Clear();

        for (int i = 0;i < nEnemyNum;i++) {
            AddBreakEnemy();
        }

        nEnemyNum = 0;

    }

    public int GetRnd() {
        return rnd;
    }

    public void SetRnd() {
        rnd++;
        if (rnd > 2) {
            rnd = 0;
        }
    }

    public void SetChange(bool change)
    {
        if (bChangeBGM == change) {
            return;
        }

        bChangeBGM = change;
    }

}
