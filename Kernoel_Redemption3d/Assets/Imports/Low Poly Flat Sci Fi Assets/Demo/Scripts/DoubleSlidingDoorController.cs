using System.Collections;

using UnityEngine;


//	This script handles automatic opening and closing sliding doors
//	It is fired by triggers and the door closes if found no character in the trigger area


//	Door status
public enum DoubleSlidingDoorStatus
{
    Closed,
    Open,
    Animating
}

[RequireComponent(typeof(AudioSource))]
public class DoubleSlidingDoorController : MonoBehaviour
{
    private DoubleSlidingDoorStatus _status = DoubleSlidingDoorStatus.Closed;

    [SerializeField] public Transform halfDoorLeftTransform; // Left panel of the sliding door
    [SerializeField] public Transform halfDoorRightTransform; // Right panel of the sliding door

    [SerializeField] private float slideDistance = 0.88f; // Sliding distance to open each panel the door

    private Vector3 _leftDoorClosedPosition;
    private Vector3 _leftDoorOpenPosition;

    private Vector3 _rightDoorClosedPosition;
    private Vector3 _rightDoorOpenPosition;

    [SerializeField] private float speed = 1f; // Speed for opening and closing the door

    private int _objectsOnDoorArea;


    // Sound Fx
    [SerializeField] public AudioClip doorOpeningSoundClip;
    [SerializeField] public AudioClip doorClosingSoundClip;

    private AudioSource _audioSource;


    // Use this for initialization
    private void Start()
    {
        _leftDoorClosedPosition = new Vector3(0f, 0f, 0f);
        _leftDoorOpenPosition = new Vector3(0f, 0f, slideDistance);

        _rightDoorClosedPosition = new Vector3(0f, 0f, 0f);
        _rightDoorOpenPosition = new Vector3(0f, 0f, -slideDistance);

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_status != DoubleSlidingDoorStatus.Animating)
        {
            if (_status == DoubleSlidingDoorStatus.Open)
            {
                if (_objectsOnDoorArea == 0)
                {
                    StartCoroutine(nameof(CloseDoors));
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_status != DoubleSlidingDoorStatus.Animating)
        {
            if (_status == DoubleSlidingDoorStatus.Closed)
            {
                StartCoroutine(nameof(OpenDoors));
            }
        }

        if (other.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Characters"))
        {
            _objectsOnDoorArea++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //	Keep tracking of objects on the door
        if (other.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Characters"))
        {
            _objectsOnDoorArea--;
        }
    }

    private IEnumerator OpenDoors()
    {
        if (doorOpeningSoundClip != null)
        {
            _audioSource.PlayOneShot(doorOpeningSoundClip, 0.7F);
        }

        _status = DoubleSlidingDoorStatus.Animating;

        var t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;

            halfDoorLeftTransform.localPosition = Vector3.Slerp(_leftDoorClosedPosition, _leftDoorOpenPosition, t);
            halfDoorRightTransform.localPosition = Vector3.Slerp(_rightDoorClosedPosition, _rightDoorOpenPosition, t);

            yield return null;
        }

        _status = DoubleSlidingDoorStatus.Open;
    }

    private IEnumerator CloseDoors()
    {
        if (doorClosingSoundClip != null)
        {
            _audioSource.PlayOneShot(doorClosingSoundClip, 0.7F);
        }

        _status = DoubleSlidingDoorStatus.Animating;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;

            halfDoorLeftTransform.localPosition = Vector3.Slerp(_leftDoorOpenPosition, _leftDoorClosedPosition, t);
            halfDoorRightTransform.localPosition = Vector3.Slerp(_rightDoorOpenPosition, _rightDoorClosedPosition, t);

            yield return null;
        }

        _status = DoubleSlidingDoorStatus.Closed;
    }

    //	Forced door opening
    public bool DoOpenDoor()
    {
        if (_status != DoubleSlidingDoorStatus.Animating)
        {
            if (_status == DoubleSlidingDoorStatus.Closed)
            {
                StartCoroutine(nameof(OpenDoors));
                return true;
            }
        }

        return false;
    }

    //	Forced door closing
    public bool DoCloseDoor()
    {
        if (_status != DoubleSlidingDoorStatus.Animating)
        {
            if (_status == DoubleSlidingDoorStatus.Open)
            {
                StartCoroutine(nameof(CloseDoors));
                return true;
            }
        }

        return false;
    }
}