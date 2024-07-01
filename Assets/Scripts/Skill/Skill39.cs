
using System.Collections.Generic;
using UnityEngine;

public class Skill39 : SkillBase
{
    float addDamage;
    bool isEnable;
    public Skill39() : base()
    {
        id = 39;
        //被动
        type = 1;
        name = "战意";
        description = "光灵身边不存在友方单位时（4格），每一次造成的伤害都会额外+1。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
        isEnable = false;

        addDamage = 1;
    }


    public override void onAttackAfter(RoleControl enemy, float damage)
    {
        if (isEnable)
        {
            role.addDamageValue(-addDamage);
        }
    }

    public override void onAttackBefore(RoleControl enemy, bool isBackAttack)
    {
        role.getXY(out int x, out int y);
        int[,] pos = new int[4, 2] {
            { 0, 1}, { -1, 0}, { 1, 0}, { 0, -1},
        };

        bool hasRole = false;
        for (int i = 0; i <= pos.Length; i++)
        {
            int ex = x + pos[i, 0];
            int ey = y + pos[i, 1];
            RoleControl role1 = RoleDataMgr.Instance.getRoleControl(ex, ey);
            if (role1 != null && role1.getRoleTag() == role.getRoleTag())
            {
                hasRole = true;
                break;
            }
        }
        if (!hasRole)
        {
            isEnable = true;
            role.addDamageValue(addDamage);
        }
    }
}
