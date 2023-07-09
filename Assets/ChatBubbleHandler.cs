using UnityEngine;


public class ChatBubbleHandler : MonoBehaviour
{
    [SerializeField] private GameObject chatBubblePrefab;
    private float chatBoxOffsetX = -0.5f; // slightly left off object
    private float chatBoxOffsetY = 1f; // slightly above object


    private void Start()
    {
    }

    public void ShowText(Vector3 objectPos, string type)
    {
        string text = "LARRY? LARRY! STOP IT!!1!";
        Vector3 chatPos = new Vector3(objectPos.x + chatBoxOffsetX, objectPos.y + chatBoxOffsetY);
        GameObject chatBox = Instantiate(chatBubblePrefab, chatPos, Quaternion.identity);
        chatBox.GetComponent<ChatBox>().textToShow = text;
    }
}