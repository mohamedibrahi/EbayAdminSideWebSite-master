﻿namespace EbayAdminRepository.Products
{
    using EbayAdminDbContext;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductRepository : IProductRepository
    {
        private readonly myDbContext _context;
        public ProductRepository(myDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddProductAsync(Product product)
        {
            await _context.AddAsync(product);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch(Exception )
            {

            }
            return product.ProductId;
        }

        public async Task<int> DeleteProductAsync(Product product)
        {
            _context.Remove(product);
           await _context.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task<int> GetProductCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<Product> GetProductDetailsAsync(int value)
        {
            return await _context.Products
                                .Include(p=>  p.Admin )
                                .Include(p=>  p.brands) // add by aly
                                .Include(p=>  p.category)
                                .Include(p=>  p.subCategory)
                                .Include(p=>  p.stock)
                                .Include(p=>  p.rates)
                                .Include(p=>  p.comments)
                                .Include(p=>  p.productImgs)
                                .Where(p => p.ProductId == value)
                                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<List<Product>> GetProductsAsyncWithSearch(string SearchText)
        {
            return await _context.Products.Where(c => c.Name.Contains(SearchText)).ToListAsync();
        }
        public async Task<List<Product>> GetProductsAsyncWithSort(string SortProperty, SortOrder sortOrder)
        {
            List < Product > products= await _context.Products.ToListAsync();
           // if (SortProperty.ToLower()== "Name")
            if (SortProperty== "Name")
                {
                if (sortOrder==SortOrder.Ascending)
                {
                    products = products.OrderBy(n=>n.Name).ToList();
                }
                else
                {
                    products = products.OrderByDescending(n => n.Name).ToList();
                }
            }
            //else if (SortProperty.ToLower() == "Price")
            else if (SortProperty == "Price")
                    {
                if (sortOrder == SortOrder.Ascending)
                {
                    products = products.OrderBy(n => n.Price).ToList();
                }
                else
                {
                    products = products.OrderByDescending(n => n.Price).ToList();
                }
            }
            else
            {
                if (sortOrder == SortOrder.Ascending)
                {
                    products = products.OrderBy(n => n.Quantity).ToList();
                }
                else
                {
                    products = products.OrderByDescending(n => n.Quantity).ToList();
                }
            }
            return products;
        }
        public async Task<int> UpdateProductAsync(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return product.ProductId;
        }
    }
}
