

using UnityEngine;

public class Buff26 : BuffBase
{
    int subDef;
    public Buff26(int type) : base(type)
    {
        subDef = 100;
    }

    public override void initData()
    {
        base.initData();
        id = 26;
        name = "护甲清0，无法反击。持续一回合";
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
