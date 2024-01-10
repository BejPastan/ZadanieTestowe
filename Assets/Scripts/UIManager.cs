using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Button[] characterButtons;
    //prefab for character button
    [SerializeField]
    GameObject characterButtonPrefab;

    public void GetCharacters(ref TeamControl team)
    {
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
            //set position of button as X=0 , Y = -i * button height*2
            characterButtons[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * characterButtons[i].GetComponent<RectTransform>().rect.height * 2);
        }
    }
}
