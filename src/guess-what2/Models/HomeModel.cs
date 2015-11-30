using System.Collections.Generic;

namespace guess_what2.Models
{
    public class HomeModel
    {
        public readonly List<EstimateAspectItem> PreconditionItems = new List<EstimateAspectItem>
        {
            new EstimateAspectItem ()
            {
                DisplayName = "Requirements or User Story are",
                InternalName = "requirements",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Steps to Reproduce are",
                InternalName = "steps_to_reproduce",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Required Data Sets are",
                InternalName = "data_sets",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Required Devices are",
                InternalName = "devices",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Required Third Party Contacts are",
                InternalName = "third_party",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Technical Knowledge and Skills are",
                InternalName = "tech_skills",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Domain Knowledge and Skills are",
                InternalName = "domain_knowledge",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Test Cases are",
                InternalName = "test_cases",
                SelectedDifficulty = 0,
            },
        };

        public readonly List<EstimateAspectItem> ComplexityItems = new List<EstimateAspectItem>
        {
            new EstimateAspectItem ()
            {
                DisplayName = "Internal Product Quality",
                InternalName = "internal_product_quality",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "External Product Quality",
                InternalName = "external_product_quality",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Documentation Quality",
                InternalName = "documentation_quality",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Scope of the Changes",
                InternalName = "scope_of_changes",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Internal Interfaces",
                InternalName = "internal_interfaces",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "External Interfaces",
                InternalName = "external_interfaces",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
            {
                DisplayName = "Downward Compatibility",
                InternalName = "downward_compatibility",
                SelectedDifficulty = 0,
            },
            new EstimateAspectItem ()
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
