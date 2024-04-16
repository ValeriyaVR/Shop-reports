using Microsoft.EntityFrameworkCore;
using ShopReports.Models;
using ShopReports.Reports;

namespace ShopReports.Services
{
    public class CustomerReportService
    {
        private readonly ShopContext shopContext;

        public CustomerReportService(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public CustomerSalesRevenueReport GetCustomerSalesRevenueReport()
        {
            var reportLines = shopContext.Customers
            .Select(customer => new CustomerSalesRevenueReportLine
            {
                CustomerId = customer.Id,
                PersonFirstName = customer.Person.FirstName,
                PersonLastName = customer.Person.LastName,
                SalesRevenue = customer.Orders.Sum(order =>
                    order.Details.Sum(detail => detail.PriceWithDiscount))
            }).Where(customer => customer.SalesRevenue > 0)
            .OrderByDescending(line => line.SalesRevenue)
            .ToList();

            var report = new CustomerSalesRevenueReport(reportLines, DateTime.Now);
            return report;
        }
    }
}
