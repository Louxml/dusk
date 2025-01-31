
using System.Collections.Generic;
using UnityEngine;

public class Skill7 : SkillBase
{
    public Skill7() : base()
    {
        id = 7;
        //被动
        type = 1;
        name = "群攻";
        description = "对处于自身纵列、横列的单位攻击时，会对沿途3格的敌人造成伤害";
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
        for (int i = 1; i <= 3; i++)
        {
            int ex = x + i * hf;
            int ey = y + i * vf;
            Debug.Log("Skill7:" + ex + "," + ey);
            RoleControl enemy1 = RoleDataMgr.Instance.getRoleControl(ex, ey);
            if (enemy1 != null && enemy1 != enemy)
            {
                Debug.Log("Role Skill7:" + ex + "," + ey);
                CombatSystem.Instance.roleAttackEnemy(role, enemy1, false, () => { });

            }
        }
    }
}
