﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIP
{
    abstract class Product
    {
        private string _name;
        private double _price;
        private int _productcode;
        private Manufacturer _manufacturer;
        //List which contains all used productcodes, is used to check to if the code is unique
        private static List<int> codeList = new List<int>(); 
        
        /// <summary>
        /// The abstact class Product 
        /// </summary>
        /// <param name="name">The name of the product</param>
        /// <param name="price">The price of the product</param>
        /// <param name="productcode">The productcode of the product</param>
        public Product(string name, double price, int productcode, Manufacturer manufacturer)
        {
            Name = name;
            Price = price;
            ProductCode = productcode;
            Manufacturer = manufacturer;
        }

        /// <summary>
        /// The name of the product
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                // the name must not be null or a blank string
                if (value != null && value != "") 
                    
                {
                    _name = value;

                }
            }
        }
        
        /// <summary>
        /// The price of the product
        /// </summary>
        public double Price
        {
            get
            {
                return _price;
            }

            set
            {
                // the price must not be null or a negative number
                if (value >= 0) 
                    
                {
                _price = value;
                }
            }  
        }

        /// <summary>
        /// Manufacturer of the product
        /// </summary>
        public Manufacturer Manufacturer
        {
            get
            {
                return _manufacturer;
            }

            set
            {
                // the price must not be null or a negative number
                if (value != null)
                {
                    _manufacturer = value;
                }
            }
        }

        
        /// <summary>
        /// The productcode of the product
        /// </summary>
        internal int ProductCode
        {
            get
            {
                return _productcode;
            }

            set
            {

                // the product code must be unique
                if (!codeList.Contains(value))
                {
                    codeList.Add(value);
                    _productcode = value;

                }
                else
                {
                    throw new InvalidOperationException("Duplicate product key");
                }
            }
        }

        /// <summary>
        /// Method for making a string to be listed in search
        /// </summary>
        /// <returns></returns>
        abstract public string ToSearchResultString();
        //The ToString function
        abstract public string ToPrint();
    }
}
