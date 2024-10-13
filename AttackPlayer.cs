using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private bool cantouch = true;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player3D") && cantouch)
        {
            cantouch = false;
            collider.gameObject.GetComponent<HealthSettingIn3D>().NowHealth -= 5;
            Destroy(gameObject,1f);
        }
    }
}
