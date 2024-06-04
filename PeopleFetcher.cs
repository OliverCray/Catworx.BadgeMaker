using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CatWorx.BadgeMaker
{
  class PeopleFetcher
  {
    public static List<Employee> GetEmployees()
    {
      List<Employee> employees = new List<Employee>();
      while (true)
      {
        Console.WriteLine("Enter first name: (leave empty to exit): ");
        string firstName = Console.ReadLine() ?? "";
        if (firstName == "")
        {
          break;
        }

        Console.WriteLine("Enter last name: ");
        string lastName = Console.ReadLine() ?? "";
        Console.WriteLine("Enter ID: ");
        int id = Int32.Parse(Console.ReadLine() ?? "");
        Console.WriteLine("Enter photo URL: ");
        string photoUrl = Console.ReadLine() ?? "";

        Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
        employees.Add(currentEmployee);
      }
      return employees;
    }

    async public static Task<List<Employee>> GetFromAPI()
    {
      List<Employee> employees = new List<Employee>();

      using (HttpClient client = new HttpClient())
      {
        string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
        JObject json = JObject.Parse(response);

        if (json.SelectToken("results") is JArray results)
        {
          foreach (JToken result in results)
          {
            string firstName = result.SelectToken("name.first")?.ToString() ?? "";
            string lastName = result.SelectToken("name.last")?.ToString() ?? "";
            int id = Int32.Parse(result.SelectToken("id.value")?.ToString().Replace("-", "") ?? "");
            string photoUrl = result.SelectToken("picture.large")?.ToString() ?? "";

            Employee emp = new Employee(firstName, lastName, id, photoUrl);
            employees.Add(emp);
          }
        }
      }

      return employees;
    }
  }
}