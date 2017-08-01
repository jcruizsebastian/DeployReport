using System.Collections.Generic;
using System.Linq;

namespace DeploymentReport.Input
{
    public class ProjectInput<T, TKey> where  T : Issue where TKey : IssueId
    {
        private IList<T> issues;

        public void AddIssue(T issue)
        {
            issues.Add(issue);
        }

        public void ClearAll()
        {
            issues.Clear();
        }

        public void RemoveIssue(TKey issueId)
        {
            issues.Remove(issues.FirstOrDefault(i => i.ID == issueId));
        }
    }
}