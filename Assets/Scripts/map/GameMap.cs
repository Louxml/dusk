
using System.Collections.Generic;
using UnityEngine;


public class GameMap : MonoBehaviour
{
   
    // 地图网格数据
    private GridMap grid;

    // 地图瓦片类型列表
    private List<GameObject> mapTile;

    // 地图背景
    public Transform bg;
    // 网格容器
    public Transform gridContainer;
    // tile对象列表
    GameObject[,] tileObjects;

    //区域容器
    public Transform areaContainer;
    // 区域预制体
    public GameObject areaUnit;
    // 移动区域颜色
    public Color moveColor;
    // 攻击区域颜色
    public Color attackColor;
    // 买棋子区域颜色
    public Color buyColor;

    public Color pColor;
    // 所有区域对象列表
    SpriteRenderer[,] areaObjects;
    // 显示的区域列表
    List<PathNode> changeArea = new List<PathNode>();

    //玩家一的城墙
    List<PathNode> wallList1;

    //玩家二的城墙
    List<PathNode> wallList2;

    public List<List<int>> playerCanBuyRoleShowList = new List<List<int>>();
    public List<List<int>>  playerpaotaiRoleShowList = new List<List<int>>();


    private void Awake()
    {
        loadMapData();
        MapDataMgr.Instance.setGridMap(this);
    }
    void Start()
    { 
        mapTile = MapDataMgr.Instance.getTileList();

        createMap();
        
        // 监听显示移动和攻击区域
        OnEvent.Instance.on("onShowMoveAndAttackArea", (object @e) =>
        {
            var data = @e as Dictionary<string, int>;
            int x = data["x"];
            int y = data["y"];
            int move = data["move"];
            int attack = data["attack"];
            int size = data["size"];
            bool ignorePass = data["ignorePass"] == 1;

            showMoveAndAttackArea(x, y, size, move, attack, ignorePass);
        });

        // 监听显示可以买棋子的区域
        OnEvent.Instance.on("onShowCanBuyArea", (object @e) =>
        {
            var data = @e as Dictionary<string, List<List<int>>>;
            List<List<int>> can_buy_list = data["can_buy_list"];
            highlightArea(can_buy_list);
        });


        OnEvent.Instance.on("onShowpaotaiArea", (object @e) =>
        {
            var data = @e as Dictionary<string, int>;
            int px = data["x"];
            int py = data["y"];
            int pattack = data["attack"];
            highlightpaotaiArea(px,py, pattack);
        });


        OnEvent.Instance.on("hideBuyRoleArea", (object @e) =>
        {
            hideBuyRoleArea();
        });
        

            OnEvent.Instance.on("hidepaotaoArea", (object @e) =>
            {
                hidepaotaiArea();
            });

        OnEvent.Instance.on("onHideChangeArea", (object @e) =>
        {
            hideChangeArea();
        });

        OnEvent.Instance.on("onRoundStart", (object @e) =>
        {
            var data = @e as Dictionary<string, int>;
            int tag = data["tag"];

            //城墙设置通行状态
            foreach (PathNode node in wallList1)
            {
                node.isPass = (tag == 1);
            }

            foreach (PathNode node in wallList2)
            {
                node.isPass = (tag == 2);
                //node.isPass = true;
            }
        });
    }

    void Update()
    {
        
    }

    public void getXY(Vector3 worldPosition, out int x, out int y)
    {
        Vector3 lpos = transform.InverseTransformPoint(worldPosition);
        Vector3 offset = -new Vector3(grid.width, grid.height, 0) * grid.cellSize * 0.5f;
        lpos -= offset;
        x = Mathf.FloorToInt(lpos.x / grid.cellSize);
        y = Mathf.FloorToInt(lpos.y / grid.cellSize);
        x = (x < 0 || x >= grid.width) ? -1 : x;
        y = (y  < 0 || y >= grid.height) ? -1 : y;
    }

    //加载地图数据文件
    private void loadMapData()
    {
        grid = MapDataMgr.Instance.loadMapData(null);

        if (grid == null)
        {
            Debug.Log("地图数据文件缺失");
        }

        wallList1 = grid.getPathNodeListByType(new List<int>() { 301 });

        wallList2 = grid.getPathNodeListByType(new List<int>() { 311 });
    }

