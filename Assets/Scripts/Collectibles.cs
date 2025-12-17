using UnityEngine;

public class CollectBox : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        GameManager.Instance.CollectBox();
        Destroy(gameObject);
    }
}
