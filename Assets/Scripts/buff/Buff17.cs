

using UnityEngine;

public class Buff17 : BuffBase
{
    int subDef;
    public Buff17(int type) : base(type)
    {
        subDef = 3;
    }

    public override void initData()
    {
        base.initData();
        id = 17;
        roundTotal = 1;
        round = 1;
        name = "降低目标3点护甲，持续1回合";

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
