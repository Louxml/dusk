using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridMap
{
    public int width;
    public int height;
    public int cellSize;
    Tile[,] data;
    //用于路径搜索
    PathNode[,] mapNode;

    public GridMap(int width, int height, int cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        data = new Tile[width, height];

        mapNode = new PathNode[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                data[i, j] = new Tile(0);
                mapNode[i, j] = new PathNode(i, j);
            }
        }
    }

    //获取位置的地图数据
    public int getValue(int x, int y)
    {
        if (x < 0 || x >= width) return -1;
        if (y < 0 || y >= height) return -1;
        return data[x, y].getType();
    }

    //设置位置的地图数据
    public void setValue(int x, int y, int value)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        data[x, y].setType(value);
        mapNode[x, y].isPass = data[x, y].getIsPass();
    }

    //获得随机空格子(测试用的)
    public void getRandomMaskGrid(out int x, out int y)
    {
        List<PathNode> list = new List<PathNode>();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                PathNode node1 = mapNode[i, j];
                if ((data[i, j].getMask() & 1) != 0 && node1.isPass && !node1.isRole){
                    list.Add(node1);
                }
            }
        }

        PathNode node = list[UnityEngine.Random.Range(0, list.Count)];
        x = node.x;
        y = node.y;
    }

    //设置位置是否通过
    public void setGridIsPass(int x, int y, bool isPass)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        mapNode[x, y].isPass = isPass;
    }

    public void setGridIsRole(int x, int y, bool isRole)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        mapNode[x, y].isRole = isRole;
    }

    // AStar 最短路径查询
    public List<PathNode> findPath(int startX, int startY, int endX, int endY, int size, bool ignorePass)
    {
        List<PathNode> res = new List<PathNode>();
        if ((startX < 0) || (startX >= width) || (startY < 0) || (startY >= height)) return res;
        if ((endX < 0) || (endX >= width) || (endY < 0) || (endY >= height)) return res;
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closeList = new List<PathNode>();

        PathNode startNode = mapNode[startX, startY];
        PathNode endNode = mapNode[endX, endY];

        if (endNode.isRole) return res;

        openList.Add(startNode);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                PathNode node = mapNode[i, j];
                node.gCost = int.MaxValue;
                node.hCost = 0;
                node.calcFCost();
                node.fromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = getNodeDistance(startNode, endNode);
        startNode.calcFCost();
        while (openList.Count > 0)
        {
            PathNode currentNode = openList[0];

            if (currentNode == endNode)
            {
                return calcPath(currentNode);
            }

            openList.Remove(currentNode);
            closeList.Add(currentNode);

            foreach (PathNode node in getNeighbourList(currentNode))
            {
                if (closeList.Contains(node) || (!ignorePass && !isNodePass(node, size))) continue;
                int gCost = currentNode.gCost + getNodeDistance(currentNode, node);

                if (gCost < node.gCost)
                {
                    node.gCost = gCost;
                    node.hCost = getNodeDistance(node, endNode);
                    node.calcFCost();
                    node.fromNode = currentNode;

                    if (!openList.Contains(node))
                    {
                        openList.Add(node);
                    }
                }
            }
            openList.Sort();
        }

        return res;
    }

    bool isNodePass(PathNode node, int size)
    {
        bool isPass = true;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                //TODO判断长度
                if (node.x + i >= width || node.y + j >= height) return false;
                PathNode pn = mapNode[node.x +  i, node.y + j];
                if (!pn.isPass) return false;
            }
        }
        return isPass;
    }

    //计算最短路径
    List<PathNode> calcPath(PathNode node)
    {
        List<PathNode> list = new List<PathNode>();

        while (node != null)
        {
            list.Add(node);
            node = node.fromNode;
        }


        list.Reverse();

        return list;
    }
    //获取周围格子
    public List<PathNode> getNeighbourList(PathNode node)
    {
        List<PathNode> list = new List<PathNode>();
        if (node.y + 1 < height) list.Add(mapNode[node.x, node.y + 1]);
        if (node.y - 1 >= 0) list.Add(mapNode[node.x, node.y - 1]);
        if (node.x - 1 >= 0) list.Add(mapNode[node.x - 1, node.y]);
        if (node.x + 1 < width) list.Add(mapNode[node.x + 1, node.y]);

        return list;
    }
    //计算欧几里得距离
    public int getNodeDistance(PathNode startNode, PathNode endNode)
    {
        //单倍距离
        int distanceUnit = 1;
        return (Mathf.Abs(startNode.x - endNode.x) + Mathf.Abs(startNode.y - endNode.y)) * distanceUnit;
    }
    
    //查找可移动的区域
    public List<PathNode> findIsPassArea(int x, int y, int size, int move, bool ignorePass)
    {
        int startX = x - move;
        int startY = y - move;
        int endX = x + move;
        int endY = y + move;
        startX = Mathf.Max(startX, 0);
        startY = Mathf.Max(startY, 0);
        endX = Mathf.Min(endX, width - 1);
        endY = Mathf.Min(endY, height - 1);

        List<PathNode> list = new List<PathNode>();
        for (int i = startX; i <= endX; i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                if (x == i && y == j) continue;
                List<PathNode> path = findPath(x, y, i, j, size, ignorePass);

                int length = path.Count - 1;
                if (length > 0 && length <= move)
                {
                    for (int n = 0; n < size; n++)
                    {
                        for (int m = 0; m < size; m++)
                        {
                            PathNode pn = mapNode[i + n, j + m];
                            if (!list.Contains(pn))
                            {
                                list.Add(pn);
                            }
                        }
                    }
                    
                }
            }
        }

        return list;
    }

    //获得半径内的区域
    public List<PathNode> findArea(int x, int y, int length)
    {
        int startX = x - length;
        int startY = y - length;
        int endX = x + length;
        int endY = y + length;
        startX = Mathf.Max(startX, 0);
        startY = Mathf.Max(startY, 0);
        endX = Mathf.Min(endX, width - 1);
        endY = Mathf.Min(endY, height - 1);

        List<PathNode> list = new List<PathNode>();
        for (int i = startX; i <= endX; i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                if (x == i && y == j) continue;

                int distance = Math.Abs(i - x) + Math.Abs(j - y);
                if (distance <= length)
                {
                    list.Add(mapNode[i, j]);
                }
            }
        }

        return list;
    }

    //获取地图节点
    public PathNode getPathNode(int x, int y)
    {
        if (x < 0 || y < 0) return null;
        if (x >= width || y >= height) return null;
        return mapNode[x, y];
    }

    public List<PathNode> getPathNodeListByType(List<int> types)
    {
        List<PathNode> list = new List<PathNode> ();
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                
                if (types.Contains(data[i, j].getType()))
                {
                    list.Add(mapNode[i, j]);
                }
            }
        }

        return list;
    }
}

public class Tile
{
    int type;
    //根据type的二进制掩码
    int mask;
    public Tile(int type)
    {
        
    }
    public void setType(int type)
    {
        this.type = type;
        type = type / 100 - 1;
        mask = 1 << type;
    }

    public int getType()
    {
        return type;
    }

    public int getMask()
    {
        return mask;
    }

    public bool getIsPass()
    {
        return true;
    }
}

public class PathNode : IComparable<PathNode>
{
    public int x, y;
    public int gCost;
    public int hCost;
    public int fCost;
    //是否可通过
    public bool isPass;
    //是否存在棋子
    public bool isRole;
    public PathNode fromNode;

    public PathNode(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.isPass = true;
        this.isRole = false;
    }

    public void calcFCost()
    {
        fCost = gCost + hCost;
    }

    public bool isThisNode(PathNode node)
    {
        return x == node.x && y == node.y;
    }

    public int CompareTo(PathNode other)
    {
        if (this.fCost != other.fCost)
        {
            return this.fCost.CompareTo(other.fCost);
        }else if (this.gCost != other.gCost){
            return this.gCost.CompareTo(other.gCost);
        }

        return 0;
    }
}