using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDeadZone : MonoBehaviour
{
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
        // ‘S‚Ä‚ð”j‰ó‚·‚é
        Destroy(collision.gameObject);
    }
}
