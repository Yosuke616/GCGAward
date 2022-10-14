using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountText : MonoBehaviour
{
    [SerializeField] private GameObject block;

    //スコアを管理する変数
    private int nScore;
    [SerializeField] Text scoreText;

    //プレイヤーの情報を取得
    [SerializeField] GameObject player;
    private float old_player;
    private float Max_Z_Pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        nScore = 0;
        old_player = player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //スコアの表示
        scoreText.text = "SCORE:" + string.Format("{0}",nScore);

        //1ブロック分進んだ時だけ加算する


        //Z方向にプレイヤーが進んだときだけスコアが加算させる
        if (old_player < player.transform.position.z) {
            //最大値より大きくなったら加算する
            if (old_player > Max_Z_Pos) {
                AddScore(10);
                Max_Z_Pos = old_player;
            }
        }

        //現在の座標を保存しておく
        old_player = player.transform.position.z;
    }

    // スコアを加算させる
    public void AddScore(int Score) {
        nScore += Score;
    }
}
