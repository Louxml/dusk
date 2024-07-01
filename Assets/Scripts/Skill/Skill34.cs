
using System.Collections.Generic;
using UnityEngine;

public class Skill34 : SkillBase
{
    public Skill34() : base()
    {
        id = 34;
        //被动
        type = 1;
        name = "直线";
        description = "主动攻击时，会对整条直线上的敌方单位造成伤害。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    public override void onSelectAttackEnemy(RoleControl enemy)
    {
        role.getXY(out int x, out int y);
        enemy.getXY(out int enemyX, out int enemyY);


        int hf = 0, vf = 0;
        if (enemyX == x)    // 纵向
        {
            if (enemyY < y) //下边
            {
                vf = -1;
            }
            else if (enemyY > y)    // 上边
            {
                vf = 1;
            }

        }
        else if (enemyY == y)   // 纵向
        {
            if (enemyX < x)
            {
                hf = -1;
            }
            else if (enemyX > x)
            {
                hf = 1;
            }
        }

        if (hf == 0 && vf == 0)
        {
            return;
        }

        int length = role.getAttackDistance();
        for (int i = 1; i <= length; i++)
        {
            int ex = x + i * hf;
            int ey = y + i * vf;
            RoleControl enemy1 = RoleDataMgr.Instance.getRoleControl(ex, ey);
            if (enemy1 != null && enemy1 != enemy)
            {
                CombatSystem.Instance.roleAttackEnemy(role, enemy1, false, () => { });

            }
        }
    }
}
