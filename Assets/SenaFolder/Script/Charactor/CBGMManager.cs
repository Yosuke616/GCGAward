using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBGMManager : MonoBehaviour
{
    public enum BGMSTATE
    {
        STATE_NORMAL = 0,       // �ʏ펞
        STATE_BATTLE,   // �퓬��
        STATE_MAX,
    }
    private WaveManager waveManager;
    private bool isDiscovered;      // �퓬����
    private BGMSTATE g_state;
    private AudioSource audioSource;

    // BGM
    [Header("BGM(�ʏ펞���퓬��)")]
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
            // �ʏ펞
            case BGMSTATE.STATE_NORMAL:
                break;
            // �퓬��
            case BGMSTATE.STATE_BATTLE:
                break;
        }
    }

    // ��x�����Ă΂��
    private void ChangeState(BGMSTATE state)
    {
        g_state = state;
        audioSource.Stop();
        audioSource.clip = bgm[(int)state];
        audioSource.Play();
    }

    public void TellDiscoverPlayer(bool flg)
    {
        // �����ʒm�̏ꍇ
        if(flg)
        {
            // �ʏ펞�̏ꍇBGM��ύX����
            if (g_state == BGMSTATE.STATE_NORMAL)
            {
                isDiscovered = true;        // �����ʒm�󂯎���Ԃɂ���
                StartCoroutine("ChangeNormal");
            }
        }
        else
        {
            if (g_state == BGMSTATE.STATE_BATTLE)
            {
                isDiscovered = false;       // �����ʒm���󂯎���Ԃɂ���
                StartCoroutine("ChangeBattle");
            }
        }
    }

    private IEnumerator ChangeBattle()
    {
        yield return new WaitForSeconds(3.0f);
        // 3�b��������ʒm���󂯎���Ă��Ȃ��ꍇ�ʏ��ԂɕύX����
        if(!isDiscovered)
            ChangeState(BGMSTATE.STATE_NORMAL);
    }
    private IEnumerator ChangeNormal()
    {
        yield return new WaitForSeconds(3.0f);
        // 3�b��������ʒm���󂯎���Ă��Ȃ��ꍇ�ʏ��ԂɕύX����
        if (isDiscovered)
            ChangeState(BGMSTATE.STATE_BATTLE);
    }
}
