

using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Buff28 : BuffBase
{
    int addDef;
    public Buff28(int type) : base(type)
    {
        addDef = 3;
    }

    public override void initData()
    {
        base.initData();
        id = 28;
        name = "+3防的技能Buff";
    }

    public override void roundStart(RoleControl role)
    {
        if (--round <= 0)
        {
            disableBuff(role);
            destroy();
        }

    }

    void enableBuff(RoleControl role)
    {
        role.addDefValue(addDef);
    }

    void disableBuff(RoleControl role)
    {
        role.addDefValue(-addDef);
    }
    public override void buffStart(RoleControl role)
    {
        enableBuff(role);
    }
}
