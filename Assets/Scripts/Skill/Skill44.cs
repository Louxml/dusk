
using System.Collections.Generic;
using UnityEngine;

public class Skill44 : SkillBase
{
    public Skill44() : base()
    {
        id = 44;
        //被动
        type = 1;
        name = "血怒";
        description = "每+1生命损失，攻击力+1。";
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
        int atk = Mathf.FloorToInt(damage);

        role.addAtkValue(atk);
    }
}