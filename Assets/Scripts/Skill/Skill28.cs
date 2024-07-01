
using System.Collections.Generic;
using UnityEngine;

public class Skill28 : SkillBase
{
    public Skill28() : base()
    {
        id = 28;
        //被动
        type = 1;
        name = "免疫反击";
        description = "免疫反击伤害";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    public override void onAffectBefore(RoleControl enemy, ref float damage, bool isBackAttack, int damageType)
    {
        if (isBackAttack)
        {
            damage = 0;
        }
    }
}
