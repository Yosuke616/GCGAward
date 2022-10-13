using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class CArrowDecUIManager : MonoBehaviour
{

    #region variable
    private GameObject[] objectsDecUI;      // 威力表示UIの数
    private int maxDecStep;
    private GameObject objWeapon;           // 武器オブジェクト
    private int g_nOldStep;
    private bool bDebug;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // 武器オブジェクトを取得する
        objWeapon = GameObject.FindWithTag("Weapon");
        // 最大威力段階数を取得する
        maxDecStep = objWeapon.GetComponent<CBow>().GetMaxStep();
        // 子オブジェクトのUIを全て取得する
        objectsDecUI = GetChildren(gameObject, maxDecStep);
        g_nOldStep = 0;
        bDebug = false;
    }

    // Update is called once per frame
    void Update()
    {
        int nCurrentStep = objWeapon.GetComponent<CBow>().GetStep();
        // 段階数が変更されたら色を変更する
        if (nCurrentStep != g_nOldStep)
        {
            if (nCurrentStep == 0)
            {
                objectsDecUI[g_nOldStep - 1].GetComponent<CArrowDecUI>().setSwitch(false);
                g_nOldStep = nCurrentStep;
            }
            else if(g_nOldStep == 0)
            {
                objectsDecUI[nCurrentStep - 1].GetComponent<CArrowDecUI>().setSwitch(true);
                g_nOldStep = nCurrentStep;
            }
            else
            {
                objectsDecUI[nCurrentStep - 1].GetComponent<CArrowDecUI>().setSwitch(true);
                objectsDecUI[g_nOldStep - 1].GetComponent<CArrowDecUI>().setSwitch(false);
                g_nOldStep = nCurrentStep;
            }
        }
        else
        {
            int i = 0;
        }
    }

    /*
    * @brief 子オブジェクトになっているUIを全て取得する
    * @param parent 親オブジェクト/ childNum 子供の数
    * @sa Start()
　   */
    private GameObject[] GetChildren(GameObject parent, int childNum)
    {
        GameObject[] children = new GameObject[childNum];
        for (int i = 0; i < maxDecStep; ++i)
        {
            children[i] = parent.transform.GetChild(i).gameObject;
        }
        return children;
    }
}
