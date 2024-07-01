
using System.Collections.Generic;
using UnityEngine;

public class Skill18 : SkillBase
{
    public Skill18() : base()
    {
        id = 18;
        //被动
        type = 1;
        name = "骑脸";
        description = "对距离为1的敌人造成额外2伤害。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    public override void onAttackBefore(RoleControl enemy, bool isBackAttack)
    {
        role.getXY(out int x, out int y);
        enemy.getXY(out int ex, out int ey);
        if (MapDataMgr.Instance.getNodeDistance(x, y, ex, ey) == 1)
        {
            enemy.addBuff(new Buff19(1));
        }
    }
}
