using guess_what2.Models;
using System.Collections.Generic;
using System;

namespace guess_what2.DataSources
{
    /**
    ChecklistDataSource contains methods to retrieve ChecklistModel with it's
    ChecklistModelItems from the production data source.
    **/
    public class ChecklistDataSource
    {
        public virtual ChecklistModel LoadChecklistModel (string theId)
        {
            // Fake implementation:
            return new ChecklistModel()
            {
                Id = theId,
                Title = "Some awesome checklist",
                Description = "Lorem ipsum",
                Items = new List<ChecklistItem> ()
                {
                    new ChecklistItem ()
                    {
                        Id = "A1EA346B-AF1C-4606-8C1E-C79127247030",
                        Title = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr",
                    },
                    new ChecklistItem ()
                    {
                        Id = "FCF00786-BBE7-47D9-80C5-FA36E7C218C2",
                        Title = "ed diam nonumy eirmod tempor invidunt ut labore et dolore",
                    },
                    new ChecklistItem ()
                    {
                        Id = "13889AF6-FFA4-4609-940B-31440E50A2FF",
                        Title = "magna aliquyam erat, sed diam voluptua. At vero eos et accusam et",
                    },
                    new ChecklistItem ()
                    {
                        Id = "8A6D2004-FD15-4634-8376-6C1F8BB3AFB0",
                        Title = "justo duo dolores et ea rebum. Stet clita kasd gubergren",
                    },
                    new ChecklistItem ()
                    {
                        Id = "503FF11E-E1B6-4B3A-8548-1326D661EC1B",
                        Title = "no sea takimata sanctus est Lorem ipsum dolor sit amet",
                    },
                    new ChecklistItem ()
                    {
                        Id = "23E68222-78DE-4991-ADE4-D1A3B45AD94F",
                        Title = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy",
                    },
                    new ChecklistItem ()
                    {
                        Id = "D4E513AB-E5FE-468F-9DC0-8AA6376E5241",
                        Title = "eirmod tempor invidunt ut labore et dolore magna aliquyam erat",
                    },
                }
            };
        }

        public List <ChecklistBrowseItem> LoadChecklistCollection()
        {
            return new List<ChecklistBrowseItem>()
            {
                new ChecklistBrowseItem ()
                {
                    Id = "FAKE",
                    Title = "Some awesome checklist",
                },
                new ChecklistBrowseItem ()
                {
                    Id = "FAKE2",
                    Title = "Super awesome checklist",
                },
                new ChecklistBrowseItem ()
                {
                    Id = "FAKE3",
                    Title = "Very bad checklist",
                },
            };
        }

        public string SaveChecklistResultModel(ChecklistResultModel item)
        {
            return "FAKERESULT";
        }

        public ChecklistResultModel LoadChecklistResultModel(string id)
        {
            var checklistModel = LoadChecklistModel("FAKE");
            return new ChecklistResultModel()
            {
                Id = id,
                CreationTime = DateTime.Now,
                UserId = "d.albuschat",
                Template = checklistModel,
                Results = TestResultsFromTemplateItems(checklistModel.Items),
            }; 
        }

        /**
        Only for temporary demo purposes.
        Generates a list of CheclistResultItems with a cycling result of OK, not OK and not checked.
        **/
        private List<ChecklistResultItem> TestResultsFromTemplateItems(List<ChecklistItem> items)
        {
            var result = new List<ChecklistResultItem>();
            int counter = 0;
            foreach (var item in items)
            {
                switch (counter)
                {
                    case 0:
                        result.Add(new ChecklistResultItem()
                        {
                            Result = ChecklistResult.CheckedAndOk,
                            TemplateItem = item
                        });
                        ++counter;
                        break;
                    case 1:
                        result.Add(new ChecklistResultItem()
                        {
                            Result = ChecklistResult.CheckedAndNotOk,
                            TemplateItem = item
                        });
                        ++counter;
                        break;
                    case 2:
                        result.Add(new ChecklistResultItem()
                        {
                            Result = ChecklistResult.NotChecked,
                            TemplateItem = item
                        });
                        counter = 0;
                        break;
                }
            }
            return result;
        }
    }
}
