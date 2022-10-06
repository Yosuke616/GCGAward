using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputTest : MonoBehaviour
{
    
    [Header("プレイヤーステータス")]
    [SerializeField] private float PlayerMove = 1.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(Input.GetKey(KeyCode.W))
        {
            pos.z += PlayerMove*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += PlayerMove * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.z -= PlayerMove * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x -= PlayerMove * Time.deltaTime;
        }
        transform.position = pos;

    }
}
