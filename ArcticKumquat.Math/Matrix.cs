using System;

namespace ArcticKumquat.Math
{
    public class Matrix
    {
        public decimal?[,] Values { get; set; }
        public Matrix (decimal?[,] initialValues)
        {
            Values = initialValues;
        }

        public static Matrix ScalarMultiply(Matrix a, decimal k)
        {
            decimal?[,] values = a.Values;
            for (int i = 0; i < a.Values.GetUpperBound(0); i++)
            {
                for (int j = 0; j < a.Values.GetUpperBound(1); j++)
                {
                    values[i, j] *= k;
                }
            }
            Matrix b = new Matrix(values);
            return b;
        }

        public static Matrix Add(Matrix a, Matrix b)
        {
            if(!ValidateSameDimensions(a, b))
            {
                throw new ArgumentException("Cannot add matrices as they don't have the same dimensions.");
            }
            decimal?[,] sums = new decimal?[a.Values.GetUpperBound(0), a.Values.GetUpperBound(1)];
            for(int i = 0; i< a.Values.GetUpperBound(0); i++)
            {
                for(int j = 0; j < a.Values.GetUpperBound(1); j++)
                {
                    sums[i, j] = a.Values[i, j] + b.Values[i, j];
                }
            }
            Matrix c = new Matrix(sums);
            return c;
        }

        public static Matrix Subtract(Matrix a, Matrix b)
        {
            if (!ValidateSameDimensions(a, b))
            {
                throw new ArgumentException("Cannot subtract matrices as they don't have the same dimensions.");
            }

            Matrix c = ScalarMultiply(b, -1);
            Matrix d = Add(a, c);
            return d;
        }

        public static Matrix Transpose(Matrix a)
        {
            decimal?[,] transposed = new decimal?[a.Values.GetUpperBound(0), a.Values.GetUpperBound(1)];
            for (int i = 0; i < a.Values.GetUpperBound(0); i++)
            {
                for (int j = 0; j < a.Values.GetUpperBound(1); j++)
                {
                    transposed[j, i] = a.Values[i, j];
                }
            }
            Matrix b = new Matrix(transposed);
            return b;
        }

        public static bool ValidateSameDimensions(Matrix a, Matrix b)
        {
            if((a.Values.GetUpperBound(0)!=b.Values.GetUpperBound(0)
                || (a.Values.GetUpperBound(1) != b.Values.GetUpperBound(1))){
                return false;
            }
            return true;
        }

        public static bool ValidateCompatibleDimensions(Matrix a, Matrix b)
        {
            if (a.Values.GetUpperBound(1) != b.Values.GetUpperBound(0))
            {
                return false;
            }
            return true;
        }
    }
}
