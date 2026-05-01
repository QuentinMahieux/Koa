using UnityEngine;

public class LevelMaker : MonoBehaviour
{
    public static LevelMaker instance;
    [Header("Tabler")]
    public int nbrLigne = 6;
    public int nbrColone = 6;
    public float margeLigne = 1.4f;
    public float margeColone = 1.4f;
    
    public Vector2 startPos;
    public GameObject placeHolder;

    [Header("Interface")] 
    public GameObject buttonParent;
    
    [Header("Studio")]
    public ElementData[] elements;
    public ElementData activeElement;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Multiple LevelMaker script attached to " + gameObject.name);
            Destroy(this);
        }
    }
    
    void Start()
    {
        NewTabler();
        InstanciateInterface();
    }

    void NewTabler()
    {
        Vector2 _startPos = startPos;
        for (int i = 0; i < nbrColone; i++)
        {
            for (int j = 0; j < nbrLigne; j++)
            {
                Instantiate(placeHolder, new Vector3(_startPos.x, _startPos.y, 0), Quaternion.identity);
                _startPos.x +=  margeLigne;
            }
            _startPos.x = startPos.x;
            _startPos.y += margeColone;
        }
    }

    void InstanciateInterface()
    {
        int index = 0;
        foreach (ButtonChoiceElement button in buttonParent.GetComponentsInChildren<ButtonChoiceElement>())
        {
            if(index > elements.Length) return;
            button.Instanciate(elements[index]);
            index++;
        }
    }

    public void ChangeElement(ElementData element)
    {
        activeElement = element;
    }
}
