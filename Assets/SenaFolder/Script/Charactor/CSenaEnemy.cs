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
        // –î‚ª“–‚½‚Á‚½ê‡A©g‚Æ–î‚ğÁ–Å‚³‚¹‚é
        if(collision.gameObject.tag == "Arrow")
        {
            Debug.Log("<color=green>EnemyHit</color>");
            Destroy(collision.gameObject);      // –î‚ğÁ–Å‚³‚¹‚é
            Destroy(gameObject);      // ©g‚ğÁ–Å‚³‚¹‚é
        }
    }
}
