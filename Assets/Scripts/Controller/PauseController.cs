using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour{
    private GameObject pausePanel;
    public GameObject listItemPrefab;
    private GameObject contentPanel;
    private bool isPaused = false;
    private DeckController deckController;
    private List<GameObject> startDeck;
    private int cursor;
    private Text title;
    private Text description;
    private int cnt;
    private ScrollRect scrollView;
    private Transform cursorItem;
    private Transform imagePanel;
    private Transform selectedObject;
    private RectTransform selectedContent;

    void Start(){
        Time.timeScale = 1f;
        isPaused = false;
        pausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        imagePanel = pausePanel.transform.Find("Image");
        cursorItem = imagePanel.Find("CursorImage");
        contentPanel = GameObject.FindGameObjectWithTag("ChipPanel");
        pausePanel.SetActive(false);
        deckController = GameObject.FindGameObjectWithTag("GameController").GetComponent<DeckController>();
        title = pausePanel.transform.Find("Image/Title/Text").GetComponent<Text>();
        description = pausePanel.transform.Find("Image/Title/Description/Text").GetComponent<Text>();
        scrollView = contentPanel.GetComponent<ScrollRect>();
    }
    void Update(){
        if(!isPaused) return;
        if(Input.GetKeyDown(KeyCode.W)){
            cursor -= 1;
            if(cursor < 0) cursor = 0;
        }
        else if(Input.GetKeyDown(KeyCode.S)){
            cursor += 1;
            if(cursor >= cnt) cursor = cnt - 1;
        }
        updateCursor();
    }

    public void keyDown(){
        if(isPaused){
            Time.timeScale = 1f;
            isPaused = false;
            pausePanel.SetActive(false);
        }
        else{
            Time.timeScale = 0f;
            isPaused = true;
            pausePanel.SetActive(true);
            foreach (Transform child in contentPanel.transform.Find("Viewport/Content").transform)
                Destroy(child.gameObject);

            startDeck = deckController.fullDeck();
            cnt = 0;
            foreach (GameObject child in startDeck) {
                GameObject newItem = Instantiate(listItemPrefab) as GameObject;
                newItem.transform.SetParent(contentPanel.transform.Find("Viewport/Content").transform, false);
                newItem.GetComponentInChildren<ChipPanelController>().set(child.GetComponent<Card>());
                newItem.name = "Item" + cnt;
                cnt ++;
            }
            cursor = 0;
            updateCursor();
        }
    }
    public bool getStatus(){
        return isPaused;
    }
    public void updateCursor(){
        selectedObject = contentPanel.transform.Find("Viewport/Content").Find("Item" + cursor);
        selectedContent = selectedObject.GetComponent<RectTransform>();
        title.text = selectedObject.GetComponent<ChipPanelController>().chipName;
        description.text = selectedObject.GetComponent<ChipPanelController>().description;

        cursorItem.position = selectedObject.position - new Vector3(selectedContent.rect.width, 0, 0);
    }
}
