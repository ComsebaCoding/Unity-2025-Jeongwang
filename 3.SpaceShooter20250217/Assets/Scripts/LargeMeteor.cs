using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMeteor : Meteor
{
    protected override void OnDead()
    {
        //base.OnDead();
        GameManager.instance.AddScore(25);
        if (!outMapFlag)
        {
            for (int i = 0; i < 3; ++i)
                Instantiate(dustPrefab, transform.position, Quaternion.identity);
            // ������Ʈ Ǯ�� ���� �� ���� ����
            for (int i = 0; i < 3; ++i)
            {
                GameManager.instance.MeteorInstantiate(transform.position).direction
                    = Random.insideUnitCircle.normalized;
            }
        }
        GameManager.instance.LargeMeteorPooling(this);
    }
}