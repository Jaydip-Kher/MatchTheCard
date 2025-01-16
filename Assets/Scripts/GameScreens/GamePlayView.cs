using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePlayView : UIScreens
{
    [SerializeField] private GameData gameData;
    [SerializeField] private CardInfo cardInfo;
    [SerializeField] private List<CardSpriteData> cardSprites = new List<CardSpriteData>();
    [SerializeField] private AudioData audioData;
    private HashSet<int> accessedIndices = new HashSet<int>();

    private System.Random rng = new System.Random();

    private void OnEnable()
    {
        ActionController.Instance.levelGenerated += OnLevelGenerated;
        ActionController.Instance.onShowComplete += OnShowCardComplete;
        ActionController.Instance.showLoadingComplete += onLoadingComplete;
    }
    private void OnDisable()
    {
        if (ActionController.Instance == null) return;
        ActionController.Instance.levelGenerated -= OnLevelGenerated;
        ActionController.Instance.onShowComplete -= OnShowCardComplete;
        ActionController.Instance.showLoadingComplete -= onLoadingComplete;
    }
    private void OnLevelGenerated()
    {
        accessedIndices.Clear();
        gameData.gameCards.ForEach(card => card.OnStartShow());
        int pairCout = gameData.gameCards.Count / 2;
        for (int pairIndex = 0; pairIndex < pairCout; pairIndex++)
        {
            int randomIndex = GetRandomIndex();
            cardSprites.Add(cardInfo.cardSprites[randomIndex]);
            cardSprites.Add(cardInfo.cardSprites[randomIndex]);
        }
        Shuffle();
        for (int cardIndex = 0; cardIndex < gameData.gameCards.Count; cardIndex++)
        {
            gameData.gameCards[cardIndex].SetCardImage(cardSprites[cardIndex].cardSprite, cardSprites[cardIndex].pairid);
        }
        ActionController.Instance.hideLoading?.Invoke();
    }
    public void Shuffle()
    {
        int count = cardSprites.Count;
        for (int listIndex = 0; listIndex < count - 1; listIndex++)
        {
            int selectedIndex = rng.Next(listIndex, count); // Pick a random index from i to n-1
            (cardSprites[listIndex], cardSprites[selectedIndex]) = (cardSprites[selectedIndex], cardSprites[listIndex]); // Swap elements
        }
    }

    private int GetRandomIndex()
    {
        if (accessedIndices.Count >= cardInfo.cardSprites.Count)
        {
            Debug.LogWarning("All indices have been used. Returning -1 or resetting logic.");
            return -1; // Optional: you might want to reset accessedIndices here if needed.
        }

        int randomIndex;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, cardInfo.cardSprites.Count);
        } while (accessedIndices.Contains(randomIndex));

        accessedIndices.Add(randomIndex);
        return randomIndex;
    }

    private void OnShowCardComplete(Card card)
    {
        gameData.selectedCards.Add(card);
        CheckRule();
    }
    private void CheckRule()
    {
        if (gameData.selectedCards.Count == 2)
        {
            if (gameData.selectedCards[0].spriteId== gameData.selectedCards[1].spriteId)
            {
                gameData.selectedCards[0].OnMatched();
                gameData.selectedCards[1].OnMatched();
                ActionController.Instance.UpdateScrore?.Invoke(true);
                AudioController.Instance.PlayEffect(audioData.cardMatch);

            }
            else
            {
                gameData.selectedCards[0].FlipToHide();
                gameData.selectedCards[1].FlipToHide();
                ActionController.Instance.UpdateScrore?.Invoke(false);
                AudioController.Instance.PlayEffect(audioData.cardMismatch);
            }
            gameData.selectedCards.Clear();
        }
        bool isLevelCleared = gameData.gameCards.All(card => card.isCardMatched);
        Debug.LogError(isLevelCleared);
        if(isLevelCleared)
        {
            ActionController.Instance.displayScore?.Invoke(onDisplayScoreComplete);
            ActionController.Instance.onLevelComplete?.Invoke();
            cardSprites.Clear();
            AudioController.Instance.PlayEffect(audioData.gameWin);

        }
    }
    private void onDisplayScoreComplete()
    {
        ActionController.Instance.showLoading?.Invoke();
    }
    private void onLoadingComplete()
    {
        ActionController.Instance.onLevelReset();
        ActionController.Instance.generateLevel?.Invoke(PlayerController.Instance.currentLevel);
    }

    public void ToggleMusic()
    {
        AudioController.Instance.ToggleBG();
    }
    public void ToggleSound()
    {
        AudioController.Instance.ToggleFX();
    }
}