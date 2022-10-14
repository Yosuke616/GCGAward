using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaEnemy : CCharactorManager
{
    #region serialize field
    [SerializeField] private GameObject sceneManager;
    //[SerializeField] private int nAddScore;
    [SerializeField] private GameObject objDamageUI;
    #endregion

    // �ϐ��錾
    #region variable
    private CScore scScore;     // �X�R�A�̏��i�[�p
    private GameObject objPlayer;
    #endregion
    // Start is called before the first frame update
    #region init
    void Start()
    {
        InitHP();       // HP�̏�����
        InitAtk();      // �U���͂̏�����
        SetHPBar();
        objPlayer = GameObject.FindWithTag("Player");
        //scScore = sceneManager.GetComponent<CScore>();      // �X�R�A�̏����擾����
    }
    #endregion

    // Update is called once per frame
    #region update
    void Update()
    {
        Debug.Log("EnemyAtk" + nCurrentAtk);
    }
    #endregion

    #region collision
    private void OnCollisionEnter(Collision collision)
    {
        // ����������ꍇ�A���g�Ɩ�����ł�����
        if(collision.gameObject.tag == "Arrow")
        {
            Debug.Log("<color=green>EnemyHit</color>");
            //scScore.addScore(nAddScore);        // �X�R�A�����Z����
            Destroy(collision.gameObject);      // ������ł�����
            // ����������̃_���[�W�����擾����
            int DamageNum = collision.gameObject.GetComponent<CArrow>().GetArrowAtk();
            // �_���[�W�ʒm
            ChangeHp(-1 * DamageNum);
            objDamageUI.GetComponent<CDamageUI>().TellDamaged(DamageNum);
        }
    }
    #endregion
}
