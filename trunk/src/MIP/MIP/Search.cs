﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIP
{
    static class Search
    {
        private static List<Product> currentResult;
        private class ProductSorter : IComparer<Product>
        {
            public int Compare(Product a, Product b)
            {
                if (a is InternalHarddrive)
                {
                    if (b is InternalHarddrive)
                    {
                        return 0;
                    }
                    return -1;
                }
                
                if (a is ExternalHarddrive)
                {
                    if (b is ExternalHarddrive)
                    {
                        return 0;
                    }
                    return -1;
                }

                if (a is FlashStorage)
                {
                    if (b is FlashStorage)
                    {
                        return 0;
                    }
                    return -1;
                }

                return 1;
            }
        }

        public static List<Product> Initiate()
        {
            currentResult = Parser.GetList;

            currentResult.Sort(new ProductSorter());

            return currentResult;
        }

        public static List<Product> SearchProductCode(List<Product> searchList, int code)
        {
            currentResult = searchList.Where(x => x.ProductCode == code).ToList();
            return currentResult;
        }

        public static List<Product> SearchProductCode(int code)
        {
            return SearchProductCode(currentResult,code);
        }

        public static List<Product> SearchPriceRange(List<Product> searchList, double min, double max)
        {
            currentResult = searchList.Where(x => x.Price >= min && x.Price <= max).ToList();
            return currentResult;
        }

        public static List<Product> SearchPriceRange(double min, double max)
        {
            return SearchPriceRange(currentResult,min,max);
        }

        public static List<Product> SearchStarageRange(List<Product> searchList, int min, int max)
        {
            currentResult = searchList.Where(x => x is StorageUnit && (x as StorageUnit).Storage >= min && (x as StorageUnit).Storage <= max).ToList();
            return currentResult;
        }

        public static List<Product> SearchStarageRange(int min, int max)
        {
            return SearchStarageRange(currentResult,min,max);
        }

        public static List<Product> SearchStorageRange(List<Product> searchList, string searchString)
        {
            currentResult = searchList.Where(x => x.Name.Contains(searchString) || x.Manufacturer.Name.Contains(searchString)).ToList();
            return currentResult;
        }

        public static List<Product> SearchStorageRange(string searchString)
        {
            return SearchStorageRange(currentResult,searchString);
        }
    }
}
