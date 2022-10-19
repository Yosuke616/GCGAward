using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kariscore : MonoBehaviour
{
    private int head = 100;
    private int Break = 200;
    private int wave = 300;
    private int total = 400;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetHead() {
        return head;
    }
    public int GetBreak() {
        return Break;
    }
    public int GetWave() {
        return wave;
    }
    public int GetTotal() {
        return total;
    }
}
