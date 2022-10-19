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
        // �i�K���������Ă���Ƃ�
        if (nCurrentStep > g_nOldStep)
        {
            // ���݂̒i�K����0�̎��͉������点�Ȃ�
            if (nCurrentStep != 0)
                objectsDecUI[nCurrentStep - 1].GetComponent<CArrowDecUI>().setSwitch(true);
            g_nOldStep = nCurrentStep;          // ���݂̒i�K����ޔ�������
        }
        // �i�K���������Ă���Ƃ�
        else if (nCurrentStep < g_nOldStep)
        {
            // �O��̒i�K����0�̎��͕ύX������̂��Ȃ��̂ŃX���[����
            if(g_nOldStep != 0)
            {
                // ��������x�Ƀ��Z�b�g����ꍇ�ɂ��Ή�
                int nDif = g_nOldStep - nCurrentStep;
                for(int i = 1; i < nDif + 1; ++i)
                    objectsDecUI[g_nOldStep - i].GetComponent<CArrowDecUI>().setSwitch(false);
            }
            g_nOldStep = nCurrentStep;           // ���݂̒i�K����ޔ�������
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
