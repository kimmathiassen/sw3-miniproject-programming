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
        //List which contains all used productcodes, is used to check to if the code is unique
        private static List<int> codeList = new List<int>(); 
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="productcode"></param>
        public Product(string name, double price, int productcode)
        {
            Name = name;
            Price = price;
            ProductCode = productcode;

        }

        /// <summary>
        /// 
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
        /// 
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
                if (value != null && value >= 0) 
                    
                {
                _price = value;
                }
            }  
        }

        // the ProcutCode is an int 
        /// <summary>
        /// 
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
            }
        }




    }
}
