using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KesuDeath : MonoBehaviour
{
    private int i = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        i--;

        if (i < 0) {
            Destroy(this.gameObject);
        }
    }
}
