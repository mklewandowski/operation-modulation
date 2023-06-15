using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphChunk : MonoBehaviour
{
    GameSceneManagerDDR gameSceneManagerDDR;
    public GameObject BackImage;
    public Image GraphImage1;
    public Image GraphImage2;
    public bool HasTriggeredNext = false;
    public bool IsActive = false;
    public int BinaryVal = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetColor(Color newColor)
    {
        GraphImage1.color = newColor;
        if (GraphImage2 != null)
        {
            GraphImage2.color = newColor;
        }
    }
}
