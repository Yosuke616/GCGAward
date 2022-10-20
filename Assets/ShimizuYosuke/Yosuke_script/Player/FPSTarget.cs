using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSTarget : MonoBehaviour
{
    [SerializeField] GameObject FPSTargetMove;
    [SerializeField] private float TargetHeight = 0.0f;
    [SerializeField] private float TargetDistance = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = FPSTargetMove.transform.position;

        this.transform.position += FPSTargetMove.transform.forward * TargetDistance;
        
       // this.transform.position = new Vector3(this.transform.position.x, FPSTargetMove.transform.position.y + TargetHeight, this.transform.position.z);
    }
}
