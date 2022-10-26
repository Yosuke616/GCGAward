using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayToGround : MonoBehaviour
{
    //自分が何番目の射出地点かを認識しておくための番号
    private int Number;

    [Header("レイの距離")]
    [SerializeField] private int Distance = 100;

    private GameObject CreateEnemy;

    private List<int> numlist = new List<int>();

    private NumPower NP;

    private WaveManager WM;

    private RaycastHit hit;

    private int rnd = 0;

    // Start is called before the first frame update
    void Start()
    {
        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        NP = GameObject.Find("Spawn").GetComponent<NumPower>();

        //CreateEnemy = WM.GetEnemyObj();

        //敵の生成

        ///プレイヤーの生成
        //最初の一回だけプライヤーの場所はランダムにする
        if (WM.GetWave() == 1) {

            GameObject player = WM.GetPlayerObj();
            player = GameObject.FindGameObjectWithTag("Player");

            if (Number == NP.GetPlayerPos()) {
                //自身の真下にレイを飛ばす
                this.transform.eulerAngles = new Vector3(90.0f, 0, 0);
                Vector3 direction = new Vector3(0, -1, 0);
                Ray ray = new Ray(this.transform.position, direction);
                //int layerMask = ~(1 << 13);
                if (Physics.Raycast(ray, out hit, Distance))
                {
                    if (hit.collider.CompareTag("Enemy")||hit.collider.CompareTag("Head")) {

                    }
                    else{
                        player.transform.position = hit.transform.position;
                        player.transform.position += new Vector3(0,3,0);
                        //Instantiate(player,new Vector3(hit.point.x, hit.point.y+0.5f, hit.point.z), Quaternion.identity);
                    }

                }
            }

        }
        ReCreateEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //自分が何番目かを設定する関数
    public void SetNumber(int number) {
        Number = number;
    }

    //敵を生成しなおす関数
    public void ReCreateEnemy() {
        //マネージャーに敵の数を送る
        WaveManager WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        //敵のタグを敵にする

        //リストの数字を入れる
        numlist = NP.GetNumList();

        //生み出された敵の数をカウントする変数
        int cnt = 0;


        foreach (int i in numlist)
        {
            if (Number == i)
            {
                //自身の真下にレイを飛ばす
                this.transform.eulerAngles = new Vector3(90.0f, 0, 0);
                Vector3 direction = new Vector3(0, -1, 0);
                Ray ray = new Ray(this.transform.position, direction);
                //int layerMask = ~(1 << 13);
                if (Physics.Raycast(ray, out hit, Distance))
                {
                    //既に敵オブジェクトが生み出されていたら生み出さない
                    if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Player")|| hit.collider.CompareTag("Head") || hit.collider.CompareTag("Slope"))
                    {

                    }
                    else
                    {
                        //乱数を使って作る敵の種類を決める
                        //int rnd = Random.Range(0,2);
                        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();


                        switch (WM.GetRnd()) {
                            case 0: CreateEnemy = WM.GetNineEnemyObj(); break;
                            case 1: CreateEnemy = WM.GetEighteenEnemyObj(); break;
                            case 2: CreateEnemy = WM.GetWalkEnemyObj(); break;
                        }

                        WM.SetRnd();

                        //オブジェクトを作り出そう
                        GameObject Enemy = Instantiate(CreateEnemy,new Vector3(hit.point.x, hit.point.y+0.5f, hit.point.z), Quaternion.identity);
                        cnt = 1;
                    }
                }
            }
        }

        if (WM.GetWave() == 1)
        {
            WM.FirstEnemyNum(cnt);
        }
        else {
            WM.SetEnemyNum(cnt);
        }
    }

    //自分の番号を送る
    public int GetMyNumber() {
        return Number;
    }

}
