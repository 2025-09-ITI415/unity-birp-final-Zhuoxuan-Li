using UnityEngine;

public class CollectSignalTower : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        GameManager.Instance.CollectSignalTower();
        Destroy(gameObject);
    }
}
