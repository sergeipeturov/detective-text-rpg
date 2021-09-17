using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameManager GameManager;
    public QuestUIManager QuestUIManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GameStarted += OnGameStarted;
        QuestUIManager.ActionClickEvent += PerformAction;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGameStarted()
    {
        //пока что просто грузим первые этапы текущей локации и текущего дела.
        //далее надо будет реализовать нормальную загрузку
        if (GameManager?.Data?.Cases?.CurrentCase != null)
        {
            GoStage(0, StageType.caseStage);
        }
        else
        {
            if (GameManager?.Data?.Locations?.CurrentLocation != null)
                GoStage(0, StageType.locationStage);
        }
    }

    /// <summary>
    /// Выполнение выбранного действия
    /// </summary>
    private void PerformAction(QuestAction action) //сделать это обработчиком события, запускаемого в QuestUIManager
    {
        action.IsAlreadyPerformed = true;
        GoStage(action.StageId, action.ParentType);
    }

    /// <summary>
    /// Переход на этап по Id и типу
    /// </summary>
    /// <param name="stageId">Номер этапа</param>
    /// <param name="type">Тип этапа</param>
    private void GoStage(int stageId, StageType type)
    {
        switch (type)
        {
            case StageType.caseStage:
                //текст
                GameManager.Data.Cases.CurrentCase.GoStage(stageId);
                currentText = GameManager.Data.Cases.CurrentCase.CurrentStage.Text;
                GameManager.Data.Cases.CurrentCase.CurrentStage.IsTextAlreadyShown = true;
                if (GameManager.Data.Locations.CurrentLocation.CurrentStage.IsTextAlreadyShown)
                {
                    if (GameManager.Data.Locations.CurrentLocation.CurrentStage.IsTextRepeatable)
                    {
                        currentText = currentText.Replace("[LocText]", GameManager.Data.Locations.CurrentLocation.CurrentStage.Text);
                        GameManager.Data.Locations.CurrentLocation.CurrentStage.IsTextAlreadyShown = true;
                    }
                }
                else
                {
                    currentText = currentText.Replace("[LocText]", GameManager.Data.Locations.CurrentLocation.CurrentStage.Text);
                    GameManager.Data.Locations.CurrentLocation.CurrentStage.IsTextAlreadyShown = true;
                }
                //пока что добавляем в FullText дела текст, в котором учтен текст локации, если это подразумевается. подумать, нужно ли так делать.
                GameManager.Data.Cases.CurrentCase.FullText.Add(currentText);

                //действия
                currentActions = new List<QuestAction>();
                for (int i = 0; i < GameManager.Data.Cases.CurrentCase.CurrentStage.Actions.Count; i++)
                {
                    if (!GameManager.Data.Cases.CurrentCase.CurrentStage.Actions[i].IsAlreadyPerformed)
                    {
                        //TODO: проверка на доп. условия
                        currentActions.Add(GameManager.Data.Cases.CurrentCase.CurrentStage.Actions[i]);
                    }
                    else
                    {
                        if (GameManager.Data.Cases.CurrentCase.CurrentStage.Actions[i].IsRepeatable)
                        {
                            //TODO: проверка на доп. условия
                            currentActions.Add(GameManager.Data.Cases.CurrentCase.CurrentStage.Actions[i]);
                        }
                    }
                }
                if (GameManager.Data.Cases.CurrentCase.CurrentStage.AllowLocationActions)
                {
                    for (int i = 0; i < GameManager.Data.Locations.CurrentLocation.CurrentStage.Actions.Count; i++)
                    {
                        if (!GameManager.Data.Locations.CurrentLocation.CurrentStage.Actions[i].IsAlreadyPerformed)
                        {
                            //TODO: проверка на доп. условия
                            currentActions.Add(GameManager.Data.Locations.CurrentLocation.CurrentStage.Actions[i]);
                        }
                        else
                        {
                            if (GameManager.Data.Locations.CurrentLocation.CurrentStage.Actions[i].IsRepeatable)
                            {
                                //TODO: проверка на доп. условия
                                currentActions.Add(GameManager.Data.Locations.CurrentLocation.CurrentStage.Actions[i]);
                            }
                        }
                    }
                }
                break;

            case StageType.locationStage:
                //текст
                GameManager.Data.Locations.CurrentLocation.GoStage(stageId);
                currentText = GameManager.Data.Locations.CurrentLocation.CurrentStage.Text;
                GameManager.Data.Locations.CurrentLocation.CurrentStage.IsTextAlreadyShown = true;
                //пока что добавляем в FullText текущего дела текст локации, даже если он не относится к текущему делу. подумать, нужно ли так делать.
                GameManager.Data.Cases.CurrentCase.FullText.Add(currentText);

                //действия
                currentActions = new List<QuestAction>();
                for (int i = 0; i < GameManager.Data.Locations.CurrentLocation.CurrentStage.Actions.Count; i++)
                {
                    if (!GameManager.Data.Locations.CurrentLocation.CurrentStage.Actions[i].IsAlreadyPerformed)
                    {
                        //TODO: проверка на доп. условия
                        currentActions.Add(GameManager.Data.Locations.CurrentLocation.CurrentStage.Actions[i]);
                    }
                    else
                    {
                        if (GameManager.Data.Locations.CurrentLocation.CurrentStage.Actions[i].IsRepeatable)
                        {
                            //TODO: проверка на доп. условия
                            currentActions.Add(GameManager.Data.Locations.CurrentLocation.CurrentStage.Actions[i]);
                        }
                    }
                }
                if (GameManager.Data.Locations.CurrentLocation.CurrentStage.AllowCaseActions)
                {
                    for (int i = 0; i < GameManager.Data.Cases.CurrentCase.CurrentStage.Actions.Count; i++)
                    {
                        if (!GameManager.Data.Cases.CurrentCase.CurrentStage.Actions[i].IsAlreadyPerformed)
                        {
                            //TODO: проверка на доп. условия
                            currentActions.Add(GameManager.Data.Cases.CurrentCase.CurrentStage.Actions[i]);
                        }
                        else
                        {
                            if (GameManager.Data.Cases.CurrentCase.CurrentStage.Actions[i].IsRepeatable)
                            {
                                //TODO: проверка на доп. условия
                                currentActions.Add(GameManager.Data.Cases.CurrentCase.CurrentStage.Actions[i]);
                            }
                        }
                    }
                }
                break;
        }

        //вывод в QuestUIManager
        QuestUIManager.CurrentTextToScroll(currentText);
        QuestUIManager.ActionListToScroll(currentActions);
    }

    /// <summary>
    /// Полный текст, который отображается игроку
    /// </summary>
    private List<string> fullText;

    /// <summary>
    /// Текущий текст, который нужно добавить в FullText
    /// </summary>
    private string currentText;

    /// <summary>
    /// Доступные на текущий момент игроку действия
    /// </summary>
    private List<QuestAction> currentActions;

    
}