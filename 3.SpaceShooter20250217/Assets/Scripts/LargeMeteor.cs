using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMeteor : Meteor
{
    protected override void OnDead()
    {
        base.OnDead();

        // 오브젝트 풀링 구현 시 변경 예정
        for (int i = 0; i < 3; ++i)
        {
            Meteor meteor = Instantiate<Meteor>(GameManager.instance.GetMeteorPrefab()
                , transform.position, Quaternion.identity);
            meteor.direction = Random.insideUnitCircle.normalized;
        }
    }
}