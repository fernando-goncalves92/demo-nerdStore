using NerdStore.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace NerdStore.Customer.API.ValueObjects
{
    public class Cpf
    {
        public const int Length = 11;
        public string Number { get; private set; }

        private static readonly HashSet<string> InvalidCpfs = new HashSet<string>(StringComparer.Ordinal)
        {
            "00000000000",
            "11111111111",
            "22222222222",
            "33333333333",
            "44444444444",
            "55555555555",
            "66666666666",
            "77777777777",
            "88888888888",
            "99999999999",
            "01234567890",
            "12345678909"
        };

        protected Cpf() { }

        public Cpf(string cpf)
        {
            if (!IsValid(cpf))
                throw new DomainException("CPF inválido!");

            Number = cpf;
        }

        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = cpf.PadLeft(11, '0');

            if (cpf is null || InvalidCpfs.Contains(cpf))
            {
                return false;
            }

            int v1 = 0;
            int v2 = 0;

            for (int i = 0; i < 9; i++)
            {
                int currentDigit = cpf[i] - '0';
                if (currentDigit < 0 || currentDigit > 9)
                    return false;

                v1 += currentDigit * (10 - i);
                v2 += currentDigit * (11 - i);
            }

            v1 = v1 % 11 < 2 ? 0 : 11 - v1 % 11;
            v2 += v1 * 2;
            v2 = v2 % 11 < 2 ? 0 : 11 - v2 % 11;

            char v1Char = (char)(v1 + '0');
            char v2Char = (char)(v2 + '0');

            return v1Char == cpf[9] && v2Char == cpf[10];
        }
    }
}
