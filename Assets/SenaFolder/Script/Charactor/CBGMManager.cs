using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBGMManager : MonoBehaviour
{
    public enum BGMSTATE
    {
        STATE_NORMAL = 0,       // 通常時
        STATE_BATTLE,   // 戦闘時
        STATE_MAX,
    }
    private WaveManager waveManager;
    private bool isDiscovered;      // 戦闘中か
    private BGMSTATE g_state;
    private AudioSource audioSource;

    // BGM
    [Header("BGM(通常時→戦闘時)")]
    [SerializeField] private AudioClip[] bgm;
    // Start is called before the first frame update
    void Start()
    {
        waveManager = GetComponent<WaveManager>();
        isDiscovered = false;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgm[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        switch(g_state)
        {
            // 通常時
            case BGMSTATE.STATE_NORMAL:
                break;
            // 戦闘時
            case BGMSTATE.STATE_BATTLE:
                break;
        }
    }

    // 一度だけ呼ばれる
    private void ChangeState(BGMSTATE state)
    {
        g_state = state;
        audioSource.Stop();
        audioSource.clip = bgm[(int)state];
        audioSource.Play();
    }

    public void TellDiscoverPlayer(bool flg)
    {
        // 発見通知の場合
        if(flg)
        {
            // 通常時の場合BGMを変更する
            if (g_state == BGMSTATE.STATE_NORMAL)
            {
                isDiscovered = true;        // 発見通知受け取り状態にする
                StartCoroutine("ChangeNormal");
            }
        }
        else
        {
            if (g_state == BGMSTATE.STATE_BATTLE)
            {
                isDiscovered = false;       // 発見通知未受け取り状態にする
                StartCoroutine("ChangeBattle");
            }
        }
    }

    private IEnumerator ChangeBattle()
    {
        yield return new WaitForSeconds(3.0f);
        // 3秒後も発見通知を受け取っていない場合通常状態に変更する
        if(!isDiscovered)
            ChangeState(BGMSTATE.STATE_NORMAL);
    }
    private IEnumerator ChangeNormal()
    {
        yield return new WaitForSeconds(3.0f);
        // 3秒後も発見通知を受け取っていない場合通常状態に変更する
        if (isDiscovered)
            ChangeState(BGMSTATE.STATE_BATTLE);
    }
}
