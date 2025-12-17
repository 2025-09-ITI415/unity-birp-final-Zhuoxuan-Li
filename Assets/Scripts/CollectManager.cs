using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Stage
    {
        CollectBoxes,
        CollectSignalTower,
        CollectPhone,
        Evacuate,
        Completed,
        Failed
    }

    [Header("Stage State")]
    public Stage stage = Stage.CollectBoxes;

    [Header("Task 1: Boxes (Classroom)")]
    public int requiredBoxes = 3;
    public int currentBoxes = 0;
    public GameObject wallToRemove;

    [Header("Final: Evac Ring (inactive at start)")]
    public GameObject evacRing;

    [Header("Timer")]
    public float timeLimitSeconds = 180f;
    private float timeLeft;
    public bool timerEnabled = true;

    [Header("UI (Top-Left)")]
    public TMP_Text progressText;
    public TMP_Text taskText;
    public TMP_Text timerText;

    [Header("End Panels (Optional)")]
    public GameObject winPanel;
    public GameObject failPanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        timeLeft = timeLimitSeconds;

        if (evacRing != null) evacRing.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
        if (failPanel != null) failPanel.SetActive(false);

        UpdateUI();
    }

    private void Update()
    {
        if (!timerEnabled) return;

        if (stage == Stage.Completed || stage == Stage.Failed) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            FailGame();
        }

        UpdateTimerUI();
    }

    // ----------- Task 1: Boxes -----------
    public void CollectBox()
    {
        if (stage != Stage.CollectBoxes) return;

        currentBoxes++;
        if (currentBoxes >= requiredBoxes)
        {
            if (wallToRemove != null) Destroy(wallToRemove);
            stage = Stage.CollectSignalTower;
        }

        UpdateUI();
    }

    // ----------- Task 2: Signal Tower -----------
    public void CollectSignalTower()
    {
        if (stage != Stage.CollectSignalTower) return;

        stage = Stage.CollectPhone;
        UpdateUI();
    }

    // ----------- Task 3: Phone -----------
    public void CollectPhone()
    {
        if (stage != Stage.CollectPhone) return;

        stage = Stage.Evacuate;
        if (evacRing != null) evacRing.SetActive(true);

        UpdateUI();
    }

    // ----------- Task 4: Evacuate -----------
    public void Evacuate()
    {
        if (stage != Stage.Evacuate) return;

        stage = Stage.Completed;
        EndGame(success: true);
    }

    private void FailGame()
    {
        if (stage == Stage.Completed || stage == Stage.Failed) return;

        stage = Stage.Failed;
        EndGame(success: false);
    }

    private void EndGame(bool success)
    {
        if (success)
        {
            if (winPanel != null) winPanel.SetActive(true);
        }
        else
        {
            if (failPanel != null) failPanel.SetActive(true);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UpdateUI();
        UpdateTimerUI();
    }

    private void UpdateUI()
    {
        if (progressText != null)
        {
            if (stage == Stage.CollectBoxes)
                progressText.text = $"{currentBoxes} / {requiredBoxes} Boxes Collected";
            else
                progressText.text = $"Boxes: {requiredBoxes}/{requiredBoxes}";
        }

        if (taskText != null)
        {
            switch (stage)
            {
                case Stage.CollectBoxes:
                    taskText.text = "Task: Find 3 boxes to collect";
                    break;
                case Stage.CollectSignalTower:
                    taskText.text = "Task: Go up the mountain and collect the signal tower";
                    break;
                case Stage.CollectPhone:
                    taskText.text = "Task: Go back to the classroom and find the phone";
                    break;
                case Stage.Evacuate:
                    taskText.text = "Task: Find the golden evacuation ring to escape";
                    break;

            }
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;

        int seconds = Mathf.CeilToInt(timeLeft);
        int min = seconds / 60;
        int sec = seconds % 60;
        timerText.text = $"Time: {min:00}:{sec:00}";
    }
}
