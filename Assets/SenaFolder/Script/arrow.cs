using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    private enum STATE_ARROW
    {
        ARROW_CHARGE = 0,
        ARROW_NORMAL,
    }

    private STATE_ARROW state;
    [SerializeField] private GameObject PrefabArrow;       // 矢のオブジェクト
    [SerializeField] private GameObject spawner;
    [SerializeField] private float maxCharge;
    private GameObject objArrow;
    private float fCharge;
    
    // Start is called before the first frame update
    void Start()
    {
        state = STATE_ARROW.ARROW_NORMAL;
        fCharge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 左クリックでチャージ
        if (Input.GetMouseButtonDown(0))
        {
            state = STATE_ARROW.ARROW_CHARGE;
            objArrow = Instantiate(PrefabArrow, spawner.transform.position,Quaternion.Euler(-90.0f,0.0f,0.0f));
            objArrow.transform.parent = this.transform;
        }

        // チャージ中に左クリック離されたらチャージ解除
        if (state == STATE_ARROW.ARROW_CHARGE && Input.GetMouseButtonUp(0))
        {
            state = STATE_ARROW.ARROW_NORMAL;
            Destroy(objArrow);
        }

        Debug.Log(state);
    }
}
