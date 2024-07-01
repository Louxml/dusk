
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Skill42 : SkillBase
{
    public Skill42() : base()
    {
        id = 42;
        //被动
        type = 1;
        name = "真伤";
        description = "攻击、反击至少造成3伤害";
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
        if (enemy.getBuffById(24) == null)
        {
            enemy.addBuff(new Buff24(1));
        }
    }
}
