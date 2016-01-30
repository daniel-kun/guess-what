using Io.GuessWhat.MainApp.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Io.GuessWhat.MainApp.ViewModels
{
    /**
    ViewModel for Results.cshtml.
    **/
    public class ChecklistResultViewModel
    {

        /**
        Constructs a ChecklistResultViewModel from a ChecklistResultModel.
        result.Template is required to be set for this to succeed.

        The item tree hierarchy is constructed from the template and the corresponding
        result to each ChecklistItem is found in result.Items by matchin ChecklistResultItem.TemplateItemId
        with the ChecklistItem.Id.
        **/
        public static ChecklistResultViewModel FromResult(ChecklistResultModel result)
        {
            return new ChecklistResultViewModel()
            {
                Model = result,
                Items = ConnectResultItems(result.Template.Items, result.Results).ToList()
            };
        }

        private static IEnumerable<ChecklistResultViewItem> ConnectResultItems(List<ChecklistItem> items, List<ChecklistResultItem> results)
        {
            return ConnectResultItemsImpl(items, results, 0);
        }

        private static IEnumerable<ChecklistResultViewItem> ConnectResultItemsImpl(List<ChecklistItem> items, List<ChecklistResultItem> results, int indentation)
        {
            if (items != null) {
                foreach (var item in items)
                {
                    var resultItem = results.Find(result => result.TemplateItemId == item.Id);
                    var resultViewItems = ConnectResultItemsImpl(item.Items, results, indentation + 1).ToList();
                    yield return new ChecklistResultViewItem()
                    {
                        TemplateItem = item,
                        ResultItem = resultItem,
                        Items = resultViewItems,
                        IndentationLevel = indentation
                    };
                }
            }
        }

        /**
        The original ChecklistResultModel, as stored in the repository.
        **/
        public ChecklistResultModel Model
        {
            get;
            set;
        }

        /**
        A (possible) tree hierarchy of ChecklistResultViewItems that corresponds to the
        tree hierarchy of the original template. (see Model.Template).
        **/
        public List<ChecklistResultViewItem> Items
        {
            get;
            set;
        }
    }
}
