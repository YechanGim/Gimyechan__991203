using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    //������ ������Ʈ�� ���� ��ũ��Ʈ
    float attackPower = 15.0f;

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
            if (battle != null)
            {
                battle.TakeDamage(attackPower);
            }
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
