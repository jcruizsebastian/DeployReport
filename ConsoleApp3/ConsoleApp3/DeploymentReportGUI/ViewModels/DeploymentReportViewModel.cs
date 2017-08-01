using Prism.Windows.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeploymentReportGUI.ViewModels
{

    internal class DeploymentReportViewModel : ValidatableBindableBase
    {
        [Required(ErrorMessage = "Project name required.", AllowEmptyStrings = false)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Version number required.", AllowEmptyStrings = false)]
        [RegularExpression(@"([0-9]+\.){1,3}([0-9]+)", ErrorMessage = "Format must be 10.545.1")]
        public string VersionNumber { get; set; }

        [Required(ErrorMessage = "At least one issue required.", AllowEmptyStrings = false)]
        public List<string> IssuesId { get; set; }
    }
}
