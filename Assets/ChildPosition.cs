using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPosition : MonoBehaviour
{
    //ついていきたいオブジェクトを設定する
    [SerializeField] private Transform Child;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Child.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Child.transform.position;
        Child.transform.position = this.transform.position;
        Debug.Log(Child.transform.position);
        Debug.Log(this.transform.position);
    }
}
