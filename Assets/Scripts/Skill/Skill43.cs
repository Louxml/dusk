
using System.Collections.Generic;
using UnityEngine;

public class Skill43 : SkillBase
{
    float subEnemyDef;
    bool isEnable;
    public Skill43() : base()
    {
        id = 43;
        //被动
        type = 1;
        name = "攻守兼备";
        description = "主动攻击时无视3护甲，受反击伤害-1";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
        isEnable = false;

        subEnemyDef = 3;
    }


    public override void onAttackBefore(RoleControl enemy, bool isBackAttack)
    {
        if (!isBackAttack)
        {
            isEnable = true;
            role.addSubEnemyDef(subEnemyDef);
        }
    }

    public override void onAttackAfter(RoleControl enemy, float damage)
    {
        if (isEnable)
        {
            isEnable = false;
            role.addSubEnemyDef(-subEnemyDef);
        }
    }

    public override void onAffectBefore(RoleControl enemy, ref float damage, bool isBackAttack, int damageType)
    {
        if (isBackAttack)
        {
            damage = Mathf.Max(0.5f, damage - 1);
        }
    }
}