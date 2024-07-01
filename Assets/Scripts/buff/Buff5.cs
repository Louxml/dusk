

using UnityEngine;

public class Buff5 : BuffBase
{
    //属性提升
    int atk;
    int def;
    public Buff5(int type) : base(type)
    {
        atk = 1;
        def = 1;
    }

    public override void initData()
    {
        base.initData();
        id = 5;
        name = "城堡加属性";
    }

    public override void buffStart(RoleControl role)
    {
        role.addAtkValue(atk);
        role.addDefValue(def);
    }

    public override void roleMove(RoleControl role)
    {
        role.addAtkValue(-atk);
        role.addDefValue(-def);
        destroy();
    }

}
