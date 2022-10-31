using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyDamage : MonoBehaviour
{
    #region serialize field
    [Header("�q�b�g�J�[�\���̕`�掞��")]
    [SerializeField] private float fLifeTime;
    #endregion

    #region variable
    private GameObject objCursurUI;        // �J�[�\��UI
    private GameObject objHitCursur;       // �q�b�g�J�[�\��
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        objCursurUI = GameObject.FindWithTag("Cursur");
        transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /*
    * @brief �G����̖�̏Փ˒ʒm�󂯎��
    * @sa �G�ɖ����������
  �@*/
    public void ArrowHit()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine("DisabledHitCursur", transform.GetChild(0).gameObject);
    }

    private IEnumerator DisabledHitCursur(GameObject cursur)
    {
        yield return new WaitForSeconds(fLifeTime);
        cursur.SetActive(false);
    }
}
