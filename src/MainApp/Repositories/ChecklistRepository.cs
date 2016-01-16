using Io.GuessWhat.MainApp.Models;
using Microsoft.Extensions.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Io.GuessWhat.MainApp.Repositories
{
    public class ChecklistRepository : IChecklistRepository
    {
        public ChecklistRepository (IOptions<Settings> settings)
        {
            mChecklistDb = Settings.ConnectToDatabase(settings.Value).Item2;
        }

        public ChecklistModel SaveChecklistModel(ChecklistModel template)
        {
            var newId = Tools.Web.ShortGuid.CreateGuid();
            template.Id = newId;
            template.Items = template.Items.Select(item => new ChecklistItem()
            {
                Id = Tools.Web.ShortGuid.CreateGuid(),
                Title = item.Title,
            }).ToList();
            template.CreationTime = DateTime.Now.ToUniversalTime();
            var templateCollection = GetTemplateCollection();
            templateCollection.InsertOne(template);
            return template;
        }

        public ChecklistModel LoadChecklistModel(string id)
        {
            var templateCollection = GetTemplateCollection();
            return templateCollection.Find(Builders<ChecklistModel>.Filter.Eq("Id", id)).FirstOrDefault();
        }

        public IEnumerable<ChecklistBrowseItem> LoadChecklistBrowseItems()
        {
            var templateCollection = GetTemplateCollection();
            var sort = Builders<ChecklistModel>.Sort.Descending(nameof(ChecklistModel.CreationTime));
            int counter = 0;
            foreach (var item in templateCollection.Find(new BsonDocument()).Sort(sort).ToEnumerable())
            {
                if (++counter > MaxBrowseItems)
                {
                    break;
                }
                yield return new ChecklistBrowseItem()
                {
                    Id = item.Id,
                    Title = item.Title,
                    CreationTime = item.CreationTime,
                };
            }
        }

        private IMongoCollection<ChecklistModel> GetTemplateCollection()
        {
            return mChecklistDb.GetCollection<ChecklistModel>("templates");
        }

        public ChecklistResultModel SaveChecklistResultModel(ChecklistResultModel item)
        {
            var newId = Tools.Web.ShortGuid.CreateGuid();
            item.Id = newId;
            var resultCollection = mChecklistDb.GetCollection<ChecklistResultModel>("results");
            resultCollection.InsertOne(item);
            return item;
        }

        public ChecklistResultModel LoadChecklistResultModel(string id)
        {
            var resultCollection = mChecklistDb.GetCollection<ChecklistResultModel>("results");
            var result = resultCollection.Find(Builders<ChecklistResultModel>.Filter.Eq("Id", id)).FirstOrDefault();
            result.Template = LoadChecklistModel(result.TemplateId);
            result.Results = ConnectChecklistResultItems(result.Results, result.Template);
            return result;
        }

        /**
        Returns a new list that is equal to results, except that the "TemplateItem" property is
        set to the TemplateItem with the TemplateItemId from template.Items.
        **/
        private List<ChecklistResultItem> ConnectChecklistResultItems(List<ChecklistResultItem> results, ChecklistModel template)
        {
            return new List<ChecklistResultItem> (
                results.Select(item => new ChecklistResultItem()
                {
                    Result = item.Result,
                    TemplateItemId = item.TemplateItemId,
                    TemplateItem = template.Items.Find (templateItem => templateItem.Id == item.TemplateItemId)
                }));
        }

        private IMongoDatabase mChecklistDb;

        /// Maximum number of entries in the browse page
        public static readonly int MaxBrowseItems = 20;
    }
}
