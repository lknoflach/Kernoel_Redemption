using UnityEngine;

public class MoveObjectAToB : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Transform endMarker;

    // Movement speed in units per second.
    public float moveSpeed = 1f;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    private void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Set journeyLength is startMarker and endMarker are set.
        if (startMarker && endMarker) SetJourneyLength();
    }

    // Move to the target end position.
    private void Update()
    {
        // Only start movement if journeyLength is set.
        if (journeyLength > 0f)
        {
            // Distance moved equals elapsed time times speed..
            var distCovered = (Time.time - startTime) * moveSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            var fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);

            // Destroy the GameObject if we reached the endMarker
            if (journeyLength <= distCovered)
            {
                // Check if gameObject has a parent
                if (transform.parent.CompareTag("Trap"))
                {
                    // Destroy the parent
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    // Destroy yourself
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetJourneyLength()
    {
        // Calculate the journey length if is startMarker and endMarker are set.
        if (startMarker && endMarker) journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }
}