    //获取mapTile
    GameObject getMapTile(int type)
    {
        for (int i = 0; i < mapTile.Count; i++)
        {
            var tile = mapTile[i].GetComponent<MapTile>();
            if (tile.type == type)
            {
                return mapTile[i];
            }
        }
        return null;
    }
    //创建地图
    private void createMap()
    {
        tileObjects = new GameObject[grid.width, grid.height];
        areaObjects = new SpriteRenderer[grid.width, grid.height];
        bg.localScale = new Vector3(grid.width, grid.height, 1);
        for (int i = 0; i < grid.width; i++)
        { 
            for (int j = 0; j < grid.height; j++)
            {
                int value = grid.getValue(i, j);
                Vector3 pos = MapDataMgr.Instance.getLocalPosition(i, j);
                GameObject obj = getMapTile(value);

                GameObject tile = Instantiate(obj);
                Transform tileTransform = tile.transform;
                tileTransform.SetParent(gridContainer, false);
                tileTransform.localPosition = pos;

                tileObjects[i, j] = tile;

                GameObject area = Instantiate(areaUnit);
                Transform areaTransform = area.transform;
                areaTransform.SetParent(areaContainer, false);
                areaTransform.localPosition = pos;
                areaObjects[i, j] = area.GetComponent<SpriteRenderer>();
                areaObjects[i, j].color = new UnityEngine.Color(1f, 1f, 1f, 0f);
            }
        }
    }

    // 显示攻击和移动区域
    public void showMoveAndAttackArea(int x, int y, int size, int move, int attack, bool ignorePass)
    {
        List<PathNode> list = grid.findIsPassArea(x, y, size, move, ignorePass);
        foreach (PathNode node in list)
        {
            if (!changeArea.Contains(node))
            {
                changeArea.Add(node);
            }
            SpriteRenderer area = areaObjects[node.x, node.y];
            area.color = moveColor;
        }

        list = grid.findArea(x, y, attack);
        foreach (PathNode node in list)
        {
            if (!changeArea.Contains(node))
            {
                changeArea.Add(node);
            }
            SpriteRenderer area = areaObjects[node.x, node.y];
            area.color = attackColor;
        }
    }


    // 显示可以买棋子的位置
    public void highlightArea(List<List<int>> coordinates)
    {
        foreach (List<int> coordinate in coordinates)
        {
            int x = coordinate[0];
            int y = coordinate[1];

            // 检查坐标是否在区域内部，以防越界
            if (x >= 0 && y >= 0 && x < areaObjects.GetLength(0) && y < areaObjects.GetLength(1))
            {
                SpriteRenderer area = areaObjects[x, y];
                area.color = buyColor; // 假设这里 Color.blue 是一个已定义的蓝色
                playerCanBuyRoleShowList.Add(new List<int> { x, y });
            }
        }
    }

    public void highlightpaotaiArea(int x,int y,int attack)
    {
  
        List<PathNode> list = grid.findArea(x, y, attack);
        foreach (PathNode node in list)
        {
            SpriteRenderer area = areaObjects[node.x, node.y];
            area.color = pColor;
            playerpaotaiRoleShowList.Add(new List<int> { node.x ,node.y });

        }
        
    }


    // 隐藏买棋子显示区域
    public void hideBuyRoleArea()
    {
        foreach (List<int> point in playerCanBuyRoleShowList)
        {
           
            SpriteRenderer area = areaObjects[point[0], point[1]];
            area.color = new UnityEngine.Color(1f, 1f, 1f, 0f);

        }

    }

    public void hidepaotaiArea()
    {
        Debug.LogError("xiaochule");
        foreach (List<int> point in playerpaotaiRoleShowList)
        {

            SpriteRenderer area = areaObjects[point[0], point[1]];
            area.color = new UnityEngine.Color(1f, 1f, 1f, 0f);

        }
        


    }



    public void showBothArea(int playerTag)
    {
        List<PathNode> list = MapDataMgr.Instance.getBothNode(playerTag);
        foreach (PathNode node in list)
        {

            Debug.Log(playerTag + " Both:" + node.x + "," + node.y);
            //if (!changeArea.Contains(node))
            //{
            //    changeArea.Add(node);
            //}
            //SpriteRenderer area = areaObjects[node.x, node.y];
            //area.color = moveColor;
        }
    }

    // 隐藏所有显示区域
    public void hideChangeArea()
    {
        foreach (PathNode node in changeArea)
        {
            SpriteRenderer area = areaObjects[node.x, node.y];
            area.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void showChangeArea(List<PathNode> list, Color color)
    {
        foreach (PathNode node in list)
        {
            if (!changeArea.Contains(node))
            {
                changeArea.Add(node);
            }
            SpriteRenderer area = areaObjects[node.x, node.y];
            area.color = color;
        }
    }

    public Color getAreaColor(int x, int y)
    {
        SpriteRenderer area = areaObjects[x, y];
        return area.color;
    }
}
