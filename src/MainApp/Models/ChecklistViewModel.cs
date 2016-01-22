using System;
using System.Collections.Generic;
using System.Text;

namespace Io.GuessWhat.MainApp.Models
{
    /**
    Represents a trimmed down version of the ChecklistModel that is used in the New view.
    **/
    public class ChecklistViewModel
    {
        /**
        Maximum text length for the Items property.
        **/
        public static readonly int ItemsMaxLength = 4000;

        /// @see ChecklistModel.Title
        public string Title
        {
            get;
            set;
        }

        /// @see ChecklistModel.Description
        public string Description
        {
            get;
            set;
        }

        /*
        Stores a multi-line text representation of @see ChecklistModel.Items.
        See ChecklistController.CreateChecklistItemsFromText for the rules how this Items
        string will be converted to ChecklistModel.Items.
        */
        public string Items
        {
            get;
            set;
        }

        /**
        Creates a new ChecklistViewModel from a ChecklistModel template.
        Converts the items list into a multi-line text as seen on the new/fork views.
        Items are separated by a newline, there is a newline for the last item, too.
        If items is null, an empty string is returned.
        **/
        public static ChecklistViewModel FromModel(ChecklistModel template)
        {
            return new ChecklistViewModel()
            {
                Title = template.Title,
                Description = template.Description,
                Items = CreateItemsTextFromChecklistItems(template.Items)
            };
        }

        /**
        Converts the items list into a multi-line text as seen on the new/fork views.
        Items are separated by a newline, there is a newline for the last item, too.
        If items is null, an empty string is returned.
        **/
        private static string CreateItemsTextFromChecklistItems(List<ChecklistItem> items)
        {
            if (items != null)
            {
                var result = new StringBuilder();
                foreach (var item in items)
                {
                    result.AppendLine(item.Title);
                }
                return result.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
