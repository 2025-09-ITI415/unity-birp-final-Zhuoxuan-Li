using UnityEngine;

public class CollectPhone : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        GameManager.Instance.CollectPhone();
        Destroy(gameObject);
    }
}
