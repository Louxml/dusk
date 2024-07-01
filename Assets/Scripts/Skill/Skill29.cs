
using System.Collections.Generic;
using UnityEngine;

public class Skill29 : SkillBase
{
    public Skill29() : base()
    {
        id = 29;
        //被动
        type = 1;
        name = "旋风劈";
        description = "攻击、反击能对自身周围8格敌人造成伤害。";
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
        Debug.Log("Skill29:");
        role.getXY(out int x, out int y);

        int[,] pos = new int[8, 2] {
            { -1, 1}, { 0, 1}, { 1, 1},{ -1, 0}, { 1, 0}, { -1, -1}, { 0, -1}, { 1, -1},
        };

        for (int i = 0; i <= pos.Length; i++)
        {
            int ex = x + pos[i, 0];
            int ey = y + pos[i, 1];
            Debug.Log("Skill29:" + ex + "," + ey);
            RoleControl enemy1 = RoleDataMgr.Instance.getRoleControl(ex, ey);
            if (enemy1 != null && enemy1 != enemy)
            {
                Debug.Log("Role Skill29:" + ex + "," + ey);
                CombatSystem.Instance.roleAttackEnemy(role, enemy1, false, () => { });

            }
        }
    }
}
