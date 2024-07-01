

using UnityEngine;

public class Buff10 : BuffBase
{
    //移动距离
    int move;
    public Buff10(int type) : base(type)
    {
        move = 2;
    }

    public override void initData()
    {
        base.initData();
        id = 10;
        name = "风坛加移速";
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
