using System;
using Xunit;
using ArcticKumquat.Math;

namespace ArcticKumquat.Math.Tests
{
    public class MatrixUnitTests
    {
        [Fact]
        public void TestMatrixInitialization()
        {
            decimal?[,] initVals = new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { 1.9m, -8.3m, 6.6m }
                };

            Matrix actual = new Matrix(initVals);

            Assert.Equal(initVals.GetUpperBound(0), actual.Values.GetUpperBound(0));
            Assert.Equal(initVals.GetUpperBound(1), actual.Values.GetUpperBound(1));
            for (int i = 0; i <= initVals.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= initVals.GetUpperBound(1); j++)
                {
                    Assert.Equal(initVals[i, j], actual.Values[i, j]);
                }
            }
        }

        [Fact]
        public void TestMatrixInitializationWithNullArgument()
        {
            const string EXPECTED_MESSAGE = "initialValues";

            var exc = Assert.Throws<ArgumentNullException>(() => new Matrix(null));
            Assert.Contains(EXPECTED_MESSAGE, exc.Message);
        }

        [Fact]
        public void TestScalarMultiply()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { 1.9m, -8.3m, 6.6m }
                });
            decimal k = 3.7m;
            Matrix expected = new Matrix(new decimal?[,] {
                { 0.7m*k, 2.5m*k, 0.0m*k },
                { 1.9m*k, -8.3m*k, 6.6m*k }
                });

            Matrix actual = Matrix.ScalarMultiply(a, k);
            
            Assert.Equal(expected.Values.GetUpperBound(0), actual.Values.GetUpperBound(0));
            Assert.Equal(expected.Values.GetUpperBound(1), actual.Values.GetUpperBound(1));
            for (int i = 0; i <= expected.Values.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= expected.Values.GetUpperBound(1); j++)
                {
                    Assert.Equal(expected.Values[i, j], actual.Values[i, j]);
                }
            }
        }

        [Fact]
        public void TestScalarMultiplyWithNullValue()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 3.1m },
                { null, 8.3m, 6.6m }
                });
            decimal k = 3.7m;
            Matrix expected = new Matrix(new decimal?[,] {
                { 0.7m*k, 2.5m*k, 3.1m*k },
                { null, 8.3m*k, 6.6m*k }
                });

            Matrix actual = Matrix.ScalarMultiply(a, k);

            Assert.Equal(expected.Values.GetUpperBound(0), actual.Values.GetUpperBound(0));
            Assert.Equal(expected.Values.GetUpperBound(1), actual.Values.GetUpperBound(1));
            for (int i = 0; i <= expected.Values.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= expected.Values.GetUpperBound(1); j++)
                {
                    Assert.Equal(expected.Values[i, j], actual.Values[i, j]);
                }
            }
        }

        [Fact]
        public void TestAdd()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { -1.9m, 8.3m, -6.6m }
                });
            Matrix b = new Matrix(new decimal?[,] {
                { 2.1m, 0.0m, 0.0m },
                { -0.9m, -8.3m, 9.9m }
                });
            Matrix expected = new Matrix(new decimal?[,] {
                { 2.8m, 2.5m, 0.0m },
                { -2.8m, 0.0m, 3.3m }
                });

            Matrix actual = Matrix.Add(a, b);

            Assert.Equal(expected.Values.GetUpperBound(0), actual.Values.GetUpperBound(0));
            Assert.Equal(expected.Values.GetUpperBound(1), actual.Values.GetUpperBound(1));
            for (int i = 0; i <= expected.Values.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= expected.Values.GetUpperBound(1); j++)
                {
                    Assert.Equal(expected.Values[i, j], actual.Values[i, j]);
                }
            }
        }

        [Fact]
        public void TestAddWithNullValue()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { -1.9m, 8.3m, -6.6m }
                });
            Matrix b = new Matrix(new decimal?[,] {
                { 2.1m, 0.0m, 0.0m },
                { -0.9m, null, 9.9m }
                });
            Matrix expected = new Matrix(new decimal?[,] {
                { 2.8m, 2.5m, 0.0m },
                { -2.8m, 8.3m, 3.3m }
                });

            Matrix actual = Matrix.Add(a, b);

            Assert.Equal(expected.Values.GetUpperBound(0), actual.Values.GetUpperBound(0));
            Assert.Equal(expected.Values.GetUpperBound(1), actual.Values.GetUpperBound(1));
            for (int i = 0; i <= expected.Values.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= expected.Values.GetUpperBound(1); j++)
                {
                    Assert.Equal(expected.Values[i, j], actual.Values[i, j]);
                }
            }
        }

        [Fact]
        public void TestAddWithDifferentDimensions()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { -1.9m, 8.3m, -6.6m }
                });
            Matrix b = new Matrix(new decimal?[,] {
                { 2.1m, 0.0m, 0.0m },
                { -0.9m, -8.3m, 9.9m },
                { -0.9m, -8.3m, 9.9m }
                });
            const string EXPECTED_MESSAGE = "Cannot add matrices as they don't have the same dimensions.";

            var exc = Assert.Throws<ArgumentException>(() => Matrix.Add(a, b));
            Assert.Equal(EXPECTED_MESSAGE, exc.Message);
        }

        [Fact]
        public void TestAddNullReference()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { -1.9m, 8.3m, -6.6m }
                });
            const string EXPECTED_PARAMETER = "a or b";

            var exc = Assert.Throws<ArgumentNullException>(() => Matrix.Add(a, null));
            Assert.Contains(EXPECTED_PARAMETER, exc.Message);
        }

        [Fact]
        public void TestSubtract()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { -1.9m, 8.3m, -6.6m }
                });
            Matrix b = new Matrix(new decimal?[,] {
                { 2.1m, 0.0m, 0.0m },
                { -0.9m, -8.3m, -6.6m }
                });
            Matrix expected = new Matrix(new decimal?[,] {
                { -1.4m, 2.5m, 0.0m },
                { -1.0m, 16.6m, 0.0m }
                });

            Matrix actual = Matrix.Subtract(a, b);

            Assert.Equal(expected.Values.GetUpperBound(0), actual.Values.GetUpperBound(0));
            Assert.Equal(expected.Values.GetUpperBound(1), actual.Values.GetUpperBound(1));
            for (int i = 0; i <= expected.Values.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= expected.Values.GetUpperBound(1); j++)
                {
                    Assert.Equal(expected.Values[i, j], actual.Values[i, j]);
                }
            }
        }

        [Fact]
        public void TestSubtractWithNullValue()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { -1.9m, 8.3m, -6.6m }
                });
            Matrix b = new Matrix(new decimal?[,] {
                { 2.1m, 0.0m, 0.0m },
                { -0.9m, null, -6.6m }
                });
            Matrix expected = new Matrix(new decimal?[,] {
                { -1.4m, 2.5m, 0.0m },
                { -1.0m, 8.3m, 0.0m }
                });

            Matrix actual = Matrix.Subtract(a, b);

            Assert.Equal(expected.Values.GetUpperBound(0), actual.Values.GetUpperBound(0));
            Assert.Equal(expected.Values.GetUpperBound(1), actual.Values.GetUpperBound(1));
            for (int i = 0; i <= expected.Values.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= expected.Values.GetUpperBound(1); j++)
                {
                    Assert.Equal(expected.Values[i, j], actual.Values[i, j]);
                }
            }
        }

        [Fact]
        public void TestSubtractWithDifferentDimensions()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { -1.9m, 8.3m, -6.6m }
                });
            Matrix b = new Matrix(new decimal?[,] {
                { 2.1m, 0.0m, 0.0m },
                { -0.9m, -8.3m, 9.9m },
                { -0.9m, -8.3m, 9.9m }
                });
            const string EXPECTED_MESSAGE = "Cannot subtract matrices as they don't have the same dimensions.";

            var exc = Assert.Throws<ArgumentException>(() => Matrix.Subtract(a, b));
            Assert.Equal(EXPECTED_MESSAGE, exc.Message);
        }

        [Fact]
        public void TestSubtractNullReference()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { -1.9m, 8.3m, -6.6m }
                });
            const string EXPECTED_PARAMETER = "a or b";

            var exc = Assert.Throws<ArgumentNullException>(() => Matrix.Subtract(a, null));
            Assert.Contains(EXPECTED_PARAMETER, exc.Message);
        }

        [Fact]
        public void TestTranspose()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { 0.7m, 2.5m, 0.0m },
                { -1.9m, 8.3m, -6.6m }
                });
            Matrix expected = new Matrix(new decimal?[,]
            {
                { 0.7m, -1.9m },
                { 2.5m, 8.3m },
                { 0.0m,-6.6m }
            });

            Assert.Equal(expected.Values.GetUpperBound(0), a.Values.GetUpperBound(1));
            Assert.Equal(expected.Values.GetUpperBound(1), a.Values.GetUpperBound(0));
            for(int i = 0; i <= expected.Values.GetUpperBound(0); i++)
            {
                for(int j = 0; j <= expected.Values.GetUpperBound(1); j++)
                {
                    Assert.Equal(expected.Values[i, j], a.Values[j, i]);
                }
            }
        }

        [Fact]
        public void TestTransposeNullReference()
        {
            Assert.Throws<ArgumentNullException>(() => Matrix.Transpose(null));
        }
    }
}
