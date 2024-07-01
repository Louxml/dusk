using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class RoleDataMgr : MonoBehaviour
{
    public ui_mgr uiManager;

    public ui_mgr ui_mgr;

    public static RoleDataMgr Instance;

    //所有角色棋子列表
    public List<GameObject> roleList = new List<GameObject>();

    //角色棋子容器
    public Transform roleContainer;

    //角色棋子预制体
    public GameObject roleControl;

    //玩家一的棋子列表
    public List<RoleControl> roleList1 = new List<RoleControl>();
    //玩家二的棋子列表
    public List<RoleControl> roleList2 = new List<RoleControl>();

    //选择的棋子
    RoleControl overRole;
    public RoleControl selectRole;

    public RoleControl selectplayerMYRole;
    public RoleControl selectplayerYOURRole;


    public int preRoleId;
    public GameObject preBuyRole;

    //数据
    private PlayerData player1;
    private PlayerData player2;

    //上一轮玩家一阵亡的棋子数据列表
    public List<RoleData> roleDeath1 = new List<RoleData>();
    public List<RoleData> roleDeath2 = new List<RoleData>();


    //Destroy
    public List<List<int>> player1CanBuyRoleList = new List<List<int>>();
    public List<List<int>> player2CanBuyRoleList = new List<List<int>>();

    public List<List<int>> player1CanBuyRoleList_extra = new List<List<int>>();
    public List<List<int>> player2CanBuyRoleList_extra = new List<List<int>>();



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        initData();
        addListener();
        addPlayerRole();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // 点击发生在UI元素上，不处理场景中的点击
            if (selectRole != null)
            {
                GetRoleInfoList(selectRole);
            }
            return;
        }

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        int player_type = CombatSystem.Instance.getCurrentPlayerTag();
        MapDataMgr.Instance.getXY(pos, out int x, out int y);

        if (selectRole != null)
        {
            GetRoleInfoList(selectRole);
        }
        if (GameManager.gamestate == 1 || GameManager.gamestate == 5)
        {
            if (GameManager.gamestate == 1)
            {
                GetbuyRoleInfoList();
                selectRole?.unSelectRole();
                selectRole = null;

                selectplayerMYRole?.unSelectRole();
                selectplayerMYRole = null;

                selectplayerYOURRole?.unSelectRole();
                selectplayerYOURRole = null;

                OnEvent.Instance.emit("onHideChangeArea");
                showCan_Buy_Area(player_type);
                if (judge_can_buy_pos(player_type, x, y))
                {
                    if (preRoleId != GameManager.GetPurchasedPrefab() || preBuyRole == null)
                    {
                        get_pre_buyRole(x, y);
                    }
                    else
                    {
                        Vector3 p = MapDataMgr.Instance.getLocalPosition(x, y);
                        preBuyRole.transform.position = p;
                    }

                }
            }
            if (GameManager.gamestate == 5)
            {
                GetbuyRoleInfoList();
                selectRole?.unSelectRole();
                selectRole = null;

                selectplayerMYRole?.unSelectRole();
                selectplayerMYRole = null;

                selectplayerYOURRole?.unSelectRole();
                selectplayerYOURRole = null;

                OnEvent.Instance.emit("onHideChangeArea");
                showCan_Buy_Area(player_type);
                if (judge_can_buy_pos(player_type, x, y))
                {
                    if (preRoleId != GameManager.GetPurchasedPrefab() || preBuyRole == null)
                    {
                        get_pre_buyRole(x, y);
                    }
                    else
                    {
                        Vector3 p = MapDataMgr.Instance.getLocalPosition(x, y);
                        preBuyRole.transform.position = p;
                    }

                }

            }

        }
        else
        {
            OnEvent.Instance.emit("hideBuyRoleArea");
            Destroy(preBuyRole, 0f);
            preRoleId = 0;
        }
       
        if (GameManager.gamestate == 3)
        {
            paotai_state(1);//炮台

            if (Input.GetMouseButtonDown(0))
            {
                Debug.LogError("zuo");
                GameManager.gamestate = 0;
                OnEvent.Instance.emit("hidepaotaoArea");
                selectplayerMYRole.showMoveAndAttackArea();
                return;
            }
            if (Input.GetMouseButtonDown(1))
            {
                Debug.LogError("you");
                int pao_result = paotai_atk(x, y,5,8);
                if (pao_result == 0)
                        { return; }

                CannonController1.Instance.StartAttack();



                GameManager.gamestate = 0;
                OnEvent.Instance.emit("hidepaotaoArea");
                GameManager.Instance.paotai1_cd = 2;
                selectplayerMYRole.showMoveAndAttackArea();
                return;
            }
           
        }
        if (GameManager.gamestate == 4)
        {
            paotai_state(2);//炮台
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.gamestate = 0;
                OnEvent.Instance.emit("hidepaotaoArea");
                selectplayerMYRole.showMoveAndAttackArea();
                return;
            }
            if (Input.GetMouseButtonDown(1))
            {
                int pao_result = paotai_atk(x, y,21,8);
                if (pao_result == 0)
                { return; }

                CannonController2.Instance.StartAttack();

                GameManager.gamestate = 0;
                OnEvent.Instance.emit("hidepaotaoArea");
                GameManager.Instance.paotai2_cd = 2;
                selectplayerMYRole.showMoveAndAttackArea();

                return;
            }
        }


        
       

        if (GameManager.gamestate == 5 && Input.GetMouseButtonDown(0) && judge_can_buy_pos(player_type, x, y))
        {
            if (getRoleControl(x, y) != null)
            {
                GameManager.gamestate = 0;
                return;

            }

            buyRole(x, y);
            GameManager.gamestate = 0;
            return;

        }



        if (GameManager.gamestate == 1 && Input.GetMouseButtonDown(0) && judge_can_buy_pos(player_type, x, y))
        {
            if (getRoleControl(x, y) != null)
            {
                GameManager.gamestate = 0;
                return;

            }

            buyRole(x, y);
            GameManager.gamestate = 0;
            return;

        }




        checkOverRole(x, y);
        if (Input.GetMouseButtonDown(0))
        {
            checkSelectRole(x, y);
            GameManager.gamestate = 0;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            atkSelectRole(x, y);
            GameManager.gamestate = 0;
        }

    }




    void addListener()
    {
        OnEvent.Instance.on("onRoundOver", (@e) =>
        {
            var data = @e as Dictionary<string, int>;
            int playerTag = data["tag"];

            if (playerTag == 1)
            {
                roleDeath1.Clear();
            }
            else if (playerTag == 2)
            {
                roleDeath2.Clear();
            }
        });
    }
    public int paotai_atk(int x,int y,int px,int py)
    {
       
            if (x < 0 || y < 0) return 0;
            RoleControl role = getRoleControl(x, y);
            if (selectplayerMYRole == null)
            {
                return 0;
            }
            if (role == null)
            {
                return 0;
            }
            int tag = role.getRoleTag();

            int playerTag = CombatSystem.Instance.getCurrentPlayerTag();
            if (tag != playerTag)
            {
                CombatSystem.Instance.attack_by_paotai(role,px,py);
            return 1;
        }
        return 0;
        
    }


    public void paotai_state(int state)
    {
        Dictionary<string, int> data = new Dictionary<string, int>();
       
        if (state == 1)
        {
            data["x"] = 5;
            data["y"] = 8;
            data["attack"] = 5;
        }
        else
        {
            data["x"] = 21;
            data["y"] = 8;
            data["attack"] = 5;
        }
        OnEvent.Instance.emit("onShowpaotaiArea", data);
    }

    //鼠标悬浮地图位置
    void checkOverRole(int x, int y)
    {
        RoleControl role = getRoleControl(x, y); // 获取当前位置的棋子
        if (role != null)
        {
            GetRoleInfoList(role);
        }
        if (role == null && selectRole == null && GameManager.gamestate != 1)
        {
            ClearRoleInfoList();

        }
        // 如果有一个新的棋子被鼠标悬停
        if (overRole != role)
        {
            // 如果之前有棋子被悬停且不是选中的棋子，取消高亮
            if (overRole != null &&
  (selectplayerMYRole != null ? overRole != selectplayerMYRole : true) &&
  (selectplayerYOURRole != null ? overRole != selectplayerYOURRole : true))
            {
                overRole.unSelectRole();
            }

            // 更新当前悬停的棋子
            overRole = role;

            // 如果当前有棋子被悬停，高亮棋子
            if (overRole != null)
            {
                //todo
                int player_type = CombatSystem.Instance.getCurrentPlayerTag();

                if (player_type == overRole.roleModel.roleData.tag)
                {
                    overRole.selectRole();
                }
                else
                {
                    overRole.selectRoleRed();
                }
                //overRole.selectRole();
                //overRole.selectRoleRed();
            }
        }
        // 如果鼠标移动到没有棋子的地方，且之前悬停的棋子不是选中的棋子
        else if (role == null && overRole != null && overRole != selectRole)
        {
            // 取消之前悬停棋子的高亮
            overRole.unSelectRole();
            overRole = null;
        }
    }

    void checkSelectRole(int x, int y)
    {
        if (x < 0 || y < 0) return;
        RoleControl role = getRoleControl(x, y);
        if (role != null)
        {
            selectRole = role;
            int tag = role.getRoleTag();

            int playerTag = CombatSystem.Instance.getCurrentPlayerTag();

            if (tag == playerTag)
            {

                selectplayerMYRole?.unSelectRole();
                if (selectplayerMYRole != null)
                {
                    selectplayerMYRole.clear_mianban();
                }
                //选择其他棋子
                selectplayerMYRole = role;
                selectplayerMYRole.selectRole();
                selectplayerMYRole.get_mianban();
                OnEvent.Instance.emit("onHideChangeArea");
                selectplayerMYRole.showMoveAndAttackArea();
                GameManager.gamestate = 0;

            }
            else
            {
                selectplayerYOURRole?.unSelectRole();
                if (selectplayerYOURRole != null)
                {
                    selectplayerYOURRole.clear_mianban();
                }
                selectplayerYOURRole = role;
                selectplayerYOURRole.selectRoleRed();
                selectplayerYOURRole.get_mianban();
            }
        }
        else
        {
            if (selectplayerMYRole != null)
            {
                bool isMove = CombatSystem.Instance.roleMove(selectplayerMYRole, x, y);
                if (isMove)
                {
                    // TODO 可优化为事件通知
                    //隐藏地图的区域显示
                    OnEvent.Instance.emit("onHideChangeArea");
                    CombatSystem.Instance.add_time();
                }
                else
                {
                    OnEvent.Instance.emit("onHideChangeArea");
                    selectplayerMYRole?.unSelectRole();
                    selectplayerYOURRole?.unSelectRole();
                    selectRole?.unSelectRole();
                    selectplayerYOURRole?.unSelectRole();
                    if (selectplayerYOURRole != null)
                    {
                        selectplayerYOURRole.clear_mianban();
                    }
                    selectplayerMYRole?.unSelectRole();
                    if (selectplayerMYRole != null)
                    {
                        selectplayerMYRole.clear_mianban();
                    }
                    selectplayerMYRole = null;
                    selectplayerYOURRole = null;
                    selectRole = null;
                }
            }
            else
            {
                OnEvent.Instance.emit("onHideChangeArea");
                selectplayerMYRole?.unSelectRole();
                selectplayerYOURRole?.unSelectRole();
                selectRole?.unSelectRole();
                if (selectplayerYOURRole != null)
                {
                    selectplayerYOURRole.clear_mianban();
                }
                selectplayerMYRole?.unSelectRole();
                if (selectplayerMYRole != null)
                {
                    selectplayerMYRole.clear_mianban();
                }
                selectplayerMYRole = null;
                selectplayerYOURRole = null;
                selectRole = null;
            }
            GameManager.gamestate = 0;

        }


    }


    void atkSelectRole(int x, int y)
    {
        if (x < 0 || y < 0) return;
        RoleControl role = getRoleControl(x, y);
        if (selectplayerMYRole == null)
        {
            return;
        }
        if (role == null)
        {
            return;
        }
        int tag = role.getRoleTag();

        int playerTag = CombatSystem.Instance.getCurrentPlayerTag();
        if (tag != playerTag)
        {
            bool isAttack = CombatSystem.Instance.roleAttack(selectplayerMYRole, role, false, () =>
            {
                if (selectplayerMYRole.isLife())
                {
                    selectplayerMYRole.showMoveAndAttackArea();
                }
                else
                {
                    selectplayerMYRole = null;
                }
            });
            if (isAttack)
            {
                selectplayerMYRole.roleSelectEnemy(role);
                OnEvent.Instance.emit("onHideChangeArea");
                CombatSystem.Instance.add_time();
            }
        }




    }


    //初始化玩家棋子数据
    void initData()
    {
        player1 = GameDataMgr.Instance.getPlayerData(1);

        player2 = GameDataMgr.Instance.getPlayerData(2);
    }

    //创建角色棋子模型
    GameObject createRoleObject(int id)
    {
        foreach (var item in roleList)
        {
            var roleModel = item.GetComponent<RoleModel>();
            if (roleModel.roleConfig.roleID == id)
            {
                return Instantiate(item);
            }
        }
        return null;
    }

    //用于测试
    public int getRandomRoleId()
    {
        int index = Random.Range(0, roleList.Count);
        return roleList[index].GetComponent<RoleModel>().roleConfig.roleID;
    }

    //创建角色棋子
    private GameObject createRole(RoleData data)
    {
        GameObject role = Instantiate(roleControl);
        GameObject roleObject = createRoleObject(data.id);
        roleObject.name = "role";
        roleObject.transform.SetParent(role.transform, false);

        //设置数据
        RoleModel roleModel = roleObject.GetComponent<RoleModel>();
        roleModel.setRoleData(data);

        return role;
    }

    //添加角色棋子
    private void addPlayerRole()
    {
        PlayerData playerData1 = player1;
        foreach (RoleData item in playerData1.roleList)
        {
            placeRole(item);
        }

        PlayerData playerData2 = player2;
        foreach (RoleData item in playerData2.roleList)
        {
            placeRole(item);
        }
    }

    //获取位置上的棋子
    public RoleControl getRoleControl(int x, int y)
    {
        if (x < 0 || y < 0) return null;
        foreach (var role in roleList1)
        {
            if (role.isRole(x, y))
            {
                return role;
            }
        }

        foreach (var role in roleList2)
        {
            if (role.isRole(x, y))
            {
                return role;
            }
        }

        return null;
    }

    //放置棋子
    public void placeRole(RoleData roleData)
    {
        GameObject role = createRole(roleData);
        Vector3 pos = MapDataMgr.Instance.getLocalPosition(roleData.x, roleData.y);
        role.transform.position = pos;
        role.transform.SetParent(roleContainer, false);

        RoleControl roleControl = role.GetComponent<RoleControl>();

        roleControl.setXY(roleData.x, roleData.y);

        //添加棋子
        int playerTag = roleData.tag;
        if (playerTag == 1)
        {
            roleList1.Add(roleControl);
        }
        else
        {
            roleList2.Add(roleControl);
        }

        Dictionary<string, int> data = new Dictionary<string, int>();
        data["tag"] = roleControl.getRoleTag();
        OnEvent.Instance.emit("onRoleMapChange", data);
    }

    public void removeRole(RoleControl role)
    {
        int playerTag = role.getRoleTag();
        if (playerTag == 1)
        {
            roleList1.Remove(role);
        }
        else if (playerTag == 2)
        {
            roleList2.Remove(role);
        }
    }
  
    //购买棋子
    public void buyRole(int x, int y)
    {
        int role_cost = GameManager.GetBuyRoleCost();

        CombatSystem.Instance.add_time();
        int role_id = GameManager.GetPurchasedPrefab();
        RoleData role = createRoleData(role_id);
        role.x = x;
        role.y = y;
        int player_type = CombatSystem.Instance.getCurrentPlayerTag();
        PlayerData player = GameDataMgr.Instance.getPlayerData(player_type);

        if (player_type == 1)
        {
            GameDataMgr.Instance.player1.gold -= role_cost;
        }
        else
        {
            GameDataMgr.Instance.player2.gold -= role_cost;
        }

        player.addRole(role);

        placeRole(role);

    }

    public void addDeathRole(RoleData role, int playerTag)
    {
        if (playerTag == 1)
        {
            roleDeath1.Add(role);
        }
        else if (playerTag == 2)
        {
            roleDeath2.Add(role);
        }
    }

    public List<RoleData> getDeathRoleList(int playerTag)
    {
        if (playerTag == 1)
        {
            return roleDeath1;
        }else if (playerTag == 2)
        {
            return roleDeath2;
        }

        return null;
    }

    public RoleData createRoleData(int id)
    {
        RoleData role = new RoleData(id);

        //技能分配
        if (id == 10)   //圣光法师（人族）
        {
            role.skills.Add(new Skill1());
            role.skills.Add(new Skill47());
        }
        else if (id == 32)  //烈弓（魔族）
        {
            role.skills.Add(new Skill2());
            role.skills.Add(new Skill4());
        }
        else if (id == 17) // 屠夫（亡灵）
        {
            role.skills.Add(new Skill3());
            role.skills.Add(new Skill5());
        }
        else if (id == 3)   // 矛兵（人族）
        {
            role.skills.Add(new Skill2());
            role.skills.Add(new Skill6());
        }
        else if (id == 33)  // 行刑者（魔族）
        {
            role.skills.Add(new Skill7());
            role.skills.Add(new Skill8());
            role.skills.Add(new Skill57());
        }
        else if (id == 18)  // 亡灵法师（亡灵）
        {
            role.skills.Add(new Skill9());
            role.skills.Add(new Skill3());
        }
        else if (id == 4)   // 御卫方阵（亡灵）
        {
            role.skills.Add(new Skill10());
        }
        else if (id == 35)  // 翼魔（魔族）
        {
            role.skills.Add(new Skill11());
            role.skills.Add(new Skill14());
            role.skills.Add(new Skill15());
        }
        else if (id == 21)  // 幽魂(亡灵)
        {
            role.skills.Add(new Skill11());
            role.skills.Add(new Skill12());
            role.skills.Add(new Skill13());
        }
        else if (id == 5)   // 骑兵（人族）
        {
            role.skills.Add(new Skill16());
        }
        else if (id == 39)  // 地狱行者（魔族）
        {
            role.skills.Add(new Skill17());
            role.skills.Add(new Skill18());
            role.skills.Add(new Skill19());
        }
        else if (id == 20)  // 巨型僵尸（亡灵）
        {
            role.skills.Add(new Skill20());
            role.skills.Add(new Skill21());
        }
        else if (id == 6)   // 重剑武士（人族）
        {
            role.skills.Add(new Skill17());
            role.skills.Add(new Skill22());
            role.skills.Add(new Skill23());
        }
        else if (id == 37)  // 狱炎（魔族）
        {
            role.skills.Add(new Skill2());
            role.skills.Add(new Skill58());
        }
        else if (id == 22)  // 吸血鬼（亡灵）
        {
            role.skills.Add(new Skill14());
            role.skills.Add(new Skill24());
        }
        else if (id == 7)   // 狂战士（人族）
        {
            role.skills.Add(new Skill25());
            role.skills.Add(new Skill26());
        }
        else if (id == 34)  // 瘴盾（魔族）
        {
            role.skills.Add(new Skill59());
        }
        else if (id == 19)  // 厉鬼（亡灵）
        {
            role.skills.Add(new Skill16());
            role.skills.Add(new Skill26());
            role.skills.Add(new Skill27());
            role.skills.Add(new Skill48());
        }
        else if (id == 8)   // 守望者（人族）
        {
            role.skills.Add(new Skill26());
            role.skills.Add(new Skill32());
            role.skills.Add(new Skill48());
        }
        else if (id == 38)  // 悍魔（魔族）
        {
            role.skills.Add(new Skill26());
            role.skills.Add(new Skill33());
            role.skills.Add(new Skill48());
        }
        else if (id == 23)  // 巫妖（亡灵）
        {
            role.skills.Add(new Skill2());
            role.skills.Add(new Skill9());
        }
        else if (id == 40)  // 双生子（魔族）
        {
            role.skills.Add(new Skill14());
            role.skills.Add(new Skill28());
            role.skills.Add(new Skill29());
        }
        else if (id == 24)  // 狂灵（亡灵）
        {
            role.skills.Add(new Skill29());
            role.skills.Add(new Skill11());
            role.skills.Add(new Skill26());
            role.skills.Add(new Skill14());
            role.skills.Add(new Skill12());
            role.skills.Add(new Skill30());
            role.skills.Add(new Skill48());

        }
        else if ( id == 9)  // 修女（人族）
        {
            role.skills.Add(new Skill49());
        }
        else if (id == 41)  // 噬魔（魔族）
        {
            role.skills.Add(new Skill14());
            role.skills.Add(new Skill2());
            role.skills.Add(new Skill60());
        }
        else if (id == 25)  // 阴影（亡灵）
        {
            role.skills.Add(new Skill14());
            role.skills.Add(new Skill30());
        }
        else if (id == 11)  // 圣教军（人族）
        {
            role.skills.Add(new Skill31());
            role.skills.Add(new Skill48());
            role.skills.Add(new Skill50());
        }
        else if (id == 42)  // 军士（魔族）
        {
            // TODO 每个不同敌方单位都可攻击（不受次数限制）、防御不可无视不可降低
            role.skills.Add(new Skill26());
            role.skills.Add(new Skill31());
            role.skills.Add(new Skill48());
        }
        else if (id == 26)  // 骨龙（亡灵）
        {
            role.skills.Add(new Skill25());
            role.skills.Add(new Skill2());
            role.skills.Add(new Skill14());
            role.skills.Add(new Skill48());
            role.skills.Add(new Skill66());
        }
        else if (id == 12) // 黑色卫队（人族）
        {
            // TODO 跃杀（不好弄，复杂）
            role.skills.Add(new Skill34());
            role.skills.Add(new Skill26());
        }
        else if (id == 36)  // 怒兽（魔族）
        {
            role.skills.Add(new Skill25());
            role.skills.Add(new Skill35());
        }
        else if (id == 27)  // 亡灵君主（亡灵）
        {
            role.skills.Add(new Skill36());
            role.skills.Add(new Skill9());
            role.skills.Add(new Skill26());
            role.skills.Add(new Skill37());
            role.skills.Add(new Skill48());
            role.skills.Add(new Skill67());
        }
        else if (id == 13)  //  神之子（人族）
        {
            role.skills.Add(new Skill40());
            role.skills.Add(new Skill51());
            role.skills.Add(new Skill52());
        }
        else if (id == 15)  // 圣光导师（人族）
        {
            role.skills.Add(new Skill38());
            role.skills.Add(new Skill53());
            role.skills.Add(new Skill54());
            role.skills.Add(new Skill55());
        }
        else if (id == 14)  // 光灵（人族）
        {
            // TODO 光标
            role.skills.Add(new Skill2());
            role.skills.Add(new Skill23());
            role.skills.Add(new Skill39());
            role.skills.Add(new Skill56());
        }
        else if (id == 43)  // 地狱武士（魔族）
        {
            role.skills.Add(new Skill41());
            role.skills.Add(new Skill61());
        }
        else if (id == 44)  // 湮灭使徒（魔族）
        {
            // TODO 低语
            role.skills.Add(new Skill42());
            role.skills.Add(new Skill62());
            role.skills.Add(new Skill63());
        }
        else if (id == 45)  // 湮灭刺客（魔族）
        {
            role.skills.Add(new Skill43());
            role.skills.Add(new Skill14());
            role.skills.Add(new Skill64());
            role.skills.Add(new Skill65());
        }
        else if (id == 28)  // 吸血鬼女王（亡灵）
        {
            role.skills.Add(new Skill19());
            role.skills.Add(new Skill44());
            role.skills.Add(new Skill45());
            role.skills.Add(new Skill46());
        }
        else if (id == 29)  // 无头骑士（亡灵）
        {

        }
        else if (id == 30)  //低语者（亡灵）
        {
            // TODO 灵魂抽取、灵魂剥离
            role.skills.Add(new Skill9());
            role.skills.Add(new Skill2());
            role.skills.Add(new Skill68());
        }

        return role;
    }

    //预放置棋子
    public void get_pre_buyRole(int x, int y)
    {
        int role_cost = GameManager.GetBuyRoleCost();

        preRoleId = GameManager.GetPurchasedPrefab();
        RoleData role = createRoleData(preRoleId);
        role.x = x;
        role.y = y;
        int player_type = CombatSystem.Instance.getCurrentPlayerTag();

        GameObject roleObj = createRole(role);
        Vector3 pos = MapDataMgr.Instance.getLocalPosition(role.x, role.y);
        roleObj.transform.position = pos;
        roleObj.transform.SetParent(roleContainer, false);

        preBuyRole = roleObj;

        Transform roleObjectTransform = roleObj.transform.Find("role");
        if (roleObjectTransform != null)
        {

            GameObject roleObject = roleObjectTransform.gameObject;

            SpriteRenderer spriteRenderer = roleObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // 获取材质的当前颜色
                Color color = spriteRenderer.color;

                // 修改颜色的Alpha分量以设置全透明
                color.a = 0.5f; // 设置Alpha为0，即全透明//todo

                // 应用新的颜色到材质
                spriteRenderer.color = color;
            }


        }
        else
        {
            Debug.LogError("roleObject not found as a child of the role GameObject.");
        }


    }



    public List<RoleControl> getRoleList(int playerTag)
    {
        if (playerTag == 1)
        {
            return roleList1;
        }
        else if (playerTag == 2)
        {
            return roleList2;
        }

        return null;
    }



    public bool judge_can_buy_pos(int player_tag, int x, int y)
    {
        List<PathNode> list = MapDataMgr.Instance.getBothNode(player_tag);
        foreach (PathNode node in list)
        {
            if (node.x == x)
            {
                if (node.y == y)
                {
                    return true;
                }
            }
        }
        return false;


    }


    public void GetRoleInfoList(RoleControl roleControl)
    {
        RoleModel indexRole = roleControl.roleModel;
        List<float> infoList = new List<float>();
        infoList.Add(indexRole.roleData.hp);
        infoList.Add(indexRole.roleConfig.hp);
        infoList.Add(indexRole.getDef());
        infoList.Add(indexRole.getAtk());
        infoList.Add(indexRole.getAttackTimes());
        infoList.Add(indexRole.getMoveDistance());
        infoList.Add(indexRole.getMoveTimes());
        infoList.Add(indexRole.roleConfig.range);


        uiManager.update_role_info(infoList, indexRole.roleData.id, indexRole.roleData.x, indexRole.roleData.y, roleControl.roleModel.roleData.tag, roleControl);
        //ui_mgr.updateRoleSkillList(roleControl);
    }

    public void ClearRoleInfoList()
    {


        uiManager.clear_role_info();

    }

    public void GetbuyRoleInfoList()
    {

        preRoleId = GameManager.GetPurchasedPrefab();
        Debug.Log(preRoleId);

        GameObject rolepre = roleList[preRoleId-1];
        int hp;
        hp = rolepre.GetComponent<RoleModel>().roleConfig.hp;


        List<float> infoList = new List<float>();
        infoList.Add(rolepre.GetComponent<RoleModel>().roleConfig.hp);
        infoList.Add(rolepre.GetComponent<RoleModel>().roleConfig.hp);
        infoList.Add(rolepre.GetComponent<RoleModel>().roleConfig.def);
        infoList.Add(rolepre.GetComponent<RoleModel>().roleConfig.atk);
        infoList.Add(rolepre.GetComponent<RoleModel>().roleConfig.attackTimes);
        infoList.Add(rolepre.GetComponent<RoleModel>().roleConfig.move);
        infoList.Add(rolepre.GetComponent<RoleModel>().roleConfig.moveTimes);
        infoList.Add(rolepre.GetComponent<RoleModel>().roleConfig.range);

        uiManager.update_role_info(infoList, preRoleId, -1, -1,0);
    }

    //显示可以买棋子的区域
    public void showCan_Buy_Area(int player_type)
    {
        List<List<int>> combinedList = new List<List<int>>();
        Dictionary<string, List<List<int>>> data = new Dictionary<string, List<List<int>>>();
        List<PathNode> list = MapDataMgr.Instance.getBothNode(player_type);
        foreach (PathNode node in list)
        {
            combinedList.Add(new List<int> { node.x, node.y });
        }
        data["can_buy_list"] = combinedList;

        OnEvent.Instance.emit("onShowCanBuyArea", data);

    }



}
