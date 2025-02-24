using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMeteor : Meteor
{
    protected override void OnDead()
    {
        base.OnDead();

        // ������Ʈ Ǯ�� ���� �� ���� ����
        for (int i = 0; i < 3; ++i)
        {
            Meteor meteor = Instantiate<Meteor>(GameManager.instance.GetMeteorPrefab()
                , transform.position, Quaternion.identity);
            meteor.direction = Random.insideUnitCircle.normalized;
        }
    }
}