using System;
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
        The timestamp of the exact time that the user saved this checklist result.
        **/
        public DateTime CreationTime
        {
            get;
            set;
        }

        /**
        The unique, non-modifable user id (user name) of the user that saved this checklist result.
        (Currently, the user can freely enter a UserId himself/herself when saving the results
         and it is only used for reference).
        **/
        public string UserId
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