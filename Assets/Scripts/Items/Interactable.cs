using UnityEngine;

public class Interactable : MonoBehaviour
{

    private GameObject player1;

    public float radius = 3f;

    public Transform interactionTransform;

    public bool isFocus = false;

    public float distance;
    Transform player;

    bool hasInteracted = false;

    //public void OnFocused (Transform playerTransform)
    //{
    //isFocus = true;
    //player = playerTransform;
    //}


    public virtual void Interact()//This is when the item will get picked up and put into the players inventory
    {
        //This is meant to be overwrtten
        Debug.Log("Interacting With " + transform.name);
    }

    private void Awake()
    {
        player1 = GameObject.Find("Camera");
    }

    void Update()
    {
        //Testing to find player position
        //Debug.Log("Player Position is: " + player1.transform.position);

        //var playerObject = GameObject.Find("Player");
        //var playerPos:Vector3 = playerObject.transform.position;
        //Debug.Log(playerPos);

        //if (isFocus)
        //{
        //if (isFocus)
        //{
        //distance = player.transform.position;

        //The distance of the playefrom the item
        distance = Vector3.Distance(player1.transform.position, interactionTransform.position);
        //If the player is with in the radious and is pressing the F key then this runs
        if (distance <= radius * 0.8f && !hasInteracted && Input.GetKeyDown(KeyCode.F))
        {

            Debug.Log("INTERACT"); //This is wehere the interacion will happen.
            hasInteracted = true;
            Interact();
        }
    }



    //public void OnDefocused()
    //{
        //isFocus = true;
        //player = null;
   // }

    void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
