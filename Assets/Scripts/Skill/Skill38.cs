
using System.Collections.Generic;
using UnityEngine;

public class Skill38 : SkillBase
{
    float subEnemyDef;
    bool isEnable;
    public Skill38() : base()
    {
        id = 38;
        //被动
        type = 1;
        name = "周围易伤";
        description = "与自身周围8格的敌人战斗时无视2护甲。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
        isEnable = false;

        subEnemyDef = 2;
    }


    public override void onAttackBefore(RoleControl enemy, bool isBackAttack)
    {
        role.getXY(out int x, out int y);
        enemy.getXY(out int ex, out int ey);
        if (ex >= x - 1 && ex <= x + 1 && ey >= y - 1 && ey <= y + 1)
        {
            isEnable = true;
            role.addSubEnemyDef(subEnemyDef);
        }
    }

    public override void onAttackAfter(RoleControl enemy, float damage)
    {
        if (isEnable)
        {
            role.addSubEnemyDef(-subEnemyDef);
        }
    }
}
