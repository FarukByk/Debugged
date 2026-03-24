using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Collections;
public class myMath : MonoBehaviour
{
    
    public static async void waitAndStart(float second, Action action)
    {
        myMath mm = new GameObject("wait").AddComponent<myMath>();
        mm.waitStart(second, action);
    }

    public void waitStart(float second, Action action)
    {
        StartCoroutine(wait(second, action));
    }

    public IEnumerator wait(float second, Action action)
    {
        yield return new WaitForSeconds(second);
        if (Application.isPlaying)
        {
            action?.Invoke();
            Destroy(this.gameObject);
        }
    }


}
