using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro gridPositionText;
    [SerializeField] private MeshRenderer gridDebugColor;

    private GridObject gridObject;

    private void Start()
    {
        UpdateText();
        this.gameObject.name = gridPositionText.text;
    }

    public void UpdateText()
    {
        gridPositionText.text = gridObject.ToString();
    }

    public void SetColor(Color color)
    {
        gridDebugColor.material.color = color;
    }
    
    
    public void SetGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
    }
    
    protected virtual void Update()
    {
        gridPositionText.text = gridObject.ToString();//放到事件触发里面
    }
    
    
    
}
