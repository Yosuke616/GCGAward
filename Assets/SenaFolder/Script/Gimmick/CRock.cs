using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRock : MonoBehaviour
{
    [Header("石攻撃のため時間")]
    [SerializeField] private float fWaitTime;
    [Header("石の飛ぶスピード"),Range(1.0f,100.0f)]
    [SerializeField] private float fSpeed;
    [Header("飛ぶ角度")]
    [SerializeField] private Vector3 direction;
    //[Header("")]

    private Rigidbody rb;           // 矢の剛体

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        StartCoroutine("RockShoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 石発射
    private IEnumerator RockShoot()
    {
        yield return new WaitForSeconds(fWaitTime);
        rb.useGravity = true;
        //Vector3 direction = new Vector3(-1.0f, 1.0f, 0f);
        rb.AddForce(direction * fSpeed, ForceMode.Impulse);        // 岩を発射する
    }
}
