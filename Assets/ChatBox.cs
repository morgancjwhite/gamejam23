using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    public string textToShow;
    private TextMeshPro textComponent;
    private GameObject background;
    private bool isLoaded;

    // Start is called before the first frame update
    void Start()
    {
        textComponent = gameObject.GetComponentInChildren<TextMeshPro>();
        background = gameObject.GetComponentInChildren<SpriteRenderer>().gameObject;
        StartCoroutine(DelayDie());
        isLoaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = textToShow;
        if (textToShow != null && !isLoaded)
        {
            float scale = (float)textToShow.Length / 10;
            background.transform.localScale = new Vector3(scale, 0.8f, 1f);
            float xOffset = textToShow.Length;
            print(transform.position.x);
            print(xOffset);
            print(scale);
            background.transform.localPosition = new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z); ;
            isLoaded = true;
        }
    }

    IEnumerator DelayDie()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}