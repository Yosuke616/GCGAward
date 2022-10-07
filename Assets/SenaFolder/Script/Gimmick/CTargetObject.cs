using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTargetObject : MonoBehaviour
{
    #region serialize field
    [SerializeField] private Material[] matSwitch = new Material[2];
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Arrow")
        Debug.Log("<color=red>GimmickHit</color>");
    }
}
