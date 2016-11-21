using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAndCAssignment.Models
{
    public class CreditCardValidation
    {
       
        public bool IsValidCreditCard(string creditCardNumber)
        {


            if (string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }


            /* 
            .Where method is an extension method of the IEnumerable<T> interface(which extends IEnumerable). The string class implements the IEnumerable<char> and so is a subclass and so inherits the .Where method.
            The .Where method takes a function as a parameter. In this case an anonymous delegate is passed in the form of a lambda expression. The .Where method takes a function with a char as a parameter and a bool as a return type. It returns
            an IEnumerable object with only numbers(all non numbers have been removed)
            The Lambda expression checks if the char is only a number and return a bool - true or false. In the case where any char is false, its discarded. Same as the method IsNumber(char c) below.
            
            
            */
            IEnumerable<char> numbersOnly = creditCardNumber.Where(c => c >= '0' && c <= '9');



            /* The .Reverse method reverses the order of the elements in this sequence. */

            IEnumerable<char> numbersReversed = numbersOnly.Reverse();

            /* 
            The .Select method projects each element of a sequance into a new form by incorporating the elements index.
            The anonymous delegate typecasts the char into its' integer value (as a char is an integer value). It then subtracts 48 to convert it to the same int value.
            The i variable is the index withing the sequence. The ternary operator returns 2 for every other number in the sequence.
            */

            IEnumerable<int> doubleEveryOther = numbersReversed.Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2));

            /* 
            The .Sum method computes the sum of the sequence of values that are obtained by invoking a transform function on each element of the input sequence. 
            The anonymous delegate returns the sum of each individual element as per Luhn's algorithm such as 10 = 1 and 12 = 3
            */
            int s = doubleEveryOther.Sum((c) => c / 10 + c % 10);


            /* 
            If the final result is divisible by 10 then it's a valid credit card number
            The != 0 check is incase the user only enters non numeric characters and the sequence then is of length zero from the start.
            */
            return s % 10 == 0 && s != 0;


        }


        /* Method for demonstration purposes. No lambda or anonymous functions used. */
        public  bool IsValidCreditCardSimple(string creditCardNumber)
        {


            if (string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }



            IEnumerable<char> credCardChar = creditCardNumber.Where(IsNumber);
            IEnumerable<char> credCardCharRev = credCardChar.Reverse();
            IEnumerable<int> s = credCardCharRev.Select(SelectMethod);
            int answer = s.Sum(SumMethod);

            return answer % 10 == 0 && answer != 0;


        }


        public  bool IsNumber(char c)
        {


            if (c >= '0' && c <= '9') return true;
            else return false;


        }

        public  int SelectMethod(char c, int x)
        {
            int y = ((int)c - 48) * (x % 2 == 0 ? 1 : 2);

            return y;
        }

        public  int SumMethod(int x)
        {
            int y = x / 10 + x % 10;

            return y;
        }

    }
}