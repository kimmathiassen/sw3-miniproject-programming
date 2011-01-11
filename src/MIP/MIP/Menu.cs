﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MIP.Helpers;

namespace MIP
{
    class Menu
    {
        private List<string> _identifier;
        private Action _quit;
        private Action _back;
        private Action _main;
        static private Menu _menu;
        private string _quitCommand;
        private string _backCommand;
        private string _mainCommand;
        private string _quitText;
        private string _backText;
        private string _mainText;
        private int _commandLength;
        private string _seperator;

        private Menu(Action quit, Action back, Action main) : this()
        {
            _quit = quit;
            _back = back;
            _main = main;
        }

        private Menu()
        {
            _seperator = " - ";

            _quitText = "Quit";
            _backText = "Back";
            _mainText = "Go to Main";

            _quitCommand = "q";
            _backCommand = "b";
            _mainCommand = "m";
            _identifier = new List<string>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (c + "" == _quitCommand || c + "" == _backCommand || c + "" == _mainCommand)
                {
                    continue;
                }
                _identifier.Add(c+"");
            }

            _commandLength = 1;
        }

        static public Menu GetMenu
        {
            get
            {
                if (_menu == null)
                {
                    _menu = new Menu();
                }

                return _menu;
            }
        }

        public Action Quit
        {
            get
            {
                return _quit;
            }

            set
            {
                if (_quit == null)
                {
                    _quit = value;
                }
            }
        }

        public Action Main
        {
            get
            {
                return _main;
            }

            set
            {
                if (_main == null)
                {
                    _main = value;
                }
            }
        }

        public Action Back
        {
            get
            {
                return _back;
            }

            set
            {
                _back = value;   
            }
        }

        /// <summary>
        /// Writes out a menu which has the text specified in the list, and a delegate
        /// called.
        /// </summary>
        /// <param name="funcText"></param>
        /// <param name="back"></param>
        public void MakeMenu(List<KeyValuePair<Action, string>> funcText, Action back, KeyValuePair<Action, string> caller)
        {
            MakeMenu(funcText, back, caller, _identifier);

            return;
        }

        public void MakeMenu(List<KeyValuePair<Action, string>> funcText, Action back,
            KeyValuePair<Action, string> caller, List<string> identifier)
        {
            _back = back;
            if (funcText.Count > identifier.Count)
            {
                throw new ArgumentException("Too many inputs");
            }

            identifier.Add(_backCommand);
            funcText.Add(new KeyValuePair<Action, string>(_back, _backText));
            identifier.Add(_mainCommand);
            funcText.Add(new KeyValuePair<Action, string>(_main, _mainText));
            identifier.Add(_quitCommand);
            funcText.Add(new KeyValuePair<Action, string>(_quit, _quitText));

            MakeCleanMenu(funcText, caller, identifier);
        }

        public void MakeCleanMenu(List<KeyValuePair<Action, string>> funcText, Action back, KeyValuePair<Action, string> caller)
        {
            MakeCleanMenu(funcText, caller, _identifier);

            return;
        }

        public void MakeCleanMenu(List<KeyValuePair<Action, string>> funcText,
            KeyValuePair<Action, string> caller, List<string> identifier)
        {
            string input;
            if (funcText.Count > identifier.Count)
            {
                throw new ArgumentException("Too many inputs");
            }

            int maxLenght = 0;

            foreach (var item in funcText)
            {
                if (item.Value.Length > maxLenght)
                {
                    maxLenght = item.Value.Length;
                }
            }

            maxLenght += _commandLength + _seperator.Length;

            Console.WriteLine();

            for (int i = 0; i < funcText.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(identifier[i]);
                Console.ForegroundColor = ConsoleColor.White;
                for (int j = identifier[i].Length; j < _commandLength; j++)
                {
                    Console.Write(" ");
                }

                Console.Write(_seperator);
                Console.WriteLine(funcText[i].Value);
            }

            Console.WriteLine("Enter your choice:");
            int enterRow = Console.CursorTop;
            input = Console.ReadLine();
            while (true)
            {
                if (input == _quitCommand)
                {
                    Program.QuitBack = caller;
                }

                for (int i = 0; i < funcText.Count; i++)
                {
                    if (input == identifier[i])
                    {
                        funcText[i].Key();
                        return;
                    }
                }

                Console.SetCursorPosition(0, enterRow);
                for (int i = 0; i < input.Length; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(0, enterRow - 1);
                for (int i = 0; i < 80; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(0, enterRow - 1);
                Console.WriteLine("Incorrect identifier \"{0}\". Please try again:", input.Truncate(10));
                input = Console.ReadLine();
            }
        }
    }
}
