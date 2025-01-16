using UnityEngine;
using UnityEngine.UI;

public class LevelBuilder : MonoBehaviour
{
    [Header("<<<--- Configuration --->>>")]
    [Space(10)]
    [SerializeField] private LevelConfigurator levelConfigurator;
    [SerializeField] private GridLayoutGroup layoutGroup;
    [SerializeField] private Card cardBlueprint;
    [SerializeField] private Transform cardHolder;
    [SerializeField] private CardPoolling cardPool;

    [Header("<<<--- Game Data --->>>")]
    [Space(10)]
    [SerializeField] private GameData mainGameData;
    [SerializeField] private CardInfo cardInfo;  // Assumes your class is called "CardDta"

    private RectTransform layoutRectTransform;
    private LevelInfo activeLevelData;

    private void InitializeBuilder()
    {
        // Set target FPS
        Application.targetFrameRate = 90;

        // Prepare internal card info
        cardInfo.FillCardData();

        // Cache the RectTransform
        layoutRectTransform = layoutGroup.GetComponent<RectTransform>();

        // Subscribe to events
        ActionController.Instance.generateLevel += CommenceLevelCreation;
        ActionController.Instance.onLevelReset += WipeActiveLevel;
    }

    private void DeinitializeBuilder()
    {
        // Unsubscribe when disabled/destroyed
        if (ActionController.Instance == null) return;
        ActionController.Instance.generateLevel -= CommenceLevelCreation;
        ActionController.Instance.onLevelReset -= WipeActiveLevel;
    }

    /// <summary>
    /// Trigger the process of creating a new level based on the given level index.
    /// </summary>
    private void CommenceLevelCreation(int levelIndex)
    {
        activeLevelData = levelConfigurator.levelInfo[levelIndex];
        layoutGroup.constraintCount = activeLevelData.columns;

        BuildCardGrid(activeLevelData.rows, activeLevelData.columns);
    }

    /// <summary>
    /// Instantiates cards in a grid, based on row and column counts.
    /// </summary>
    private void BuildCardGrid(int totalRows, int totalColumns)
    {
        int totalCards = totalRows * totalColumns;
        for (int i = 0; i < totalCards; i++)
        {
            Card newCard = cardPool.GetCard();
            // Re-parent the card to the cardHolder so it appears in the grid
            //newCard.transform.SetParent(cardHolder, false);
            newCard.transform.localScale = Vector3.one;
            // Make sure any relevant data is updated
            newCard.cardId = i;

            // Add to the mainGameData
            mainGameData.gameCards.Add(newCard);
        }
        mainGameData.gameCards.ForEach(card => card.isCardMatched = false);
        // Defer grid resize so the layout has time to update
        Invoke(nameof(ResizeCardLayout), 0.1f);
    }

    /// <summary>
    /// Adjusts the cell size of the GridLayoutGroup so that each cell is a uniform square.
    /// </summary>
    private void ResizeCardLayout()
    {
        RectTransform holderRect = cardHolder.GetComponent<RectTransform>();
        float availableWidth = holderRect.rect.width;
        float availableHeight = holderRect.rect.height;

        if (availableWidth <= 0 || availableHeight <= 0)
        {
            Debug.LogWarning("Card holder dimensions are invalid or not updated yet.");
            return;
        }

        int rowCount = activeLevelData.rows;
        int columnCount = activeLevelData.columns;

        float widthMinusPadding = availableWidth
            - layoutGroup.padding.left
            - layoutGroup.padding.right
            - (layoutGroup.spacing.x * (columnCount - 1));

        float heightMinusPadding = availableHeight
            - layoutGroup.padding.top
            - layoutGroup.padding.bottom
            - (layoutGroup.spacing.y * (rowCount - 1));

        if (rowCount <= 0 || columnCount <= 0 || widthMinusPadding <= 0 || heightMinusPadding <= 0)
        {
            Debug.LogWarning("Effective grid dimensions are invalid.");
            return;
        }

        // Determine the smaller dimension to keep cells square
        float cellWidth = widthMinusPadding / columnCount;
        float cellHeight = heightMinusPadding / rowCount;
        float finalCellSize = Mathf.Min(cellWidth, cellHeight);

        layoutGroup.cellSize = new Vector2(Mathf.RoundToInt(finalCellSize), Mathf.RoundToInt(finalCellSize));

        // Notify other systems (if required)
        ActionController.Instance.levelGenerated?.Invoke();
    }

    /// <summary>
    /// Removes all existing cards and resets the current level data.
    /// </summary>
    private void WipeActiveLevel()
    {
        mainGameData.isGameStarted = false;

        // Destroy all card objects
        for (int cardIndex = 0; cardIndex < mainGameData.gameCards.Count; cardIndex++)
        {
            cardPool.ReturnCard(mainGameData.gameCards[cardIndex]);
        }

        // Clear and reset card data
        mainGameData.gameCards.Clear();

        mainGameData.gameCards.ForEach(card =>
        {
            card.isCardSpriteSet = false;
        });
    }
    /// <summary>
    /// Cleanup logic before this object is destroyed.
    /// </summary>
    private void CleanupBuilder()
    {
        mainGameData.gameCards.Clear();
        mainGameData.selectedCards.Clear();

        mainGameData.gameCards.ForEach(c => c.isCardSpriteSet = false);

        // Reset any additional card data
        cardInfo.OnReset();
    }

    #region Unity Lifecycle

    private void OnEnable()
    {
        InitializeBuilder();
    }

    private void OnDisable()
    {
        DeinitializeBuilder();
    }

    private void OnDestroy()
    {
        CleanupBuilder();
    }

    #endregion
}
