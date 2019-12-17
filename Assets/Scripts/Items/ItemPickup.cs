using UnityEngine;

public class ItemPickup : Interactable
{
    

    //The iem variable
    //This is where the picked up will be stored
    public Item item;


    //Overides the Interact method in the Intractables skills
    public override void Interact()
    {
        //Anything above this line happens before the other method is ran   V
        base.Interact();
        //Anything below this line happens before the other method is ran   A

        PickUp(); 
    }

    void PickUp()
    {



        Debug.Log("Picking up " + item.name);

        //Add to inventory
        bool wasPickedUp = Inventory.instance.Add(item);


        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        
    }
}
