

using System.Data;
using UnityEngine;

public class Buff2 : BuffBase
{
    //伤害
    int damage;
    public Buff2(int type) : base(type)
    {
        damage = 3;
    }

    public override void initData()
    {
        base.initData();
        id = 2;
        name = "出生点扣HP";
    }


    public override void roundOver(RoleControl role)
    {
        role.affectAnimate(0, 0, damage);
    }

    public override void roleMove(RoleControl role)
    {
        destroy();
    }

}
