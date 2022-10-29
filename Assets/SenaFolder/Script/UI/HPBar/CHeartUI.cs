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
        objPlayer = GameObject.FindWithTag("Player").gameObject;        // プレイヤーの情報取得
        nMaxHP = objPlayer.GetComponent<CCharactorManager>().nMaxHp;    // プレイヤーの最大HP取得
        anim = GetComponent<Animator>();                                // アニメーション情報の取得
        animSpeed = anim.speed;                                         // アニメーションスピードの取得
    }

    // Update is called once per frame
    void Update()
    {
        nHP = objPlayer.GetComponent<CCharactorManager>().nCurrentHp;

        // 現在のHPが最大HPの1/10になった場合
        if (nHP < nMaxHP / 10)
            anim.speed = animSpeed * 3;
        // 現在のHPが最大HPの半分になった場合
        else if(nHP < nMaxHP / 2)
            anim.speed = animSpeed * 2;
    }
}
