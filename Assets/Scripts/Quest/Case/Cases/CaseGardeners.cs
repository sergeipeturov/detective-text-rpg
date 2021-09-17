using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseGardeners : CaseBase
{
    public CaseGardeners() : base()
    {
        Name = CasesNames.trying;
        Label = "Садовники умирают молодыми";
        GoStage(0);
        CurrentGoalKey = "start";
    }

    public override void SetStages()
    {
        CaseStage cs = new CaseStage(0, $"Что-то странное происходит... {Environment.NewLine}[LocText]");
        cs.Actions.Add(new QuestAction(0, "Нужно разобраться.", 1));
        cs.Actions.Add(new QuestAction(1, "Ну и плевать.", 2));
        cs.AllowLocationActions = true;
        SetActionsParent(cs);
        Stages.Add(cs);

        cs = new CaseStage(1, "Я решил, что мне нужно в этом разобраться.");
        cs.Actions.Add(new QuestAction(0, "И начал разбираться.", 3));
        SetActionsParent(cs);
        Stages.Add(cs);

        cs = new CaseStage(2, "Мне плевать. Не мое дело.");
        cs.Actions.Add(new QuestAction(0, "Так тому и быть.", 4));
        SetActionsParent(cs);
        Stages.Add(cs);
    }

    public override void SetGoals()
    {
        Goals.Add("start", "Разобраться, что происходит.");
    }

    public override void SetFacts()
    {
        Facts.Add(new CaseFact("call_time", "Рикардо позвонил тебе в 22:17. Ты это помнишь, потому что в это время смотрел на часы."));
        //LetPlayerKnowFact("call_time");
        Facts.Add(new CaseFact("rik_was_worried", "Рикардо был очень удручен, взволнован и расстроен. Для тебя это очевидно, ведь ты проницательный."));
    }

    public override void SetGueses()
    {
        Gueses.Add(new CaseGues("rik_was_worried", "Тебе показалось, что Рикардо был очень удручен, взволнован и расстроен.", true));
        //LetPlayerKnowGues("rik_was_worried");
    }

    public override void SetTestimonies()
    {
        Testimonies.Add(new CaseTestimony("gardener_has_gun", "У садовника был пистолет.", "none", true));
        //LetPlayerKnowTestimony("gardener_has_gun", "Рикардо Компадрес");
    }

    public override void SetQuestions()
    {
        Questions.Add(new CaseQuestion("who_killer", "Кто убийца?", true));
    }
}
