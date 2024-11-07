using Grpc.Net.Client;
using GrpcExample.Protos;

class Program
{
    static async Task Main(string[] args)
    {
        using var channel = GrpcChannel.ForAddress("https://localhost:5001");
        var client = new CustomerService.CustomerServiceClient(channel);

        Console.WriteLine("Enter Customer ID:");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            try
            {
                var response = await client.GetCustomerInfoAsync(new CustomerRequest { CustomerId = customerId });
                Console.WriteLine($"Customer Name: {response.Name}, Email: {response.Email}");
            }
            catch (Grpc.Core.RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
            {
                Console.WriteLine("Customer not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }
}
