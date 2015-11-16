using System.Collections.Generic;

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
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Steps to Reproduce are",
                InternalName = "steps_to_reproduce",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Required Data Sets are",
                InternalName = "data_sets",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Required Devices are",
                InternalName = "devices",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Required Third Party Contacts are",
                InternalName = "third_party",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Technical Knowledge and Skills are",
                InternalName = "tech_skills",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Domain Knowledge and Skills are",
                InternalName = "domain_knowledge",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Test Cases are",
                InternalName = "test_cases",
                SelectedDifficulty = 0,
            },
        };

        public readonly List<DifficultyAspectItem> ComplexityItems = new List<DifficultyAspectItem>
        {
            new DifficultyAspectItem ()
            {
                DisplayName = "Internal Product Quality",
                InternalName = "internal_product_quality",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "External Product Quality",
                InternalName = "external_product_quality",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Documentation Quality",
                InternalName = "documentation_quality",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Scope of the Changes",
                InternalName = "scope_of_changes",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Internal Interfaces",
                InternalName = "internal_interfaces",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "External Interfaces",
                InternalName = "external_interfaces",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Downward Compatibility",
                InternalName = "downward_compatibility",
                SelectedDifficulty = 0,
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Number of People involved",
                InternalName = "number_of_people_involved",
                SelectedDifficulty = 0,
            },
        };

        public int? Estimate {
            get;
            set;
        }
    }
}
