

using UnityEngine;

public class Buff27 : BuffBase
{
    public Buff27(int type) : base(type)
    {

    }

    public override void initData()
    {
        base.initData();
        id = 27;
        roundTotal = 1;
        round = 1;
        name = "无法攻击、反击一回合";

    }

    public override void buffStart(RoleControl role)
    {
        enableBuff(role);
    }

    public override void roundStart(RoleControl role)
    {

        enableBuff(role);
        if (--round <= 0)
        {
            destroy();
        }
    }

    public void enableBuff(RoleControl role)
    {
        //role.roleModel.roleData.moveTimes = 0;
        role.roleModel.roleData.attackTimes = 0;
        role.roleModel.roleData.backAttackTimes = 0;
    }

}
