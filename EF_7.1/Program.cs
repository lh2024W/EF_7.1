using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EF_7._1
{
        
    public class Program
    {
        private static DatabaseService databaseService;
        static void Main()
        {
            databaseService = new DatabaseService();
            databaseService.EnsurePopulated();

            databaseService.PrintAllOrders();

            databaseService.RemoveOrderById(1);
        }
    }
}
            
            
            
    

