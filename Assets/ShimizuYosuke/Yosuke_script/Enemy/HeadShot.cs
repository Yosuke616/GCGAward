using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
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
        if (collision.gameObject.tag == "Arrow") {
            //親のスクリプトを持ってくる
            CSenaEnemy obj = this.transform.parent.gameObject.GetComponent<CSenaEnemy>();
            obj.CollHead(collision);
        }
    }
}
