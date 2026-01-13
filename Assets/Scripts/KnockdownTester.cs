using UnityEngine;

public class KnockdownTester : MonoBehaviour
{
    public Consciousness consciousness;
    public int testDamage = 25;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (consciousness != null)
                consciousness.ReceiveImpact(testDamage);
        }
    }
}