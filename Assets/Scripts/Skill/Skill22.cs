
using System.Collections.Generic;
using UnityEngine;

public class Skill22 : SkillBase
{
    float subEnemyDef;
    public Skill22() : base()
    {
        id = 22;
        //被动
        type = 1;
        name = "易伤";
        description = "无视2点护甲";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;

        subEnemyDef = 2;
    }


    public override void onAttackBefore(RoleControl enemy, bool isBackAttack)
    {
        role.addSubEnemyDef(subEnemyDef);
    }

    public override void onAttackAfter(RoleControl enemy, float damage)
    {
        role.addSubEnemyDef(-subEnemyDef);
    }
}
