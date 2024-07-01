

using UnityEngine;

public class Buff3 : BuffBase
{
    //属性提升
    int atk;
    int def;
    public Buff3(int type) : base(type)
    {
        atk = 2;
        def = 2;
    }

    public override void initData()
    {
        base.initData();
        id = 3;
        name = "城墙加属性";
    }

    public override void buffStart(RoleControl role)
    {
        role.addAtkValue(atk);
        role.addDefValue(def);

        Debug.Log("ATK:" + role.getAtk());
    }

    public override void roleMove(RoleControl role)
    {
        role.addAtkValue(-atk);
        role.addDefValue(-def);
        destroy();
    }

}
