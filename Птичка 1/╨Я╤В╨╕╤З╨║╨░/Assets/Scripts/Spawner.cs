using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject pipe;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            if (pipe == null)
            {
                yield break;
            }

            GameObject pipeObj = Instantiate(pipe);
            pipeObj.transform.position = transform.position + new Vector3(0f, Random.Range(-2f, 2f), 0f);
        }
    }
}
