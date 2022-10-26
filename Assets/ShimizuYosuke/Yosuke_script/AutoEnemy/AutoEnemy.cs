using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEnemy : MonoBehaviour
{

    //生成したいオブジェクト
    [SerializeField]private GameObject CreateObj;

    private List<GameObject> spawnList = new List<GameObject>();

    [Header("どのくらいの高さに生成するかを決める")]
    [SerializeField] private float Create_Height = 100;

    //何個作り出すかの変数
    private int cnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        //タグを検索して"Ground"のタグを付けている奴を入れるリストを作成する
        GameObject[] tags = GameObject.FindGameObjectsWithTag("Ground_Spawn");

        //番号振り分けスクリプトをアタッチする
        this.gameObject.AddComponent<NumPower>();

        //敵の番号振り分け用変数
        int nEnemy = 0;

        //リストに入ったオブジェクトの上空にオブジェクトを作る
        foreach (GameObject obj in tags) {
            GameObject ray = Instantiate(CreateObj,new Vector3(obj.transform.position.x, Create_Height, obj.transform.position.z), Quaternion.identity);
            //レイを飛ばすためのスクリプトをアタッチする
            ray.AddComponent<RayToGround>();
            ray.AddComponent<RayView>();
            ray.GetComponent<RayToGround>().SetNumber(nEnemy);
            ray.tag = "Spawn_Enemy";
            spawnList.Add(ray);
            nEnemy++;
            cnt++;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetNum() {
        return cnt;
    }

    public List<GameObject> GetList() {
        return spawnList;
    }
}
