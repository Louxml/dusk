
using System.Collections.Generic;
using UnityEngine;

public class Skill48 : SkillBase
{
    public Skill48() : base()
    {
        id = 48;
        //被动
        type = 1;
        name = "免疫法术";
        description = "免疫法术伤害";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 1;
        cd = 0;
    }

    public override void onAffectBefore(RoleControl enemy, ref float damage, bool isBackAttack, int damageType)
    {
        if (damageType == 2)
        {
            damage = 0f;
        }
    }
}
