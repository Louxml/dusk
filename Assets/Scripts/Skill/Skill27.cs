
using System.Collections.Generic;
using UnityEngine;

public class Skill27 : SkillBase
{
    public Skill27() : base()
    {
        id = 27;
        //被动
        type = 1;
        name = "抵挡";
        description = "受到的近战伤害-2（至少为0.5）";
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
        damage = Mathf.Max(0.5f, damage - 2);
    }
}
