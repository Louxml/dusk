using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 0.1f; // 调整缩放速度
    public float minZoom = 1f; // 最小缩放值
    public float maxZoom = 1.1f; // 最大缩放值

   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // 获取鼠标滚轮滑动的值
        // 根据滚动值来调整相机的尺寸
        Camera.main.orthographicSize -= scroll * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        
    }
    
}
