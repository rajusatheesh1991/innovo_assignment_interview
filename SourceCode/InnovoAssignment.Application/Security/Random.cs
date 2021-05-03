using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace InnovoAssignment.Application.Security
{
    public class Random
    {


        public static byte[] GenerateRandomNumber(int length)
        {
           
            using(var randomNumberGeneraor=new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[length];
                randomNumberGeneraor.GetBytes(randomNumber);
                return randomNumber;
            }

          
        }



    }
}
