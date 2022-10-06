using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayView : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] int RayLength = 30;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position;      // 始点をオブジェクトの中心座標に設定
        Vector3 direction = transform.forward;       // X軸方向を表すベクトル
        Ray ray = new Ray(origin, direction);           // Rayを生成
        Debug.DrawRay(ray.origin, ray.direction * RayLength, Color.red);    
    }
}
