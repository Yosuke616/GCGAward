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

        

        SetList(30);

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

        int u = 0;

        int rnd = Random.Range(0,AE.GetNum());
        for (int i = 0;i < nNum;i++) {
            u += rnd;
            if (u > AE.GetNum()) {
                u = 3 + i;
            }
            RndNumList.Add(u);
        Debug.Log(nNum);
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
