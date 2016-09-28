using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using WebApiClient;

namespace WebApiClientConsole
{    
    public class Program
    {
        /// <summary>
        /// Main Function
        /// </summary>
        public static void Main(string[] args)
        {
            // You cannot use await in the Main Method
            // Result property of a Task blocks until the task has completed
            var repositories = ProcessRepositories().Result;

            foreach (var repo in repositories)
            {
                Console.WriteLine(String.Concat(repo.Name, " - ", repo.Description));
                Console.WriteLine(repo.GitHubHomeUrl);
                Console.WriteLine(repo.Homepage);
                Console.WriteLine(repo.Watchers);
                Console.WriteLine(repo.LastPush);
                Console.WriteLine("===============================================");
            }
        }

        /// <summary>
        /// Process Repositories
        /// </summary>
        /// <returns></returns>
        private static async Task<List<Repositories>> ProcessRepositories()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var serializer = new DataContractJsonSerializer(typeof(List<Repositories>));

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = serializer.ReadObject(await streamTask) as List<Repositories>;

            return repositories;
        }
    }
}
