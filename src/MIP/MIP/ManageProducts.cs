﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIP
{
    class ManageProducts
    {
        static public void ManageProduct()
        {
            Console.Clear();
            List<KeyValuePair<Action, string>> list = new List<KeyValuePair<Action, string>>();
            list.Add(new KeyValuePair<Action, string>(AddProduct, "Add a new product"));
            list.Add(new KeyValuePair<Action, string>(RemoveProduct, "Remove an existing product"));

            MenuBuilder.GetMenu.MakeMenu(list, Program.MainMenu, new KeyValuePair<Action, string>(ManageProduct, "Manage Products"));
            Program.MainMenu();
        }

        static public void AddProduct()
        {
            Console.Clear();
            List<KeyValuePair<Action, string>> list = new List<KeyValuePair<Action, string>>();
            list.Add(new KeyValuePair<Action, string>(AddInternalHarddrive, "Add internal harddrive"));
            list.Add(new KeyValuePair<Action, string>(AddExternalHarddrive, "Add external harddrive"));
            list.Add(new KeyValuePair<Action, string>(AddFlashStorage, "Add flash storage device"));

            MenuBuilder.GetMenu.MakeMenu(list, ManageProduct, new KeyValuePair<Action, string>(AddProduct, "Add Product"));
            ManageProduct();
        }

        static void AddInternalHarddrive()
        {
            Console.Clear();
            Console.WriteLine("Adding internal harddrive.\n");
            string name = Toolbox.GetString("Enter name:", x => x.Length >= 5);
            double price = Toolbox.GetDouble("Enter price(eg. 1200,50):");
            int productCode = Toolbox.GetInt("Enter product code(must be unique):", x =>
                Parser.ProductList.FirstOrDefault(y => y.ProductCode == x) == null);
            string manu = Toolbox.GetString("Enter product manufacturer(eg. " +
                Parser.ManufacturerList.FirstOrDefault(x => true).Name + "):",
                x => Parser.ManufacturerList.FirstOrDefault(y => y.Name.ToUpper() == x.ToUpper()) != null);
            int store = Toolbox.GetInt("Enter storage capacity(GB):", x => x > 0);
            int rpm = Toolbox.GetInt("Enter rpm(eg. 4200):", x => Enum.IsDefined(typeof(ERPM), x));
            double form = Toolbox.GetDouble("Enter form factor(eg. 2,5):", x => Enum.IsDefined(typeof(EFormFactor), (int)(x * 100)));

            InternalHarddrive temp = new InternalHarddrive(
                name,
                price,
                productCode,
                Parser.ManufacturerList.FirstOrDefault(x => x.Name.ToUpper() == manu.ToUpper()),
                store,
                rpm,
                form);

            Parser.ProductList.Add(temp);
            Console.WriteLine("Internal harddrive with above specifications added. Press any key to continue.");
            Console.ReadKey();
        }

        static void AddExternalHarddrive()
        {
            Console.Clear();
            Console.WriteLine("Adding external harddrive.\n");
            string name = Toolbox.GetString("Enter name:", x => x.Length >= 5);
            double price = Toolbox.GetDouble("Enter price(eg. 1200,50):");
            int productCode = Toolbox.GetInt("Enter product code(must be unique):", x =>
                Parser.ProductList.FirstOrDefault(y => y.ProductCode == x) == null);
            string manu = Toolbox.GetString("Enter product manufacturer(eg. " +
                Parser.ManufacturerList.FirstOrDefault(x => true).Name + "):",
                x => Parser.ManufacturerList.FirstOrDefault(y => y.Name.ToUpper() == x.ToUpper()) != null);
            int store = Toolbox.GetInt("Enter storage capacity(GB):", x => x > 0);
            int rpm = Toolbox.GetInt("Enter rpm(eg. 4200):", x => Enum.IsDefined(typeof(ERPM), x));
            double height = Toolbox.GetDouble("Enter height:");
            double width = Toolbox.GetDouble("Enter width:");
            double depth = Toolbox.GetDouble("Enter depth:");

            ExternalHarddrive temp = new ExternalHarddrive(
                name,
                price,
                productCode,
                Parser.ManufacturerList.FirstOrDefault(x => x.Name.ToUpper() == manu.ToUpper()),
                store,
                rpm,
                height,
                width,
                depth);

            Parser.ProductList.Add(temp);
            Console.WriteLine("External harddrive with above specifications added. Press any key to continue.");
            Console.ReadKey();
        }

        static void AddFlashStorage()
        {
            Console.Clear();
            Console.WriteLine("Adding flash storage unit.\n");
            string name = Toolbox.GetString("Enter name:", x => x.Length >= 5);
            double price = Toolbox.GetDouble("Enter price(eg. 1200,50):");
            int productCode = Toolbox.GetInt("Enter product code(must be unique):", x =>
                Parser.ProductList.FirstOrDefault(y => y.ProductCode == x) == null);
            string manu = Toolbox.GetString("Enter product manufacturer(eg. " +
                Parser.ManufacturerList.FirstOrDefault(x => true).Name + "):",
                x => Parser.ManufacturerList.FirstOrDefault(y => y.Name.ToUpper() == x.ToUpper()) != null);
            int store = Toolbox.GetInt("Enter storage capacity(GB):", x => x > 0);
            bool secure = Toolbox.GetBool("Enter secure usb(true/false):");

            FlashStorage temp = new FlashStorage(
                name,
                price,
                productCode,
                Parser.ManufacturerList.FirstOrDefault(x => x.Name.ToUpper() == manu.ToUpper()),
                store,
                secure);

            Parser.ProductList.Add(temp);
            Console.WriteLine("Flash storage unit with above specifications added. Press any key to continue.");
            Console.ReadKey();
        }

        static public void RemoveProduct()
        {
            var searchResult = Parser.ProductList;
            Console.Clear();
            int i = 1;
            List<KeyValuePair<Action, string>> list = new List<KeyValuePair<Action, string>>();
            List<string> identifier = new List<string>();
            foreach (var item in searchResult)
            {
                list.Add(new KeyValuePair<Action, string>(RemoveSpecificProduct, item.ToSearchResultString()));
                identifier.Add(i + "");
                i++;
            }

            if (list.Count > 0)
            {
                MenuBuilder.GetMenu.MakeMenu(list, ManageProduct, new KeyValuePair<Action, string>(RemoveProduct, "Remove Product"), identifier,
                    "Select the number of the product you wish to remove.\n");
            }
            else
            {
                MenuBuilder.GetMenu.MakeMenu(list, ManageProduct, new KeyValuePair<Action, string>(RemoveProduct, "Remove Product"), identifier,
                    "\nNo products were found!\n");
            }
        }

        static public void RemoveSpecificProduct()
        {
            int index = int.Parse(MenuBuilder.GetMenu.LastSelected);
            var temp = Parser.ProductList[index - 1];
            Parser.ProductList.Remove(temp);
            Console.WriteLine("\"" + temp.ToSearchResultString() + "\" has been removed. Press any key to continue.");
            Console.ReadKey();
            ManageProduct();
            return;
        }
    }
}