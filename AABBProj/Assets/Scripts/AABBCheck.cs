using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AABBCheck : MonoBehaviour
{
    public Material NormalMaterial;
    public Material SelMaterial;
    public MeshRenderer Render;
    public Vector2 Size = new Vector2(1, 1);
    public Vector2 Center;
    public AABBCheck Target;

    public float XPosition { get { return transform.position.x; } }
    public float YPosition { get { return transform.position.y; } }
    public float BoxWidth { get { return Size.x/ 2; } }
    public float BoxHeight { get { return Size.y / 2; } }
    
    // Start is called before the first frame update
    void Start()
    {
        Render.material = NormalMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target!=null)
        {
            //bool collider = DetectionBoxColliderByBB(Target);
            bool collider = DetectionBoxColliderByBS(Target);
            if(collider)
            {
                Render.material = SelMaterial;
            }
            else
            {
                Render.material = NormalMaterial;
            }
        }
    }

    private bool DetectionBoxColliderByBS(AABBCheck target)
    {
        float xDis = target.XPosition - XPosition;
        float pX = Mathf.Clamp(xDis, -BoxWidth, BoxWidth);

        float yDic = target.YPosition - YPosition;
        float pY = Mathf.Clamp(yDic, -BoxHeight, BoxHeight);

        Vector2 closePoint = new Vector2(XPosition + pX, YPosition + pY);

        return Vector2.Distance(target.transform.position, closePoint) <= 0.5f;
    }

    private bool DetectionBoxColliderByBB(AABBCheck target)
    {
        Debug.Log("XPos:" + (XPosition + BoxWidth).ToString());
        Debug.Log("TargetPos:" + (target.XPosition - target.BoxWidth).ToString());
        bool isColliderX = XPosition + BoxWidth >= target.XPosition - target.BoxWidth
            && target.XPosition + target.BoxWidth >= XPosition - BoxWidth;

        bool isColliderY = YPosition + Size.y >= target.YPosition - target.BoxHeight
            && target.YPosition + target.BoxHeight >=   - BoxHeight;

        return isColliderX && isColliderY;
    }
}
