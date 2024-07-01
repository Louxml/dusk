

using UnityEngine;

public class Buff1 : BuffBase
{
    //移动距离
    int move;
    public Buff1(int type) : base(type)
    {
        move = 3;
    }

    public override void initData()
    {
        base.initData();
        id = 1;
        name = "出生点加移速";
    }


    public override void buffStart(RoleControl role)
    {
        role.addMoveDistance(move);
    }

    public override void roleMove(RoleControl role)
    {
        role.addMoveDistance(-move);
        destroy();
    }

}
