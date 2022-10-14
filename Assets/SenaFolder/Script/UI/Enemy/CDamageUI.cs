using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CDamageUI : MonoBehaviour
{
    #region serialize field
    [Header("�_���[�W��UI�̐F")]
    [SerializeField] private Color textColor;
    [SerializeField] private GameObject objDamageUI;
    [Header("UI�\������")]
    [SerializeField] private int nLifeTime;     // UI��\�������鎞��
    #endregion

    #region valiable
    private int nShowNum = 0;           // �\�����鐔�l
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * @brief �_���[�W���󂯂��ʒm���󂯎��
     * @param DamageNum �󂯂��_���[�W��
     * @sa �I�u�W�F�N�g���_���[�W���󂯂���
     * @details �_���[�W���󂯂��I�u�W�F�N�g����_���[�W�ʂ��󂯎��
    */
    #region tell damaged
    public void TellDamaged(int DamageNum)
    {
        nShowNum = DamageNum;
        GameObject obj = Instantiate(objDamageUI);
        //GameObject test = obj.transform.GetChild(0).;
        obj.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = DamageNum.ToString();
        StartCoroutine("DestroyUI",obj);
    }
    #endregion

    /*
     * @brief UI���폜����
     * @details UI�\�����Ԃ��߂�����UI���폜����
    */
    #region destroy ui
    private IEnumerator DestroyUI(GameObject target)
    {
        yield return new WaitForSeconds(nShowNum);
        Destroy(target);
    }
    #endregion
}
