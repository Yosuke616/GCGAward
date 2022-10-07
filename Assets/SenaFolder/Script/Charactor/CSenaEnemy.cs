using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSenaEnemy : MonoBehaviour
{
    #region serialize field
    //[SerializeField] private Material[] matSwitch = new Material[2];
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 矢が当たった場合、自身と矢を消滅させる
        if(collision.gameObject.tag == "Arrow")
        {
            Debug.Log("<color=green>EnemyHit</color>");
            Destroy(collision.gameObject);      // 矢を消滅させる
            Destroy(gameObject);      // 自身を消滅させる
        }
    }
}
