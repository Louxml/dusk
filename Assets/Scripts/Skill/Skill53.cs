
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill53 : SkillBase
{

    float damage;
    float addHp;
    public Skill53() : base()
    {
        id = 53;
        //主动
        type = 2;
        controlType = 1;

        name = "光尘";
        description = "对自身周围8格敌人造成4伤害，对自身周围8格友军恢复2生命。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 1;
        cd = 0;

        damage = 4f;
        addHp = 2f;
    }

    public override void onClickSkill()
    {
        int playerTag = role.getRoleTag();
        role.getXY(out int x, out int y);
        int[,] pos = { { -1, 1 }, { 0, 1 }, { 1, 1 }, { -1, 0 }, { 1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };
        for (int i = 0; i < pos.GetLength(0); i++)
        {
            int tx = x + pos[i, 0];
            int ty = y + pos[i, 1];
            RoleControl role1 = RoleDataMgr.Instance.getRoleControl(tx, ty);
            if (role1 != null)
            {
                if (role1.getRoleTag() == playerTag)
                {
                    role1.addHp(addHp, false);
                }
                else
                {
                    CombatSystem.Instance.roleMagicEnemy(role, role1, damage, null);
                }
            }
        }

        useSkill();
    }

}
