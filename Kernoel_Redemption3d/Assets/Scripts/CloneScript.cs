using UnityEngine;

public class CloneScript : MonoBehaviour
{
    /** MOVEMENT STUFF **/
    public bool isArrivedAtPlayer;

    public float currentMovementSpeed = 10f;
    public float movementSpeed = 10f;

    /** PLAYER STUFF **/
    private GameObject player;

    private PlayerScript playerScript;

    private void Start()

    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player) playerScript = player.GetComponent<PlayerScript>();

        currentMovementSpeed = movementSpeed;
    }

    private void OnCollisionEnter(Collision other)
    {
        var target = other.gameObject;
        if (!isArrivedAtPlayer)
        {
            switch (target.tag)
            {
                case "Clone":
                    var cloneScript = target.GetComponent<CloneScript>();
                    if (cloneScript && cloneScript.isArrivedAtPlayer)
                    {
                        Debug.Log("Clone is arrived at other Clone");
                        isArrivedAtPlayer = true;
                    }

                    break;

                case "Player":
                    Debug.Log("Clone is arrived at Player");
                    isArrivedAtPlayer = true;
                    break;
            }
        }

        if (isArrivedAtPlayer) currentMovementSpeed = 0f;
    }

    private void Update()
    {
        if (player)
        {
            // Debug.Log(playerScript.moveInput);
            if (!Mathf.Approximately(playerScript.moveInput.x, 0.0f) ||
                !Mathf.Approximately(playerScript.moveInput.y, 0.0f) ||
                !Mathf.Approximately(playerScript.moveInput.z, 0.0f))
            {
                isArrivedAtPlayer = false;
                currentMovementSpeed = movementSpeed;
            }

            if (!isArrivedAtPlayer)
            {
                transform.LookAt(player.transform.position);
                transform.position += Time.deltaTime * currentMovementSpeed * transform.forward;
            }
        }
    }
}