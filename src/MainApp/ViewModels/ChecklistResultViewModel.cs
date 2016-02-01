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

        /**
        The total numbers of checklist items that are marked as "Checked and OK".
        Parent-items do not count, since they are a "yes/no" type of question, without
        saying anything about the actual quality of the checked item.
        **/
        public int CheckedAndOkTotalCount
        {
            get
            {
                if (!mCheckedAndOkTotalCount.HasValue)
                {
                    int totalCount = 0;
                    ForEachRecursiveItem(Items, (item) =>
                    {
                        if (item.ResultItem != null && item.ResultItem.Result == ChecklistResult.CheckedAndOk)
                        {
                                ++totalCount;
                        }
                    });
                    mCheckedAndOkTotalCount = totalCount;
                }
                return mCheckedAndOkTotalCount.Value;
            }
        }

        /**
        The total numbers of checklist items that are marked as "Checked and not OK".
        Parent-items do not count, since they are a "yes/no" type of question, without
        saying anything about the actual quality of the checked item.
        **/
        public int CheckedAndNotOkTotalCount
        {
            get
            {
                if (!mCheckedAndNotOkTotalCount.HasValue)
                {
                    int totalCount = 0;
                    ForEachRecursiveItem(Items, (item) =>
                    {
                        if (item.ResultItem != null && item.ResultItem.Result == ChecklistResult.CheckedAndNotOk)
                        {
                            ++totalCount;
                        }
                    });
                    mCheckedAndNotOkTotalCount = totalCount;
                }
                return mCheckedAndNotOkTotalCount.Value;
            }
        }

        /**
        The total numbers of checklist items that are marked as "Not checked".
        **/
        public int NotCheckedTotalCount
        {
            get
            {
                if (!mNotCheckedTotalCount.HasValue)
                {
                    int totalCount = 0;
                    ForEachRecursiveItem(Items, (item) =>
                    {
                        if (item.ResultItem != null && item.ResultItem.Result == ChecklistResult.NotChecked)
                        {
                            ++totalCount;
                        }
                    });
                    mNotCheckedTotalCount = totalCount;
                }
                return mNotCheckedTotalCount.Value;
            }
        }

        private void ForEachRecursiveItem (List<ChecklistResultViewItem> items, Action<ChecklistResultViewItem> callback)
        {
            foreach (var item in items)
            {
                if (item.Items != null && 
                    item.Items.Count > 0)
                {
                    if (item.ResultItem != null &&
                        item.ResultItem.Result == ChecklistResult.CheckedAndOk)
                    {
                        ForEachRecursiveItem(item.Items, callback);
                    }
                } else
                {
                    callback(item);
                }
            }
        }

        int? mCheckedAndOkTotalCount = null;
        int? mCheckedAndNotOkTotalCount = null;
        int? mNotCheckedTotalCount = null;
    }
}
