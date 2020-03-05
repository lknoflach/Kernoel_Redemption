using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class HideWalls : MonoBehaviour
{
    // The player to shoot the ray at
    private GameObject _player;

    // The camera to shoot the ray from
    private Camera _camera;

    // List of all objects that we have hidden.
    public List<Transform> hiddenObjects;

    // Layers to hide
    public LayerMask layerMask;

    private void Start()
    {
        if (Camera.main) _camera = Camera.main;
        _player = GameObject.Find("PlayerHans");

        // Initialize the list
        hiddenObjects = new List<Transform>();
    }

    private void Update()
    {
        if (!_camera || !_player) return;

        // Find the direction from the camera to the player
        var direction = _player.transform.position - _camera.transform.position;

        // The magnitude of the direction is the distance of the ray
        var distance = direction.magnitude;

        // Raycast and store all hit objects in an array. Also include the layermaks so we only hit the layers we have specified
        var hits = Physics.RaycastAll(_camera.transform.position, direction, distance, layerMask);

        // Go through the objects
        foreach (var hit in hits)
        {
            var currentHit = hit.transform;

            // Only do something if the object is not already in the list
            if (hiddenObjects.Contains(currentHit)) continue;
            
            // Add to list and disable renderer
            hiddenObjects.Add(currentHit);
            var currentHitRenderer = currentHit.GetComponent<Renderer>();
            if (currentHitRenderer) currentHitRenderer.enabled = false;
        }

        // Clean the list of objects that are in the list but not currently hit.
        for (var i = 0; i < hiddenObjects.Count; i++)
        {
            var isHit = hits.Any(hit => hit.transform == hiddenObjects[i]);
            // Check every object in the list against every hit

            // If it is not among the hits
            if (isHit) continue;
            
            // Enable renderer, remove from list, and decrement the counter because the list is one smaller now
            var wasHidden = hiddenObjects[i];
            var wasHiddenRenderer = wasHidden.GetComponent<Renderer>();
            if (wasHiddenRenderer) wasHiddenRenderer.enabled = true;
            hiddenObjects.RemoveAt(i);
            i--;
        }
    }
}