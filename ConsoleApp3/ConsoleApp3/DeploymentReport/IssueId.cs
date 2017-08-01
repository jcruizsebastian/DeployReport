namespace DeploymentReport
{
    public class IssueId
    {
        private string issueID { get; set; }

        public override bool Equals(object obj)
        {
            return issueID.Equals(((IssueId)obj).issueID);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}