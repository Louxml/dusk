
using System.Collections.Generic;
using UnityEngine;

public class Skill35 : SkillBase
{
    public Skill35() : base()
    {
        id = 35;
        //被动
        type = 1;
        name = "反击抵挡";
        description = "受到的反击伤害-3（至少为1）";
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
            damage = Mathf.Max(1f, damage - 3);
        }
    }
}
