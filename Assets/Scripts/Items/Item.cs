using UnityEngine;


[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    //Blueprint for the items
    new public string name = "New Item";
    public Sprite icon;
    public bool isDefault = false;

    //Just type item. then a list of the items attributes will come up in any script that is linked to this

}
