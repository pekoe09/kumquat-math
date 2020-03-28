using System;

namespace ArcticKumquat.Math
{
    /// <summary>
    /// Class defining a matrix object and operations that can be performed 
    /// with one or more matrices.
    /// </summary>
    public class Matrix
    {
        private decimal?[,] _values;

        /// <value>Gets the values of the matrix as a two-dimensional array 
        /// of nullable decimals</value>
        public decimal?[,] Values
        {
            get { return _values; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("initialValues");
                }
                _values = value;
            }
        }

        /// <summary>
        /// Constructor for a Matrix object.
        /// </summary>
        /// <param name="initialValues">Values of the matrix as a two-dimensional
        /// array of nullable decimals.</param>
        /// <exception cref="ArgumentNullException">Thrown if a null ref is
        /// provided as an argument.</exception>
        public Matrix (decimal?[,] initialValues)
        {
            Values = initialValues;
        }

        /// <summary>
        /// Scalar multiplication operation. Accepts any valid Matrix object, 
        /// even one with null values. If a value in the Matrix is null, 
        /// the multiplication yields null in the corresponding location in
        /// the new Matrix.
        /// </summary>
        /// <param name="a">A Matrix object; can contain null values.</param>
        /// <param name="k">Any non-null decimal number.</param>
        /// <returns>A new Matrix object representing the result of the
        /// scalar multiplication.</returns>
        public static Matrix ScalarMultiply(Matrix a, decimal k)
        {
            decimal?[,] values = a.Values;
            int ab = a.Values.GetUpperBound(0);
            int bb = a.Values.GetUpperBound(1);
            for (int i = 0; i <= a.Values.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= a.Values.GetUpperBound(1); j++)
                {
                    values[i, j] *= k;
                }
            }
            Matrix b = new Matrix(values);
            return b;
        }

        /// <summary>
        /// Addition of two matrices. Any null values in either of the matrices
        /// are converted into zeros.
        /// </summary>
        /// <param name="a">A Matrix object; can contain null values.</param>
        /// <param name="b">A Matrix object; can contain null values.</param>
        /// <returns>A new Matrix object representing the result of the
        /// summation of the matrices given as arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if either of the
        /// arguments is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the matrices provided
        /// as arguments have mismatching dimensions.</exception>
        public static Matrix Add(Matrix a, Matrix b)
        {
            if(a == null || b == null)
            {
                throw new ArgumentNullException("a or b");
            }
            if(!ValidateSameDimensions(a, b))
            {
                throw new ArgumentException("Cannot add matrices as they don't have the same dimensions.");
            }
            decimal?[,] sums = new decimal?[a.Values.GetUpperBound(0) + 1, a.Values.GetUpperBound(1) + 1]; ;
            for(int i = 0; i <= a.Values.GetUpperBound(0); i++)
            {
                for(int j = 0; j <= a.Values.GetUpperBound(1); j++)
                {
                    sums[i, j] = (a.Values[i, j] ??= 0) + (b.Values[i, j] ??= 0);
                }
            }
            Matrix c = new Matrix(sums);
            return c;
        }

        /// <summary>
        /// Substraction of two matrices (a - b). Any null values in either of the 
        /// matrices are converted into zeros.
        /// </summary>
        /// <param name="a">A Matrix object; can contain null values.</param>
        /// <param name="b">A Matrix object substracted from the first matrix; 
        /// can contain null values.</param>
        /// <returns>A new Matrix object representing the result of the
        /// substraction of the matrices given as arguments.</returns>
        /// <exception cref="ArgumentNullException">Thrown if either of the
        /// arguments is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the matrices provided
        /// as arguments have mismatching dimensions.</exception>
        public static Matrix Subtract(Matrix a, Matrix b)
        {
            if (a == null || b == null)
            {
                throw new ArgumentNullException("a or b");
            }
            if (!ValidateSameDimensions(a, b))
            {
                throw new ArgumentException("Cannot subtract matrices as they don't have the same dimensions.");
            }

            Matrix c = ScalarMultiply(b, -1);
            Matrix d = Add(a, c);
            return d;
        }

        /// <summary>
        /// A transpose operation on a matrix.
        /// </summary>
        /// <param name="a">A Matrix object; can contain null values.</param>
        /// <returns>A new Matrix object with the rows and columns transposed.</returns>
        public static Matrix Transpose(Matrix a)
        {
            if(a == null)
            {
                throw new ArgumentNullException("a");
            }
            decimal?[,] transposed = new decimal?[a.Values.GetUpperBound(0) + 1, a.Values.GetUpperBound(1) + 1];
            for (int i = 0; i <= a.Values.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= a.Values.GetUpperBound(1); j++)
                {
                    transposed[j, i] = a.Values[i, j];
                }
            }
            Matrix b = new Matrix(transposed);
            return b;
        }

        /// <summary>
        /// Validation operation that ensures that the matrices provided as arguments
        /// both have the same dimensions (so that e.g. addition operation can be
        /// performed on them).
        /// </summary>
        /// <param name="a">A Matrix object; can contain null values.</param>
        /// <param name="b">A Matrix object; can contain null values.</param>
        /// <returns>True if the dimensions are the same, otherwise false.</returns>
        public static bool ValidateSameDimensions(Matrix a, Matrix b)
        {
            if((a.Values.GetUpperBound(0)!=b.Values.GetUpperBound(0))
                || (a.Values.GetUpperBound(1) != b.Values.GetUpperBound(1)))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validation operation that ensures that the matrices provided as arguments
        /// are compatible by their dimensions for a matrix multiplication operation
        /// where a is the first matrix and b is the second one.
        /// </summary>
        /// <param name="a">A Matrix object; can contain null values.</param>
        /// <param name="b">A Matrix object; can contain null values.</param>
        /// <returns>True if the second dimension of matrix a equals the first
        /// dimension of matrix b.</returns>
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
