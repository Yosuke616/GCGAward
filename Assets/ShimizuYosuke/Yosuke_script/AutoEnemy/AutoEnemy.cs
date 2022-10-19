using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoEnemy : MonoBehaviour
{
    //タグを検索して"Ground"のタグを付けている奴を入れるリストを作成する
    public List<GameObject> GroundList = new List<GameObject>();

    //生成したいオブジェクト
    [SerializeField]private GameObject CreateObj;

    [Header("どのくらいの高さに生成するかを決める")]
    [SerializeField] private float Create_Height = 100;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] tags = GameObject.FindGameObjectsWithTag("Ground");
        //foreachで"Ground"のタグの物にリストを作る
        foreach (GameObject obj in tags) {
            GroundList.Add(obj);
        }

        //番号振り分けの為に変数を作る
        int cnt = 0;

        //リストに入ったオブジェクトの上空にオブジェクトを作る
        foreach (GameObject obj in GroundList) {
            GameObject ray = Instantiate(CreateObj,new Vector3(obj.transform.position.x, Create_Height, obj.transform.position.z), Quaternion.identity);
            //レイを飛ばすためのスクリプトをアタッチする
            ray.AddComponent<RayToGround>();
            ray.AddComponent<RayView>();
            ray.GetComponent<RayToGround>().SetNumber(cnt);
            cnt++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RemoveListbyID(GameObject id,List<GameObject> list) {
        GroundList.Remove(id);
    }
}
