using System.Collections;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    public string textToShow;
    private TextMeshPro textComponent;
    private float boxMoveSpeed = 0.001f;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = gameObject.GetComponentInChildren<TextMeshPro>();
        StartCoroutine(DelayDie());
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = textToShow;
        transform.position = new Vector3(transform.position.x, transform.position.y + boxMoveSpeed);
    }

    IEnumerator DelayDie()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}