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
        // 段階数が増えているとき
        if (nCurrentStep > g_nOldStep)
        {
            // 現在の段階数が0の時は何も光らせない
            if (nCurrentStep != 0)
                objectsDecUI[nCurrentStep - 1].GetComponent<CArrowDecUI>().setSwitch(true);
            g_nOldStep = nCurrentStep;          // 現在の段階数を退避させる
        }
        // 段階数が減っているとき
        else if (nCurrentStep < g_nOldStep)
        {
            // 前回の段階数が0の時は変更するものがないのでスルーする
            if(g_nOldStep != 0)
            {
                // 複数個を一度にリセットする場合にも対応
                int nDif = g_nOldStep - nCurrentStep;
                for(int i = 1; i < nDif + 1; ++i)
                    objectsDecUI[g_nOldStep - i].GetComponent<CArrowDecUI>().setSwitch(false);
            }
            g_nOldStep = nCurrentStep;           // 現在の段階数を退避させる
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
