
using System.Collections.Generic;
using UnityEngine;

public class Skill36 : SkillBase
{
    public Skill36() : base()
    {
        id = 36;
        //被动
        type = 1;
        name = "反伤";
        description = "受到伤害时，发起者会受到2伤害";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    public override void onAffectAfter(RoleControl enemy, float damage)
    {
        role.getXY(out int x, out int y);
        enemy.affectAnimate(x, y, 2);
    }
}
