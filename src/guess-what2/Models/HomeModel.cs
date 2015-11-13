using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace guess_what2.Models
{
    public class HomeModel
    {
        public readonly List<DifficultyAspectItem> PreconditionItems = new List<DifficultyAspectItem>
        {
            new DifficultyAspectItem ()
            {
                DisplayName = "Requirements or User Story are",
                InternalName = "requirements",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Steps to Reproduce are",
                InternalName = "steps_to_reproduce",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Required Data Sets are",
                InternalName = "data_sets",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Required Devices are",
                InternalName = "devices",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Required Third Party Contacts are",
                InternalName = "third_party",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Technical Knowledge and Skills are",
                InternalName = "tech_skills",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Domain Knowledge and Skills are",
                InternalName = "domain_knowledge",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Test Cases are",
                InternalName = "test_cases",
            },
        };

    }
}
