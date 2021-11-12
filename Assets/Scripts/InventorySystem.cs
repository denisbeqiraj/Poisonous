using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Start is called before the first frame update

    private Dictionary<string,int> _listObject= new Dictionary<string,int>();

    public void Add(string inventoryObject)
    {
        if (_listObject.ContainsKey(inventoryObject))
        {
            _listObject[inventoryObject] = _listObject[inventoryObject] + 1;
        }
        else
        {
            _listObject[inventoryObject] = 1;
        }
    }

    public void Remove(string inventoryObject)
    {
        if (_listObject[inventoryObject]>1)
        {
            _listObject[inventoryObject] = _listObject[inventoryObject] - 1;
        }
        else
        {
            _listObject.Remove(inventoryObject);
        }
    }

    public Dictionary<string,int> getDictionary()
    {
        return _listObject;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
