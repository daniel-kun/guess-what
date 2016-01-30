using Io.GuessWhat.MainApp.Models;
using System.Collections.Generic;

namespace Io.GuessWhat.MainApp.ViewModels
{
    /**
    An item in the ViewModel for Result.cshtml 
    **/
    public class ChecklistResultViewItem
    {

        /**
        The original ChecklistResultItem as it is stored in the repository.
        **/
        public ChecklistResultItem ResultItem
        {
            get;
            set;
        }

        /**
        The original ChecklistItem that was used to enter the results
        for this checklist item.
        This can be null when the template's item has not been loaded, yet.
        Use TemplateItemId in this case.
        **/
        public ChecklistItem TemplateItem
        {
            get;
            set;
        }

        /**
        If this is a parent item, Items is set to a list of children.
        If this item does not have children, Items is null.
        **/
        public List<ChecklistResultViewItem> Items
        {
            get;
            set;
        }

        /**
        The level of indentation. This is 0 for top-level ChecklistResultItems, 1 for
        the children of top-level ChecklistResultItems, 2 for the children of children, and so on.
        **/
        public int IndentationLevel
        {
            get;
            set;
        } = 0;
    }
}