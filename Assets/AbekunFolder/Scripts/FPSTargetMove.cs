using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FPSTargetMove : MonoBehaviour
{
    [SerializeField] GameObject objPlayer;
    [SerializeField] float TargetDistance = 0.0f;
    [SerializeField] float TargetHeight = 0.0f;
    [SerializeField] Transform FPS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInputTest.GetChargeMode())
        {
            this.transform.LookAt(FPS);
        }
        this.transform.position =new Vector3(objPlayer.transform.position.x, objPlayer.transform.position.y+TargetHeight, objPlayer.transform.position.z);
        
    }
}
