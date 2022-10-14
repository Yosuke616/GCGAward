using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaEnemy : CHPManager
{
    #region serialize field
    [SerializeField] private GameObject sceneManager;
    [SerializeField] private int nAddScore;
    #endregion

    // �ϐ��錾
    #region variable
    private CScore scScore;     // �X�R�A�̏��i�[�p
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        InitHP();      // HP�̏�����
        SetHPBar();
        //scScore = sceneManager.GetComponent<CScore>();      // �X�R�A�̏����擾����
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("currentHP" + nCurrentHp);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ����������ꍇ�A���g�Ɩ�����ł�����
        if(collision.gameObject.tag == "Arrow")
        {
            Debug.Log("<color=green>EnemyHit</color>");
            //scScore.addScore(nAddScore);        // �X�R�A�����Z����
            Destroy(collision.gameObject);      // ������ł�����
            ChangeHp(-1);
        }
    }
}
