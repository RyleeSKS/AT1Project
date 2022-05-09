using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Widget : MonoBehaviour
{
    [SerializeField] private Image[] icons;
    private float flashTimer = -1;
    private Player player;

    private void Awake()
    {
        //avoid using FindObjectOfType function as much as possible
        //awake function is the only exception (and much smaller games)
        FindObjectOfType<Enemy>().GameOverEvent += delegate 
        {
            gameObject.SetActive(false);
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.Player;
        player.InputEvent += MovementInput;
        for(int i = 0; i < icons.Length; i++) // setting icons to default colour
        {
            icons[i].color = Color.grey;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(flashTimer >= 0)
        {
            flashTimer += Time.deltaTime;
            if(flashTimer > 0.1f)
            {
                flashTimer = -1;
                for (int i = 0; i < icons.Length; i++) // setting icons to default colour
                {
                    icons[i].color = Color.grey;
                }
            }
        }
    }

    public void MovementInput(char c) //char means character
    {
        //single quotaion marks for keys
        //make sure the order is left, right, up, down.
        flashTimer = 0;
        switch (c)
        {
            case 'l':
                if(player.CurrentNode.Parents.Length > 0)
                {
                    foreach(Node node in player.CurrentNode.Parents[0].Children)
                    {
                        if(node.transform.position.x == player.CurrentNode.transform.position.x - 10)
                        {
                            //move player to node
                            player.MoveToNode(node);
                            icons[0].color = Color.green;
                            return;
                        }
                    }
                }
                icons[0].color = Color.red;
                break;
            case 'r':
                if (player.CurrentNode.Parents.Length > 0)
                {
                    foreach (Node node in player.CurrentNode.Parents[0].Children)
                    {
                        if (node.transform.position.x == player.CurrentNode.transform.position.x + 10)
                        {
                            //move player to node
                            player.MoveToNode(node);
                            icons[1].color = Color.green;
                            return;
                        }
                    }
                }
                icons[1].color = Color.red;
                break;
            case 'u':
                if(player.CurrentNode.Parents.Length > 0 && player.CurrentNode.Parents[0].Parents.Length > 0)
                {
                    //move player to parent of current node 
                    player.MoveToNode(player.CurrentNode.Parents[0]);
                    icons[2].color = Color.green;
                }
                else
                {
                    icons[2].color = Color.red;
                }
                break;
            case 'd':
                if(player.CurrentNode.Children.Length > 0)
                {
                    //move player to first child
                    player.MoveToNode(player.CurrentNode.Children[0]);
                    icons[3].color = Color.green;
                }
                else
                {
                    icons[3].color = Color.red;
                }
                break;
        }
    }
}
