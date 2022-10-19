using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayToGround : MonoBehaviour
{
    //自分が何番目の射出地点かを認識しておくための番号
    private int Number;

    [Header("レイの距離")]
    [SerializeField] private int Distance = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        //自身の真下にレイを飛ばす
        this.transform.eulerAngles = new Vector3(90.0f,0,0);
        Vector3 direction = new Vector3(0,0,0);
        Ray ray = new Ray(this.transform.position,direction);
        //int layerMask = ~(1 << 13);
        if (Physics.Raycast(ray, out hit,Distance,0))
        {
            
        }
    }

    //自分が何番目かを設定する関数
    public void SetNumber(int number) {
        Number = number;
    }

}
