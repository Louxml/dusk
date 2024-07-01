using UnityEngine;

public class Camera_script : MonoBehaviour
{
    public float zoomSpeed = 1f;        // 放大缩放速度
    public float minOrthoSize = 1f;     // 最小正交大小
    public float maxOrthoSize = 10f;    // 最大正交大小

    public float zoomSpeedsmall = 2f;        // 缩小缩放速度
    public float minOrthoSizesmall = 1f;     // 最小正交大小
    public float maxOrthoSizesmall = 10f;    // 最大正交大小

    private Camera cam;

    // 限制摄像机位置的边界框
    public Vector2 minCameraPos;
    public Vector2 maxCameraPos;

    public float panSpeed = 25f; // 视角移动的速度

    private Vector3 lastPanPosition; // 上一帧鼠标位置


    // 缩小时的中心点坐标（世界坐标）
    public Vector2 zoomOutCenterPoint;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // 获取鼠标滚轮的输入
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            // 放大时以鼠标位置为中心，缩小时以指定坐标为中心
            Vector3 zoomCenter;
            if (scroll > 0)
            {
                // 放大时以鼠标位置为中心进行缩放
                zoomCenter = cam.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                // 缩小时以画面中心为中心进行缩放
                zoomCenter = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane));
            }
            float newSize;
            // 计算新的正交大小
            if (scroll > 0)
            {
                newSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minOrthoSize, maxOrthoSize);
                Debug.Log(scroll * zoomSpeed);
            }
            else
            {
                float small_speed = scroll * zoomSpeedsmall;
                if (small_speed > -0.05)
                {
                    small_speed = -0.05f;
                }


                newSize = Mathf.Clamp(cam.orthographicSize - small_speed, minOrthoSize, maxOrthoSize);
            }

                // 计算放大或缩小后相机的新位置
            Vector3 cameraMovement = zoomCenter - cam.transform.position;
            cameraMovement.z = 0; // 确保不改变相机在Z轴的位置
            float sizeRatio = newSize / cam.orthographicSize;
            cameraMovement *= (1 - sizeRatio);

            // 设置相机的新位置和正交大小
            if (scroll > 0)
            {
                cam.transform.position += cameraMovement;
            }
            else
            {
                cam.transform.position -= cameraMovement;
            }
            cam.orthographicSize = newSize;

            // 限制摄像机位置
            Vector3 pos = cam.transform.position;
            pos.x = Mathf.Clamp(pos.x, minCameraPos.x, maxCameraPos.x);
            pos.y = Mathf.Clamp(pos.y, minCameraPos.y, maxCameraPos.y);
            cam.transform.position = pos;
        }

        // 处理鼠标中键拖动的视角移动
        if (Input.GetMouseButtonDown(2))
        {
            lastPanPosition = Input.mousePosition; // 记录当前鼠标位置（屏幕空间）
        }
        else if (Input.GetMouseButton(2)) // 鼠标中键持续按住
        {
            PanCamera(); // 调用视角移动的方法
        }
    }


    void PanCamera()
    {
        // 计算鼠标屏幕位置的变化
        Vector3 newPanPosition = Input.mousePosition;
        Vector3 offset = lastPanPosition - newPanPosition;

        // 如果有明显的移动，则进行视角移动
        if (offset.magnitude > 0)
        {
            offset.z = 0; // 确保不改变相机的深度
            offset = cam.ScreenToViewportPoint(offset); // 转换到视口空间

            // 通过相机的正交大小来调整移动速度，并加入调整系数
            float zoomSensitivity = panSpeed * Mathf.Pow(cam.orthographicSize, -0.5f);

            // 计算移动向量
            Vector3 move = new Vector3(offset.x * zoomSensitivity, offset.y * zoomSensitivity, 0);

            // 预计算新的相机位置
            Vector3 newPos = cam.transform.position + move;
            
            // 检查X轴是否超出范围
            newPos.x = Mathf.Clamp(newPos.x, minCameraPos.x, maxCameraPos.x);

            // 检查Y轴是否超出范围
            newPos.y = Mathf.Clamp(newPos.y, minCameraPos.y, maxCameraPos.y);

            // 更新相机位置
            cam.transform.position = newPos;

            // 更新鼠标屏幕位置以用于下一次计算
            lastPanPosition = newPanPosition;
        }
    }
}