using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHeartUI : MonoBehaviour
{
    private GameObject objPlayer;
    private int nHP;
    private int nMaxHP;
    private Animator anim;
    private float animSpeed;
    // Start is called before the first frame update
    void Start()
    {
        objPlayer = GameObject.FindWithTag("Player").gameObject;        // �v���C���[�̏��擾
        nMaxHP = objPlayer.GetComponent<CCharactorManager>().nMaxHp;    // �v���C���[�̍ő�HP�擾
        anim = GetComponent<Animator>();                                // �A�j���[�V�������̎擾
        animSpeed = anim.speed;                                         // �A�j���[�V�����X�s�[�h�̎擾
    }

    // Update is called once per frame
    void Update()
    {
        nHP = objPlayer.GetComponent<CCharactorManager>().nCurrentHp;

        // ���݂�HP���ő�HP��1/10�ɂȂ����ꍇ
        if (nHP < nMaxHP / 10)
            anim.speed = animSpeed * 3;
        // ���݂�HP���ő�HP�̔����ɂȂ����ꍇ
        else if(nHP < nMaxHP / 2)
            anim.speed = animSpeed * 2;
    }
}
