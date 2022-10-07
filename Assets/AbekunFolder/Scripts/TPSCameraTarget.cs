using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCameraTarget : MonoBehaviour
{
    [Header("カメラターゲットオブジェクト")]
    [SerializeField] GameObject TPSCameraTargetX;
    [SerializeField] GameObject TPSCameraTargetY;

    [SerializeField] GameObject Player;

    //private float CameraRangeY = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //this.transform.position = new Vector3(TPSCameraTargetX.transform.position.x, TPSCameraTargetY.transform.position.y,);
       
    }
}
