using Microsoft.EntityFrameworkCore;
using ShopReports.Models;
using ShopReports.Reports;

namespace ShopReports.Services
{
    public class ProductReportService
    {
        private readonly ShopContext shopContext;

        public ProductReportService(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public ProductCategoryReport GetProductCategoryReport()
        {
            // Query the database to retrieve all product categories and sort them by name
            var categories = shopContext.Categories
                .OrderBy(pc => pc.Name)
                .ToList();

            // Create a list to store report lines
            var reportLines = new List<ProductCategoryReportLine>();

            // Populate report lines with category data
            foreach (var category in categories)
            {
                var reportLine = new ProductCategoryReportLine
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name
                };
                reportLines.Add(reportLine);
            }

            // Create a ProductCategoryReport instance with the report lines
            var report = new ProductCategoryReport(reportLines, DateTime.Now);

            return report;
        }

        public ProductReport GetProductReport()
        {
            var reportLine = shopContext.Products
            .Select(p => new ProductReportLine
            {
                ProductId = p.Id,
                ProductTitle = p.Title.Title,
                Manufacturer = p.Manufacturer.Name,
                Price = p.UnitPrice
            }).OrderByDescending(p => p.ProductTitle).ToList();

            var report = new ProductReport(reportLine, DateTime.Now);
            return report;
        }

        public FullProductReport GetFullProductReport()
        {
            var reportLines = shopContext.Products
            .Select(p => new FullProductReportLine
            {
                ProductId = p.Id,
                Name = p.Title.Title,
                CategoryId = p.Title.CategoryId,
                Category = p.Title.Category.Name,
                Manufacturer = p.Manufacturer.Name,
                Price = p.UnitPrice
            })
            .OrderBy(line => line.Name)
            .ToList();

            var report = new FullProductReport(reportLines, DateTime.Now);
            return report;
        }

        public ProductTitleSalesRevenueReport GetProductTitleSalesRevenueReport()
        {
            var reportLines = shopContext.OrderDetails
            .GroupBy(od => od.Product.Title.Title)
            .Select(g => new ProductTitleSalesRevenueReportLine
            {
                ProductTitleName = g.Key,
                SalesRevenue = g.Sum(od => od.PriceWithDiscount),
                SalesAmount = g.Sum(od => od.ProductAmount)
            })
            .OrderByDescending(line => line.SalesRevenue)
            .ToList();

            var report = new ProductTitleSalesRevenueReport(reportLines, DateTime.Now);
            return report;
        }
    }
}
