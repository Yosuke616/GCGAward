using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotationTest : MonoBehaviour
{
    [SerializeField] GameObject FPSTarget;
    private Quaternion RotY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3 (FPSTarget.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
    }
}
