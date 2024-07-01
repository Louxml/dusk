

using UnityEngine;

public class Buff9 : BuffBase
{
    //属性提升
    int atk;
    int def;
    int move;
    public Buff9(int type) : base(type)
    {
        atk = 3;
        def = 1;
        move = 2;
    }

    public override void initData()
    {
        base.initData();
        id = 9;
        name = "神庙第二个buff";
    }

    public override void buffStart(RoleControl role)
    {
        role.addAtkValue(atk);
        role.addDefValue(def);
        role.addMoveDistance(move);
    }

    public override void buffOver(RoleControl role)
    {
        role.addAtkValue(-atk);
        role.addDefValue(-def);
        role.addMoveDistance(-move);
    }

}
