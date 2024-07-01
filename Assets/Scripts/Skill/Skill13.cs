
using System.Collections.Generic;
using UnityEngine;

public class Skill13 : SkillBase
{
    public Skill13() : base()
    {
        id = 13;
        //被动
        type = 1;
        name = "护盾";
        description = "每回合可以免疫第一次攻击";
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
        if (isUse())
        {
            damage = 0f;
            useSkill();
        }
    }
}
