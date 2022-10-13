using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class CArrowDecUIManager : MonoBehaviour
{

    #region variable
    private GameObject[] objectsDecUI;      // �З͕\��UI�̐�
    private int maxDecStep;
    private GameObject objWeapon;           // ����I�u�W�F�N�g
    private int g_nOldStep;
    private bool bDebug;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        // ����I�u�W�F�N�g���擾����
        objWeapon = GameObject.FindWithTag("Weapon");
        // �ő�З͒i�K�����擾����
        maxDecStep = objWeapon.GetComponent<CBow>().GetMaxStep();
        // �q�I�u�W�F�N�g��UI��S�Ď擾����
        objectsDecUI = GetChildren(gameObject, maxDecStep);
        g_nOldStep = 0;
        bDebug = false;
    }

    // Update is called once per frame
    void Update()
    {
        int nCurrentStep = objWeapon.GetComponent<CBow>().GetStep();
        // �i�K�����ύX���ꂽ��F��ύX����
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
    * @brief �q�I�u�W�F�N�g�ɂȂ��Ă���UI��S�Ď擾����
    * @param parent �e�I�u�W�F�N�g/ childNum �q���̐�
    * @sa Start()
�@   */
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
