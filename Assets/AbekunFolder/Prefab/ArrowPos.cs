using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPos : MonoBehaviour
{
    public GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parent = transform.parent.gameObject;
        if(PlayerInputTest.GetChargeMode())
        {
            if (!(parent == null))
            {
                if (this.GetComponent<CArrow>().GetNum() == parent.GetComponent<CBow>().GetCurrentArrowNum())
                {
                    if (parent.GetComponent<CBow>().GetChargeFlg())
                    {
                        this.transform.position = parent.transform.position;
                        this.transform.eulerAngles = new Vector3(parent.transform.eulerAngles.x-90, parent.transform.eulerAngles.y-2, parent.transform.eulerAngles.z);
                    }
                }
            }
        }
    }
}
