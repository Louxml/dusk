
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EditMap : MonoBehaviour
{
    public string filename = "mapjson";
    public int width;
    public int height;
    public int cellSize;

    private GridMap grid;

    public Transform bg;

    // 0:平地，1：出生点，2：城墙，3：金矿，4：堡垒，5：炮台，6：神庙，7：风坛
    private List<GameObject> mapTile;

    public GameObject[,] mapObjects;

    public Transform tileNode;

    int tileIndex = 0;
    List<GameObject> tileList;
    // Start is called before the first frame update
    void Start()
    {
        createTile();

        loadMapData();

        mapObjects = new GameObject[width, height];

        createMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;

            checkClickTile(pos);

            int type = mapTile[tileIndex].GetComponent<MapTile>().type;
            setValue(pos, type);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;

            setValue(pos, 0);
        }
    }

    private void drawGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 startPos = getWorldPosition(i, j, 0f, 0f);
                Vector3 endPos = getWorldPosition(i, j, 0f, 1f);
                Debug.DrawLine(startPos, endPos, Color.white, 100f);
                endPos = getWorldPosition(i, j, 1f, 0f);
                Debug.DrawLine(startPos, endPos, Color.white, 100f);
            }
        }

        Vector3 pos1 = getWorldPosition(0, height, 0f, 0f);
        Vector3 pos2 = getWorldPosition(width, height, 0f, 0f);
        Debug.DrawLine(pos1, pos2, Color.white, 100f);
        pos1 = pos2;
        pos2 = getWorldPosition(width, 0, 0f, 0f);
        Debug.DrawLine(pos1, pos2, Color.white, 100f);

    }

    private Vector3 getLocalPosition(int x, int y, float anchorX = 0.5f, float anchorY = 0.5f, Vector3 anchor = default(Vector3))
    {
        Vector3 offset = -new Vector3(width, height, 0) * cellSize * 0.5f + new Vector3(anchorX, anchorY) * cellSize;
        Vector3 lpos = new Vector3(x, y, 0) * cellSize + offset;
        return lpos;
    }

    private Vector3 getWorldPosition(int x, int y, float anchorX = 0.5f, float anchorY = 0.5f)
    {
        Vector3 lpos = getLocalPosition(x, y, anchorX, anchorY);
        Vector3 wpos = transform.TransformPoint(lpos);

        return wpos;
    }

    private void getXY(Vector3 worldPosition, out int x, out int y)
    {
        Vector3 offset = -new Vector3(width, height, 0) * cellSize * 0.5f;
        Vector3 lpos = transform.InverseTransformPoint(worldPosition) - offset;
        x = Mathf.FloorToInt(lpos.x / cellSize);
        y = Mathf.FloorToInt(lpos.y / cellSize);

    }

    private int getValue(Vector3 worldPosition)
    {
        int x, y;
        getXY(worldPosition, out x, out y);
        return grid.getValue(x, y);
    }

    private void setValue(Vector3 wpos, int value)
    {
        int x, y;
        getXY(wpos, out x, out y);
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        else if(grid.getValue(x, y) != value)
        {
            grid.setValue(x, y, value);
            Destroy(mapObjects[x, y]);
            GameObject obj = getMapTile(value);
            if (obj != null)
            {
                GameObject tile = Instantiate(obj);
                mapObjects[x, y] = tile;
                Transform tileTransform = tile.transform;
                tileTransform.SetParent(transform, false);
                tileTransform.localPosition = getLocalPosition(x, y);
            }
            
        }
    }

    private void createMap()
    {
        bg.localScale = new Vector3(width, height, 1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int value = grid.getValue(i, j);
                GameObject obj = getMapTile(value);
                if (obj != null)
                {
                    GameObject tile = Instantiate(obj);
                    mapObjects[i, j] = tile;
                    Transform tileTransform = tile.transform;
                    tileTransform.SetParent(transform, false);
                    tileTransform.localPosition = getLocalPosition(i, j);
                }
                
            }
        }
    }

    void generateMapData()
    {
        grid = MapDataMgr.Instance.createGridMap(width, height, cellSize);
    }

    //加载地图数据
    void loadMapData()
    {
        grid = MapDataMgr.Instance.loadMapData(filename);

        if (grid != null)
        {
            width = grid.width;
            height = grid.height;
            cellSize = grid.cellSize;
        }
        else
        {
            generateMapData();
        }
    }

    void saveMapData()
    {
        MapData data = new MapData(grid.width, grid.height, grid.cellSize);
        data.setData(grid);
        string json = JsonUtility.ToJson(data, true);
        string filepath = Application.streamingAssetsPath + "/" + filename + ".json";

        using (StreamWriter sw = new StreamWriter(filepath))
        {
            sw.WriteLine(json);
            sw.Close();
            sw.Dispose();
        }
    }

    public void onClickSave(bool exit)
    {
        saveMapData();
        if (exit)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }

    void createTile()
    {
        mapTile = MapDataMgr.Instance.getTileList();

        tileList = new List<GameObject>();
        for (int i = 0; i < mapTile.Count; i++)
        {
            GameObject tile = new GameObject(mapTile[i].name);
            Transform tileTransform = tile.transform;
            tileTransform.SetParent(tileNode, false);

            GameObject select = Instantiate(mapTile[0]);
            Transform selectTransform = select.transform;
            selectTransform.SetParent(tileTransform, false);
            selectTransform.localScale = Vector3.one * 1.2f;
            selectTransform.localPosition = new Vector3(0f, 0.01f, 1f);
            SpriteRenderer spriteRenderer = select.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(0f, 0f, 0f, 0.6f);

            select.SetActive(i == tileIndex);
            tileList.Add(select);

            GameObject icon = Instantiate(mapTile[i]);
            icon.transform.SetParent(tileTransform, false);

            int cols = 19;
            float x = (i % cols) * 1.2f;
            float y = i / cols * -1.2f;
            tileTransform.localPosition = new Vector3(x, y);
        }
    }

    void checkClickTile(Vector3 wPos)
    {
        Vector3 lpos = tileNode.InverseTransformPoint(wPos);
        int index = -1;
        for (int i = 0; i < tileList.Count; i++)
        {
            Vector3 pos = tileList[i].transform.parent.localPosition;
            if (lpos.x >= pos.x - 0.5f && lpos.x <= pos.x + 0.5f && lpos.y >= pos.y - 0.5f && lpos.y <= pos.y + 0.5f)
            {
                index = i;
                break;
            }
        }

        if (index == -1 || index == tileIndex)
        {
            return;
        }

        tileList[tileIndex].SetActive(false);
        tileList[index].SetActive(true);
        tileIndex = index;

        Debug.Log(index);
    }

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
}
