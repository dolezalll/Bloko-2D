using UnityEngine;

public class Blocks : MonoBehaviour
{
    public enum BlockType { Teleport, Pushable, Finish }

    [SerializeField] private BlockType blockType;
    [SerializeField] private Transform teleportLocation;

    private Rigidbody rb;
    private LevelController levelController;

    private delegate void BlockAction(GameObject player);
    private static readonly Dictionary<BlockType, BlockAction> BlockActions = new()
    {
        { BlockType.Teleport, TeleportPlayer },
        { BlockType.Pushable, PushBlock },
        { BlockType.Finish, LoadNextLevel }
    };

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        levelController = GetComponent<LevelController>();
    }

    private void InitializePushableBlock()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnPlayerTouch(GameObject player)
    {
        BlockActions[blockType](player);
    }

    private void TeleportPlayer(GameObject player)
    {
        player.transform.position = teleportLocation.position;
    }

    private void PushBlock(GameObject player)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f).Select(c => c.attachedRigidbody).Where(rb => rb != null).ToArray();
        if (hitColliders.Length > 0)
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            Vector3 pushDirection = (playerRb.transform.position - transform.position).normalized;
            rb.AddForce(pushDirection * 100f, ForceMode.Impulse);
        }
    }

    private void LoadNextLevel(GameObject player)
    {
        levelController?.LoadNextLevel();
    }
}
