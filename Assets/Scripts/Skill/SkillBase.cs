
using UnityEngine;

public class SkillBase
{
    //id
    public int id;
    //类型
    public int type;
    //名字
    public string name;

    public RoleControl role;

    public string description;

    public int controlType;

    //剩余次数
    public int times;
    //总次数
    public int totalTimes;
    //总冷却回合
    public int cdTotal;
    //冷却回合
    public int cd;

    public SkillBase()
    {
        id = 0;
        name = string.Empty;
        description = string.Empty;
        controlType = 0;
        initData();
    }

    public virtual void initData()
    {
        totalTimes = 1;
        times = totalTimes;
        cdTotal = 1;
        cd = 0;
    }

    //是否可用
    public bool isUse()
    {
        return times > 0;
    }

    //使用完技能
    public void useSkill()
    {
        times--;
        if (times <= 0)
        {
            cd = cdTotal;
        }
        if (cd == 0)
        {
            enableSkill();
        }
    }

    //激活技能
    public void enableSkill()
    {
        times = totalTimes;
    }

    public void setRoleControl(RoleControl role)
    {
        this.role = role;
        onEnableRole(role);
    }

    //技能挂载时（已处理）
    public virtual void onEnableRole(RoleControl role)
    {
        
    }

    //回合开始（已处理）
    public virtual void onRoundStart()
    {
        cd--;
        if (cd <= 0)
        {
            enableSkill();
        }
    }

    //选择敌人时（已处理）
    public virtual void onSelectAttackEnemy(RoleControl enemy)
    {
        Debug.Log("Attack Select");
    }

    //攻击前（已处理）
    public virtual void onAttackBefore(RoleControl enemy, bool isBackAttack)
    {
        Debug.Log("Attack Before");
    }

    //攻击后（已处理）
    public virtual void onAttackAfter(RoleControl enemy, float damage)
    {
        Debug.Log("Attack After");
    }

    //受击前（已处理）
    public virtual void onAffectBefore(RoleControl enemy, ref float damage, bool isBackAttack, int damageType)
    {
        Debug.Log("Affect Before");
    }

    //受击后（已处理）
    public virtual void onAffectAfter(RoleControl enemy, float damage)
    {
        Debug.Log("Affect Before");
    }
    
    //棋子在地图上改变、移动、生成、死亡（已处理）
    public virtual void onMapRoleChange(int playerTag)
    {
        Debug.Log("Map Role Change");
    }

    // 回合结束（已处理）
    public virtual void onRoundOver()
    {
        Debug.Log("Round Over");
    }

    //施法动画
    public virtual void fireAnimation()
    {

    }

    //点击技能，第一次
    public virtual void onClickSkill()
    {

    }

    //点击技能，第二次
    public virtual void onClickSkill2()
    {

    }

    //悬浮网格，第一次
    public virtual void onHoverMapGrid(int x, int y)
    {

    }

    //悬浮网格，第二次
    public virtual void onHoverMapGrid2(int x, int y)
    {

    }

    //施放技能
    public virtual void fireRole(int x, int y)
    {
        
    }

    //施放技能
    public virtual void fireRole2(int x, int y)
    {

    }

    //取消技能
    public virtual void cancelSkill()
    {

    }

}