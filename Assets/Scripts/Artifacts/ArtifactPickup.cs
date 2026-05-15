using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    private Artifact currentArtifact;
    public float rotationSpeed = 50f;
    public float floatSpeed = 2f;
    public float floatHeight = 0.3f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        
        float floatY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPosition + new Vector3(0, floatY, 0);
    }

    public void SetArtifact(Artifact artifact)
    {
        currentArtifact = artifact;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerArtifacts playerArtifacts = other.GetComponent<PlayerArtifacts>();
            if (playerArtifacts != null && currentArtifact != null)
            {
                playerArtifacts.AddArtifact(currentArtifact);
            }

            Destroy(gameObject);
        }
    }
}