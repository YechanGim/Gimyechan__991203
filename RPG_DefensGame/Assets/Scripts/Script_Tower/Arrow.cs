using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //ȭ�� ������Ʈ�� ���� ��ũ��Ʈ

    float attackPower = 20.0f;

    private void Start()
    {
        StartCoroutine(Del());
    }

    //���� ������ �������� �ָ鼭 ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            if(battle != null)
            {
                battle.TakeDamage(attackPower);
            }

            //other.GetComponent<IBattle>().TakeDamage(attackPower);
            //Debug.Log("Enemy Hit!!!");
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// isTrigger�� �����߱⶧���� ���� ����Ͽ� ������ ���󰡴� ���� �����ϱ����� 3�ʵ� �ڵ����� ����
    /// </summary>
    /// <returns></returns>
    IEnumerator Del()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
