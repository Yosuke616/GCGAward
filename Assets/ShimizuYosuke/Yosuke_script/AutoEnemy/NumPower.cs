using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumPower : MonoBehaviour
{
    private AutoEnemy AE;
    private WaveManager WM;

    //敵のランダム生成用のリスト
    private  List<int> RndNumList = new List<int>();

    //プレイヤーの場所を保存する変数
    private int Player_Pos;

    private void Awake()
    {
        AE = GameObject.Find("Spawn").GetComponent<AutoEnemy>();
        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        
        SetList(WM.GetEnemyNum());

        SetPlayerPos();

        WM.SetEnemyNum0();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //引数分のランダムに選ばれた変数をリストに追加する関数
    public void SetList(int nNum) {
        //リストをクリアする
        RndNumList.Clear();

        for (int i = 0;i < nNum;i++) {
            int rnd = Random.Range(0,AE.GetNum());
            RndNumList.Add(rnd);
        }

    }

    private void SetPlayerPos() {
        Player_Pos = Random.Range(0, AE.GetNum());
    }

    public List<int> GetNumList() {
        return RndNumList;
    }

    public int GetPlayerPos() {
        return Player_Pos;
    }

}
