using Io.GuessWhat.MainApp.Models;
using System.Collections.Generic;

namespace Io.GuessWhat.MainApp.Repositories
{
    /**
    The IChecklistRepository stores every data regarding the checklist-functionality of guess-what.io.
    **/
    public interface IChecklistRepository
    {
        List<ChecklistBrowseItem> LoadChecklistCollection();
        ChecklistModel LoadChecklistModel(string theId);
        ChecklistResultModel SaveChecklistResultModel(ChecklistResultModel item);
        ChecklistResultModel LoadChecklistResultModel(string id);
    }
}
