using UnityEngine;

public class EnemyKnockdownTester : MonoBehaviour
{
    public Consciousness consciousness;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            consciousness.ReceiveImpact(999);
        }
    }
}