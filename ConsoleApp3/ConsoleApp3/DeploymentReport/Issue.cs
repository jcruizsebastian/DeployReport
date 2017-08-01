using System.Collections.Generic;

namespace DeploymentReport
{
    public class Issue
    {
        public IssueId ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public IEnumerable<Component> Components { get; set; }

        public Issue()
        {
            Components = new List<Component>();
        }
    }
}
