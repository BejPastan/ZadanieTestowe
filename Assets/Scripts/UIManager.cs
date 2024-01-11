using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Button[] characterButtons;
    //prefab for character button
    [SerializeField]
    GameObject characterButtonPrefab;
    [SerializeField]
    Transform infoWindow;

    public void GetCharacters(TeamControl team)
    {
        //subscribe to event
        PlayerControl.ScrollOverUI += PlayerScrollOverUI;
        //set character buttons array size to number of characters
        characterButtons = new Button[team.characters.Length];
        //create buttons for each character
        for(int i = 0; i < team.characters.Length; i++)
        {
            //create button
            GameObject button = Instantiate(characterButtonPrefab, transform);
            //set button text to character name
            button.GetComponentInChildren<TextMeshProUGUI>().text = team.characters[i].transform.name;
            //add button to array
            characterButtons[i] = button.GetComponent<Button>();
            //set position of button
            characterButtons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * characterButtons[i].GetComponent<RectTransform>().rect.height * 2);
            //add listener to button
            Debug.Log("UIManager: GetCharacters " + i);
            int num = i;
            characterButtons[i].onClick.AddListener(delegate { team.SetLeader(num); });
            characterButtons[i].onClick.AddListener(delegate { SelectLeader(num); });
        }
    }

    public void SelectLeader(int num)
    {
        Debug.Log("UIManager: SelectLeader " + num);
        characterButtons[num].GetComponent<Image>().color = Color.green;
        //set color of other buttons to white
        for(int i = 0; i < characterButtons.Length; i++)
        {
            if(i != num)
            {
                characterButtons[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    //show string info in info window
    public async Task ShowInfo(string info)
    {
        infoWindow.gameObject.SetActive(true);
        infoWindow.GetComponentInChildren<TextMeshProUGUI>().text = info;
        await Task.Delay(5000);
        infoWindow.gameObject.SetActive(false);

    }

    //player scroll mouse wheel over UI
    public void PlayerScrollOverUI()
    {
        Debug.Log("UIManager: PlayerScrollOverUI");
        //move all characters buttons in deriction of mouse wheel
        for(int i = 0; i < characterButtons.Length; i++)
        {
            characterButtons[i].GetComponent<RectTransform>().anchoredPosition += new Vector2(0, Input.GetAxis("Mouse ScrollWheel") * -300);
        }
    }
}
