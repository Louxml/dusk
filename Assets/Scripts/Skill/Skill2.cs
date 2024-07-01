
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : SkillBase
{
    float subEnemyDef;
    public Skill2() : base()
    {
        id = 2;
        //被动
        type = 1;
        name = "破甲";
        description = "无视护甲";
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
        subEnemyDef = enemy.getDef();
        role.addSubEnemyDef(subEnemyDef);
    }

    public override void onAttackAfter(RoleControl enemy, float damage)
    {
        role.addSubEnemyDef(-subEnemyDef);
    }
}
