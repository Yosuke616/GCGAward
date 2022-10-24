using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRock : MonoBehaviour
{
    [Header("êŒçUåÇÇÃÇΩÇﬂéûä‘")]
    [SerializeField] private float fWaitTime;
    //[Header("")]

    private Rigidbody rb;           // ñÓÇÃçÑëÃ

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator RockShoot()
    {
        yield return new WaitForSeconds(fWaitTime);
        rb.useGravity = true;
        //arrowForce = chargeTime * fFlyDistance;
        //nArrowAtk = nAtk;
        //Vector3 direction = -transform.up;
        //rb.AddForce(direction * arrowForce, ForceMode.Impulse);        // ñÓÇî≠éÀÇ∑ÇÈ
    }
}
