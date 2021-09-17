using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationMoralesOffice : LocationBase
{
    public LocationMoralesOffice() : base()
    {
        Name = LocationsNames.morales_office;
        Label = "Моралес детективс, офис";
        Address = new Address("Тенпенни стрит", 42, 6);
        GoStage(0);
    }

    public override void SetStages()
    {
        LocationStage cs = new LocationStage(0, "К слову, ты сидишь за столом в своем кабинете.");
        cs.Actions.Add(new QuestAction(0, "Осмотреть верхний ящик стола.", 1));
        cs.Actions.Add(new QuestAction(1, "Осмотреть нижний ящик стола.", 2));
        cs.Actions.Add(new QuestAction(2, "Осмотреть хранилище.", 3, true));
        cs.AllowCaseActions = true;
        SetActionsParent(cs);
        Stages.Add(cs);

        cs = new LocationStage(1, "Тут ничего нет.");
        cs.Actions.Add(new QuestAction(0, "Окей.", 0));
        cs.AllowCaseActions = false;
        SetActionsParent(cs);
        Stages.Add(cs);

        cs = new LocationStage(2, "Тут могло бы что-то быть, но тут ничего нет.");
        cs.Actions.Add(new QuestAction(0, "Ясно.", 0));
        cs.AllowCaseActions = false;
        SetActionsParent(cs);
        Stages.Add(cs);
    }
}
