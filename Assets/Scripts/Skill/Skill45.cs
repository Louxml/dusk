
using System.Collections.Generic;
using UnityEngine;

public class Skill45 : SkillBase
{

    int kills;
    public Skill45() : base()
    {
        id = 45;
        //被动
        type = 1;
        name = "血液积累";
        description = "若在一个回合内，击杀3名敌人，则回合结束时恢复所有生命值。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;

        kills = 0;
    }

    public override void onRoundStart()
    {
        kills = 0;
    }

    public override void onAttackAfter(RoleControl enemy, float damage)
    {
        if (!enemy.isLife())
        {
            kills++;
        }
    }

    public override void onRoundOver()
    {
        if (kills >= 3)
        {
            float hp = 999f;
            role.addHp(hp, false);
        }
    }
}