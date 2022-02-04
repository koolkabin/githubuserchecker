﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainChecker.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class GitHubUserInfo
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
        public string name { get; set; }
        public object company { get; set; }
        public string blog { get; set; }
        public string location { get; set; }
        public object email { get; set; }
        public bool hireable { get; set; }
        public string bio { get; set; }
        public object twitter_username { get; set; }
        public int public_repos { get; set; }
        public int public_gists { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class VMGitHubUserFull
    {
        public string username;

        public VMGitHubUserFull()
        {
            Repos = new HashSet<GitHubRepoInfo>();
        }

        public async Task<bool> ReadURL(string username)
        {
            Repos = new HashSet<GitHubRepoInfo>();
            this.username = username;
            string gitHubUrl = $"https://api.github.com/users/{username}";
            UserInfo = await JSONAPIHelper<GitHubUserInfo>.ReadUrlAsync(gitHubUrl);
            if (UserInfo != null)
            {
                gitHubUrl = $"https://api.github.com/users/{username}/repos";
                Repos = await JSONAPIHelper<IList<GitHubRepoInfo>>.ReadUrlAsync(gitHubUrl);
            }
            return true;
        }
        public GitHubUserInfo UserInfo { get; set; }
        public ICollection<GitHubRepoInfo> Repos { get; set; }
        public ICollection<GitHubRepoInfo> DisplayRepos => Repos.OrderByDescending(x => x.stargazers_count).Take(5).ToList();
    }
}
