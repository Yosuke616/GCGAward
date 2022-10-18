using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform FPSTargetMove;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3(0.0f, FPSTargetMove.transform.eulerAngles.y, 0.0f);
    }
}
