
using UnityEngine;

public class BuffBase
{
    //id
    public int id;
    //类型
    public int type;
    //名字
    public string name;

    //生效回合数
    public int roundTotal;
    //剩余回合
    public int round;
   //是否销毁
    public bool destroyed;
    //over是否已执行
    public bool isOver;
    //是否是被动技能
    public bool isSkill;

    public BuffBase(int type)
    {
        this.type = type;
        initData();
    }

    public virtual void initData()
    {
        id = 0;
        name = string.Empty;
        roundTotal = 0;
        round = 0;
        destroyed = false;
        isOver = false;
        isSkill = false;
    }

    //阶段
    //回合开始（已处理）
    public virtual void roundStart(RoleControl role)
    {
        Debug.Log("roundStart");
    }

    //buff挂载时(已处理)
    public virtual void buffStart(RoleControl role)
    {
        Debug.Log("buffStart");
    }

    //棋子移动后（已处理）
    public virtual void roleMove(RoleControl role)
    {
        Debug.Log("roleMove");
    }

    //棋子攻击后（已处理）
    public virtual void roleAttack(RoleControl role, RoleControl enemy, float damge)
    {
        Debug.Log("roleAttack");
    }
    
    //地图上棋子位置变化（已处理）
    public virtual void roleMapChange(RoleControl role, int playerTag)
    {
        Debug.Log("roleMapChange");
    }

    //棋子受击后（已处理）
    public virtual void roleAffect(RoleControl role, ref float damage)
    {
        Debug.Log("roleAffect");
    }

    //棋子反击后
    public virtual void roleBackAttack(RoleControl role)
    {
        Debug.Log("roleBackAttack");
    }

    //棋子死亡后
    public virtual void roleDeath(RoleControl role, RoleControl enemy)
    {
        Debug.Log("roleDeath");
    }

    //回合结束（已处理）
    public virtual void roundOver(RoleControl role)
    {
        Debug.Log("roundOver");
    }

    //buff结束时(已处理)
    public virtual void buffOver(RoleControl role)
    {
        Debug.Log("buffOver");
    }

    //buff移除时，role移除（）
    public virtual void buffRemove(RoleControl role)
    {
        Debug.Log("buffRemove");
    }

    public void destroy()
    {

        destroyed = true;
    }

}