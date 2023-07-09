using System;
using UnityEngine;


public class ChatBubbleHandler : MonoBehaviour
{
    [SerializeField] private GameObject chatBubblePrefab;
    private float chatBoxOffsetX = -0.5f; // slightly left off object
    private float chatBoxOffsetY = 1f; // slightly above object
    private System.Random rnd;

    private String[] humanScaredLines =
    {
        "LARRY? LARRY! STOP IT!!1!",
        "WILHELM SCREAM",
        "I SHOULD'VE KEPT UP CARDIO!",
        "ZOMBIES?? NOT AGAIN!",
        "SO... NO MORE TAXES?",
        "THAT ZOMBIE CAN SUCC!",
        "YOU STINK WORSE THAN MY MOTHER IN LAW",
        "WHAT A WAY TO GO",
        "GOODBYE CRUEL WORLD! GOODBYE!",
        "NOT NOW I'M DEBT FREE",
        "THANKS FOR THIS, RUINED MY HOLIDAY",
        "I CAN'T BELIEVE YOU'VE DONE THIS",
        "AH! MY LEG! MY LEEEEGGG!",
        "GOD HELP ME",
        "HELP! ANYONE? ANYONE THERE?",
        "SO THIS IS HOW IT ENDS...",
        "WHAT COULD'VE BEEN...",
        "I DIDN'T FINISH MY AUTOBIOGRAPHY",
        "BUT WHAT ABOUT MY CRYPTO??",
        "HUH, FUNNY HOW THINGS WORK OUT",
        "HONEY? STOP! IT'S ME!",
        "OH SHIT",
        "JUDGEMENT DAY HATH COMETH!",
        "MY SECRET DIES WITH ME",
        "MY LOVE? I KNEW YOU'D COME BACK!",
        "IS THIS BECAUSE OF WHAT I WAS LIKE AT 17?"
    };

    private String[] enemyLines =
    {
        "I'M ALL OUTTA GUM...",
        "I COUNT SIX BULLETS",
        "AIM FOR THE HEAD!",
        "COME ON MOTHAFOCKAAA",
        "FOR KING AND COUNTRY!",
        "I'VE WAITED MY WHOLE LIFE FOR THIS",
        "YEEHAW!",
        "THAT'S IT... STAY RIIIIGHT THERE...",
        "HERE WE GO!",
        "THIS ONES FOR YOU, MAMA",
        "GOD HELP US ALL",
        "DOWN TO MY LAST MAG...",
        "GOD I LOOK SO COOL RIGHT NOW",
        "ANOTHER FOR THE TALLY",
        "AND SHE SAID THE MILITARY WAS A DUMB IDEA",
        "WHAT AN UGLY PIECE OF SHIT"
    };

    private String[] zombieLines =
    {
        "ow my leg... really hurts...",
        "Hey! She was... my boss!",
        "Wait... where's my arm?",
        "wow we really stink...",
        "what's... for dinner tonight?",
        "i can see clearly now... the brain has gone...",
        "traaaaiinnss... i mean... braiiinnss",
        "ACHOO! bless me",
        "what a beautiful... day",
        "james blunt was... ahead of his... time...",
        "life is... so much easier without... grammar...",
        "hmm... tastes like chicken",
        "yoohoo! anyone... home?",
        "where's my... foot gone...?",
        "better than being... in the office",
        "i had so many... things i wanted to do...",
        "haven't walked... this much in... years",
        "so... many new... friends",
        "what's the... plan tomorrow guys...?",
        "something... stuck in my teeth...",
        "can't believe i... was vegetarian",
        "...never...showering...again...",
        "oh i used to... work here...!",
        "my back's... killing me..."
    };

    private void Start()
    {
        rnd = new System.Random(); 
    }

    public void ShowText(Vector3 objectPos, string type)
    {
        string text;
        if (type == "enemy")
        {
            int choice = rnd.Next(enemyLines.Length);
            text = enemyLines[choice];
        }
        else if (type == "humanScared")
        {
            int choice = rnd.Next(humanScaredLines.Length);
            text = humanScaredLines[choice];
        }
        else if (type == "zombie")
        {
            int choice = rnd.Next(zombieLines.Length);
            text = zombieLines[choice];
        }
        else
        {
            print("type no match in ShowText");
            return;
        }

        Vector3 chatPos = new Vector3(objectPos.x + chatBoxOffsetX, objectPos.y + chatBoxOffsetY);
        GameObject chatBox = Instantiate(chatBubblePrefab, chatPos, Quaternion.identity);
        chatBox.GetComponent<ChatBox>().textToShow = text;
    }
}