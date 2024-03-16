﻿using System;
using System.ComponentModel.DataAnnotations;
using ValidationAttributes.Models;


namespace ValidationAttributes
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var person = new Person
             (
                 null,
                 -1
             );

            bool isValidEntity = Units.Validator.IsValid(person);

            Console.WriteLine(isValidEntity);

        }
    }
}
