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
    // Start is called before the first frame update
    void Start()
    {
        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        NP = GameObject.Find("Spawn").GetComponent<NumPower>();

        ///プレイヤーの生成
        //最初の一回だけプライヤーの場所はランダムにする
        if (!WM.GetFirstPlayer()) {

            GameObject player = GameObject.Find("Player");

            if (Number == NP.GetPlayerPos()) {
                //自身の真下にレイを飛ばす
                this.transform.eulerAngles = new Vector3(90.0f, 0, 0);
                Vector3 direction = new Vector3(0, -1, 0);
                Ray ray = new Ray(this.transform.position, direction);
                //int layerMask = ~(1 << 13);
                if (Physics.Raycast(ray, out hit, Distance))
                {
                    Instantiate(player, hit.point, Quaternion.identity);
                    WM.SetFirstPlayer(true);
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
        //敵の生成
        CreateEnemy = GameObject.Find("Enemy");

        //リストの数字を入れる
        numlist = NP.GetNumList();

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
                    if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Player"))
                    {

                    }
                    else
                    {
                        //オブジェクトを作り出そう
                        GameObject Enemy = Instantiate(CreateEnemy, hit.point, Quaternion.identity);
                    }
                }
            }
        }
    }
}
