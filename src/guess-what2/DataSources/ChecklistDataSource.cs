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
        public virtual ChecklistModel LoadChecklistModel(string theId)
        {
            // Fake implementation:
            if (mFakeChecklists.ContainsKey(theId))
            {
                return mFakeChecklists[theId];
            } else
            {
                return null;
            }
        }

        public List<ChecklistBrowseItem> LoadChecklistCollection()
        {
            var result = new List<ChecklistBrowseItem>();
            foreach (var checklist in mFakeChecklists)
            {
                result.Add(new ChecklistBrowseItem()
                {
                    Id = checklist.Value.Id,
                    Title = checklist.Value.Title
                });
            }
            return result;
        }

        /**
        Creates a GUID of 22 character length via base64 decoding.
        **/
        public static string CreateGuid()
        {
            byte[] bytes = Guid.NewGuid().ToByteArray();
            string guidWithPadding = Convert.ToBase64String(bytes);
            string urlSafeGuid = ReplaceUrlUnsafeChars(guidWithPadding);
            if (urlSafeGuid.EndsWith ("==") && urlSafeGuid.Length == 24)
            {
                return urlSafeGuid.Substring(0, 22);
            } else
            {
                return string.Empty;
            }
        }

        /**
        Replaces all url-unsafe characters from a base64 encoding into url-safe characters.
        This means "+" becomes "-" and "/" becomes "_".

        **/
        public static string ReplaceUrlUnsafeChars(string guidWithPadding)
        {
            return guidWithPadding.Replace('+', '-').Replace('/', '_');
        }

        public ChecklistResultModel SaveChecklistResultModel(ChecklistResultModel item)
        {
            var newId = CreateGuid();
            item.Id = newId;
            mFakeChecklistResultModels.Add(newId, item);
            return item;
        }

        public ChecklistResultModel LoadChecklistResultModel(string id)
        {
            if (mFakeChecklistResultModels.ContainsKey (id))
            {
                var result = mFakeChecklistResultModels[id];
                result.Template = LoadChecklistModel(result.TemplateId);
                result.Results = TestResultsFromTemplateItems(result.Template.Items);
                return result;
            }
            else
            {
                return null;
            }
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

        private static readonly string mFakeChecklistId       = "3B0E78BEE31A401BB69D2F";
        private static readonly string mFakeChecklistResultId = "659BF2D779C54EFE94CE5D";

        private static readonly Dictionary<string, ChecklistModel> mFakeChecklists = new Dictionary<string, ChecklistModel>
        {
            {
                mFakeChecklistId,
                new ChecklistModel()
                {
                    Id = mFakeChecklistId,
                    Title = "Some awesome checklist",
                    Description = "Lorem ipsum",
                    Items = new List<ChecklistItem> ()
                    {
                        new ChecklistItem ()
                        {
                            Id = "A1EA346BAF1C46068C1EC7",
                            Title = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr",
                        },
                        new ChecklistItem ()
                        {
                            Id = "FCF00786BBE747D980C5FA",
                            Title = "ed diam nonumy eirmod tempor invidunt ut labore et dolore",
                        },
                        new ChecklistItem ()
                        {
                            Id = "13889AF6FFA44609940B31",
                            Title = "magna aliquyam erat, sed diam voluptua. At vero eos et accusam et",
                        },
                        new ChecklistItem ()
                        {
                            Id = "8A6D2004FD15463483766C",
                            Title = "justo duo dolores et ea rebum. Stet clita kasd gubergren",
                        },
                        new ChecklistItem ()
                        {
                            Id = "503FF11EE1B64B3A854813",
                            Title = "no sea takimata sanctus est Lorem ipsum dolor sit amet",
                        },
                        new ChecklistItem ()
                        {
                            Id = "23E6822278DE4991ADE4D1",
                            Title = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy",
                        },
                        new ChecklistItem ()
                        {
                            Id = "D4E513ABE5FE468F9DC08A",
                            Title = "eirmod tempor invidunt ut labore et dolore magna aliquyam erat",
                        },
                    }
                }
            },
        };

        private static readonly Dictionary<string, ChecklistResultModel> mFakeChecklistResultModels = new Dictionary<string, ChecklistResultModel> {
            {
                mFakeChecklistResultId,
                new ChecklistResultModel()
                {
                    Id = mFakeChecklistResultId,
                    CreationTime = DateTime.Now,
                    UserId = "d.albuschat",
                    TemplateId = mFakeChecklistId,
                }
            }
        };

    }
}
