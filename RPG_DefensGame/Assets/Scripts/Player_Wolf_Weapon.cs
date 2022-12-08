using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Wolf_Weapon : MonoBehaviour
{
    //�÷��̾��� ���⿡ ���� ��ũ��Ʈ


    PlayerWolf weaponOwner;

    private void Start()
    {
        weaponOwner = GameManager.INSTANCE.PLAYER.GetComponent<PlayerWolf>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            if (battle != null)
            {
                battle.TakeDamage(50.0f,1);
                if(weaponOwner.isSkillOn)
                {
                    StartCoroutine(SkillAttack(other));
                }
            }

        }
    }
    /// <summary>
    /// ��ų�ߵ��� ������ �о�� IEnumerator
    /// </summary>
    /// <param name="other">Ÿ��</param>
    /// <returns></returns>
    IEnumerator SkillAttack(Collider other)
    {
        other.attachedRigidbody.isKinematic = false;
        Debug.Log("��ų�� ���� �ߵ�");
        other.attachedRigidbody.AddForce(-other.transform.forward * 5.0f, ForceMode.Impulse);
        yield return new WaitForSeconds(1.0f);
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.isKinematic = true;
        }
    }
}
