using System.Collections.Generic;

namespace guess_what2.Models
{

    /**
    The ChecklistResultModel contains all information about a filled-out,
    submitted checklist result.
    **/
    public class ChecklistResultModel
    {
        public string Id
        {
            get;
            set;
        }

        /**
        The template that has been filled out and which results have been submitted.
        **/
        public ChecklistModel Template
        {
            get;
            set;
        }

        /**
        The results of each checklist item.
        **/
        public List <ChecklistResultItem> Results
        {
            get;
            set;
        }
    }
}