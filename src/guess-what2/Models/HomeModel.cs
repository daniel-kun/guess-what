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

        public readonly List<DifficultyAspectItem> ComplexityItems = new List<DifficultyAspectItem>
        {
            new DifficultyAspectItem ()
            {
                DisplayName = "Internal Product Quality",
                InternalName = "internal_product_quality",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "External Product Quality",
                InternalName = "external_product_quality",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Documentation Quality",
                InternalName = "documentation_quality",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Scope of the Changes",
                InternalName = "scope_of_changes",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Internal Interfaces",
                InternalName = "internal_interfaces",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "External Interfaces",
                InternalName = "external_interfaces",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Downward Compatibility",
                InternalName = "downward_compatibility",
            },
            new DifficultyAspectItem ()
            {
                DisplayName = "Number of People involved",
                InternalName = "number_of_people_involved",
            },
        };
    }
}
