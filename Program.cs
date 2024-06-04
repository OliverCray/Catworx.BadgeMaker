using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatWorx.BadgeMaker
{
  class Program
  {
    async static Task Main(string[] args)
    {
      List<Employee> employees = new List<Employee>();

      Console.WriteLine("Would you like to fetch employees from the API? (yes/no/exit): ");
      string response = Console.ReadLine()?.ToLower() ?? "";
      while (response != "yes" && response != "no" && response != "exit")
      {
        Console.WriteLine("Please enter 'yes', 'no' or 'exit': ");
        response = Console.ReadLine()?.ToLower() ?? "";
      }
      if (response == "exit")
      {
        return;
      }
      else if (response == "yes")
      {
        employees = await PeopleFetcher.GetFromAPI();
      }
      else if (response == "no")
      {
        employees = PeopleFetcher.GetEmployees();
      }

      Util.PrintEmployees(employees);
      Util.MakeCSV(employees);
      await Util.MakeBadges(employees);
    }
  }
}