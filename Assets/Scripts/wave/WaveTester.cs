using UnityEngine;

public class WaveTester : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            WaveManager wm = GetComponent<WaveManager>();
            if (wm != null)
            {
                wm.StartNextWave();
                Debug.Log("🚀 Запущена следующая волна!");
            }
        }
    }
}
