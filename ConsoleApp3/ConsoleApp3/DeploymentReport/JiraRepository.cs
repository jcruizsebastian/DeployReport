using System;
using System.Linq;
using RestSharp;

namespace DeploymentReport
{
    public class JiraRepository : IIssuesRepository<Issue, string>
    {
        public Issue Get(string key)
        {
            var client = new RestClient("http://jira.openfinance.es/");

            var request = new RestRequest(string.Format("rest/api/2/issue/{0}", key), Method.GET);

            request.AddHeader("Authorization", string.Format("Basic {0}", Base64Encode("jcruiz:_*_d1d4ct1c78")));

            var jiraIssueResponse = client.Execute<JiraIssue>(request);

            if(jiraIssueResponse == null || jiraIssueResponse.Data == null)
                throw  new Exception("Error with : " + key);

            return ConvertToIssue(jiraIssueResponse.Data);
        }

        private Issue ConvertToIssue(JiraIssue jiraIssueResponse)
        {
            var components = jiraIssueResponse.fields.components.Select(x => new Component() {Name = x.name}).ToList();

            return new Issue()
            {
                ID = jiraIssueResponse.key,
                Description = jiraIssueResponse.fields.description,
                Title = jiraIssueResponse.fields.summary,
                Components = components,
                Creator = jiraIssueResponse.fields.creator.name
            };
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
