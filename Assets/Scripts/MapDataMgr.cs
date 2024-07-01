

using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class MapDataMgr : MonoBehaviour
{
    public static MapDataMgr Instance;

    public List<GameObject> mapTiles;

    private GridMap mapGrid;
    private GameMap gameMap;

    public Animator wall1;
    public Animator wall2;

    Dictionary<PathNode, int> tempBothList1;
    Dictionary<PathNode, int> tempBothList2;


    private string mapFileName = "mapjson";

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
        //排序
        mapTiles = mapTiles.OrderBy((tile) =>
        {
            return tile.GetComponent<MapTile>().type;
        }).ToList();

        tempBothList1 = new Dictionary<PathNode, int>();
        tempBothList2 = new Dictionary<PathNode, int>();

        OnEvent.Instance.on("onRoundOver", (@e) =>
        {
            var data = @e as Dictionary<string, int>;
            int playerTag = data["tag"];

            Dictionary<PathNode, int> list;

            if (playerTag == 1)
            {
                list = tempBothList1;
            }
            else
            {
                list = tempBothList2;
            }


            foreach (var v in list.ToList())
            {
                if (v.Value > 0)
                {
                    list[v.Key] -= 1;
                }
            }
        });
    }

    public List<GameObject> getTileList()
    {
        return mapTiles;
    }

    //加载地图数据
    public GridMap loadMapData(string fileName)
    {
        if (fileName == null) fileName = mapFileName;
        string json;
        string filepath = Application.streamingAssetsPath + "/" + fileName + ".json";

        if (File.Exists(filepath))
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                json = sr.ReadToEnd();
                sr.Close();
            }

            MapData data = JsonUtility.FromJson<MapData>(json);
            mapGrid = new GridMap(data.width, data.height, data.cellSize);
            data.getData(mapGrid);

            return mapGrid;
        }

        return null;
    }

    //创建空地图数据（用于EditScene）
    public GridMap createGridMap(int width, int height, int cellSize)
    {
        mapGrid = new GridMap(width, height, cellSize);
        return mapGrid;
    }

    public void getXY(Vector3 wPos, out int x, out int y)
    {
        gameMap.getXY(wPos, out x, out y);
    }

    public void getRandomMaskGrid(out int x, out int y)
    {
        mapGrid.getRandomMaskGrid(out x, out y);
    }

    //获取格子的局部位置
    public Vector3 getLocalPosition(int x, int y, float anchorX = 0.5f, float anchorY = 0.5f)
    {
        Vector3 offset = -new Vector3(mapGrid.width, mapGrid.height, 0) * mapGrid.cellSize * 0.5f + new Vector3(anchorX, anchorY) * mapGrid.cellSize;
        Vector3 lpos = new Vector3(x, y, 0) * mapGrid.cellSize + offset;
        return lpos;
    }

    //设置地图是否可通过
    public void setGridIsPass(int x, int y, bool isPass)
    {
        mapGrid.setGridIsPass(x, y, isPass);
    }

    public void setGridIsRole(int x, int y, bool isRole)
    {
        mapGrid.setGridIsRole(x, y, isRole);
    }

    //设置地图对象
    public void setGridMap(GameMap map)
    {
        gameMap = map;
    }

    //获取网格之间的距离
    public int getNodeDistance(int startX, int startY, int endX, int endY)
    {
        return mapGrid.getNodeDistance(mapGrid.getPathNode(startX, startY), mapGrid.getPathNode(endX, endY));
    }

    //寻路算法
    public List<PathNode> findPath(int startX, int startY, int endX, int endY, int size, bool ignorePass)
    {
        return mapGrid.findPath(startX, startY, endX, endY, size, ignorePass);
    }

    //添加出生点
    public void addBothNode(PathNode node, int playerTag, int round)
    {
        if (playerTag == 1){
            tempBothList1[node] = round;
        }
        else
        {
            tempBothList2[node] = round;
        }
    }

    //获取出生节点列表
    public List<PathNode> getBothNode(int playerTag)
    {
        int tag = 201;
        if (playerTag == 2)
        {
            tag = 211;
        }

        List<int> tags = new List<int>() { tag };
        List<PathNode> list = mapGrid.getPathNodeListByType(tags);

        //获取临时出生点
        Dictionary<PathNode, int> temp;
        if (playerTag == 1)
        {
            temp = tempBothList1;
        }
        else
        {
            temp = tempBothList2;
        }

        foreach (var v in temp)
        {
            if (v.Value > 0)
            {
                list.Add(v.Key);
            }
        }

        List<PathNode> res = new List<PathNode>();
        foreach (PathNode pathNode in list)
        {
            if (!pathNode.isRole)
            {
                res.Add(pathNode);
            }
        }
        return res;
    }

    //获取types类型的节点列表
    public List<PathNode> getPathNodeListByType(List<int> types)
    {
        return mapGrid.getPathNodeListByType(types);
    }

    //获取格子的类型
    public int getGridType(int x, int y)
    {
        int type = mapGrid.getValue(x, y);

        return type;
    }

    public Animator getPlayerWallAnimator(int playerTag)  
    {
        if (playerTag == 1)
        {
            return wall1;
        }else if (playerTag == 2)
        {
            return wall2;
        }
        return null;
    }

    public PathNode getPathNode(int x, int y)
    {
        return mapGrid.getPathNode(x, y);
    }

    public List<PathNode> getNeighbourList(PathNode node)
    {
        return mapGrid.getNeighbourList(node);
    }

    public List<PathNode> getRadiusArea(int x, int y, int length)
    {
        return mapGrid.findArea(x, y, length);
    }

    public void showBothArea(int playerTag)
    {
        gameMap.showBothArea(playerTag);
    }

    public void showChangeArea(List<PathNode> list, Color color)
    {
        gameMap.showChangeArea(list, color);
    }

    public Color getAreaColor(int x, int y)
    {
        return gameMap.getAreaColor(x, y);
    }
}


//用于保存与加载地图数据
public class MapData
{
    public int width;
    public int height;
    public int cellSize;

    public int[] data;

    public MapData(int width, int height, int cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        data = new int[width * height];
    }

    public void setData(GridMap grid)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                data[i * height + j] = grid.getValue(i, j);
            }
        }
    }

    public void getData(GridMap grid)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                grid.setValue(i, j, data[i * height + j]);
            }
        }
    }
}
