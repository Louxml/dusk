
using System.Collections.Generic;
using UnityEngine;

public class Skill40 : SkillBase
{
    public Skill40() : base()
    {
        id = 40;
        //被动
        type = 1;
        name = "溅射";
        description = "每回合第一次主动攻击可对该方向一排（3格）敌人造成伤害";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 1;
        cd = 0;
    }

    public override void onSelectAttackEnemy(RoleControl enemy)
    {
        enemy.getXY(out int x, out int y);

        int[,] pos = new int[2, 2] {
            { -1, 0}, { 1, 0},
        };

        bool isUse = false;
        for (int i = 0; i <= pos.Length; i++)
        {
            int ex = x + pos[i, 0];
            int ey = y + pos[i, 1];
            RoleControl enemy1 = RoleDataMgr.Instance.getRoleControl(ex, ey);
            if (enemy1 != null && enemy1 != enemy)
            {
                isUse = true;
                CombatSystem.Instance.roleAttackEnemy(role, enemy1, false, () => { });
            }
        }
        
        if (isUse) {
            useSkill();
        }
    }
}
