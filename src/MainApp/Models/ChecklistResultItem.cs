﻿namespace Io.GuessWhat.MainApp.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /**
    A ChecklistResultItem is one "row" or item of a filled-out checklist and is part
    of the ChecklistResultModel, @see ChecklistResultModel.Results.
    **/
    public class ChecklistResultItem
    {
        /**
        The id of the original ChecklistItem that wa sused to enter the results for
        this checklist item.
        This is never null.
        **/
        public string TemplateItemId
        {
            get;
            set;
        }

        /**
        The result that the user entered for this checklist item (ok, not ok, not checked).
        **/
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonRepresentation(BsonType.String)]
        public ChecklistResult Result
        {
            get;
            set;
        }
    }
}
