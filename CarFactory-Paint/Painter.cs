using System;
using System.Collections.Generic;
using CarFactory_Domain;
using CarFactory_Factory;

namespace CarFactory_Paint
{
    public class Painter : IPainter
    {
        public Car PaintCar(Car car, PaintJob paint)
        {
            if (car.Chassis == null) throw new Exception("Cannot paint a car without chassis");

            /*
             * Mix the paint
             * 
             * Unfortunately the paint mixing instructions needs to be unlocked
             * And we don't know the password!
             * 
             * Please only touch the "FindPaintPassword" function
             * 
             */
            var (passwordLength, encodedPassword) = paint.CreateInstructions();
            var solution = FindPaintPassword(passwordLength, encodedPassword);

            if (!paint.TryUnlockInstructions(solution))
            {
                throw new Exception("Could not unlock paint instructions");
            }
            car.PaintJob = paint;
            return car;
        }

        private static string FindPaintPassword(int passwordLength, long encodedPassword)
        {
            var rd = new Random();
            string CreateRandomString()
            {
                char[] chars = new char[passwordLength];

                for (int i = 0; i < passwordLength; i++)
                {
                    chars[i] = PaintJob.ALLOWED_CHARACTERS[rd.Next(0, PaintJob.ALLOWED_CHARACTERS.Length)];
                }

                return new string(chars);
            }
            string str = CreateRandomString();
            while (PaintJob.EncodeString(str) != encodedPassword) str = CreateRandomString();
            return str;

            //var test = new char[passwordLength];

            //for(int i = 0; i < test.Length; i++)
            //{
            //    test[i] = PaintJob.ALLOWED_CHARACTERS[0];
            //}

            //var solution = RecursivePermutation(passwordLength - 1, test, encodedPassword);
            //return solution;
        }

        private static string RecursivePermutation(int index, char[] password, long encodedPassword)
        {
            for(int i = 0; i < PaintJob.ALLOWED_CHARACTERS.Length; i++)
            {
                password[index] = PaintJob.ALLOWED_CHARACTERS[i];

                var str = new string(password);
                if(PaintJob.EncodeString(str) == encodedPassword)
                {
                    return new string(password);
                }

                if(index > 0)
                {
                    RecursivePermutation(index - 1, password, encodedPassword);
                }
            }

            return new string(password);
        }
    }
}