

using UnityEngine;

public class Buff25 : BuffBase
{
    int subDef;
    public Buff25(int type) : base(type)
    {
        subDef = 1;
    }

    public override void initData()
    {
        base.initData();
        id = 25;
        name = "-1防，无法反击。持续一回合";
        roundTotal = 1;
        round = 1;
    }

    public override void roundStart(RoleControl role)
    {
        if (--round <= 0)
        {
            disableBuff(role);
            destroy();
        }

    }

    public override void buffStart(RoleControl role)
    {
        enableBuff(role);
        role.roleModel.roleData.backAttackTimes = 0;
    }

    void enableBuff(RoleControl role)
    {
        role.addDefValue(-subDef);
    }

    void disableBuff(RoleControl role)
    {
        role.addDefValue(subDef);
    }
}
