using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;
    [SerializeField] private GameObject selectedGameobject;

    private BaseAction baseAction;
    
    public void SetBaseAction(BaseAction baseAction)
    {
        if (baseAction == null)
        {
            return;
        }
        
        this.baseAction = baseAction;
        
        textMeshPro.text = baseAction.GetActionName().ToUpper();
        
        button.onClick.AddListener(()=>
        {
            //Debug.Log("111");
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
        
        
    }

    public void UpdateSelectedVisual()
    {
        BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
        selectedGameobject.SetActive(selectedBaseAction == baseAction);
    }
    
    
}
