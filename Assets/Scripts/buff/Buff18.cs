

using UnityEngine;

public class Buff18 : BuffBase
{
    public Buff18(int type) : base(type)
    {

    }

    public override void initData()
    {
        base.initData();
        id = 18;
        roundTotal = 1;
        round = 1;
        name = "无法行动、反击一回合";

    }

    public override void buffStart(RoleControl role)
    {
        enableBuff(role);
    }

    public override void roundStart(RoleControl role)
    {
        
        enableBuff(role);
        if (--round <= 0 )
        {
            destroy();
        }
    }

    public void enableBuff(RoleControl role)
    {
        role.roleModel.roleData.moveTimes = 0;
        //role.roleModel.roleData.attackTimes = 0;
        role.roleModel.roleData.backAttackTimes = 0;
    }

}
