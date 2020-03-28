using System;
using Xunit;
using ArcticKumquat.Math;

namespace ArcticKumquat.Math.Tests
{
    public class MatrixUnitTests
    {
        [Fact]
        public void TestScalarMultiply()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { (decimal?)0.7, (decimal?)2.5, (decimal?)0.0 },
                { (decimal?)1.9, (decimal?)-8.3, (decimal?)6.6 }
                });
            decimal k = 3.7m;
            Matrix expected = new Matrix(new decimal?[,] {
                { (decimal?)0.7*k, (decimal?)2.5*k, (decimal?)0.0*k },
                { (decimal?)1.9*k, (decimal?)-8.3*k, (decimal?)6.6*k }
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
        public void TestScalarMultiplyWithNull()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { (decimal?)0.7, (decimal?)2.5, (decimal?)3.1 },
                { null, (decimal?)8.3, (decimal?)6.6 }
                });
            decimal k = 3.7m;
            Matrix expected = new Matrix(new decimal?[,] {
                { (decimal?)0.7*k, (decimal?)2.5*k, (decimal?)3.1*k },
                { null, (decimal?)8.3*k, (decimal?)6.6*k }
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
                { (decimal?)0.7, (decimal?)2.5, (decimal?)0.0 },
                { (decimal?)-1.9, (decimal?)8.3, (decimal?)-6.6 }
                });
            Matrix b = new Matrix(new decimal?[,] {
                { (decimal?)2.1, (decimal?)0.0, (decimal?)0.0 },
                { (decimal?)-0.9, (decimal?)-8.3, (decimal?)9.9 }
                });
            Matrix expected = new Matrix(new decimal?[,] {
                { (decimal?)2.8, (decimal?)2.5, (decimal?)0.0 },
                { (decimal?)-2.8, (decimal?)0.0, (decimal?)3.3 }
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
        public void TestAddWithNull()
        {
            Matrix a = new Matrix(new decimal?[,] {
                { (decimal?)0.7, (decimal?)2.5, (decimal?)0.0 },
                { (decimal?)-1.9, (decimal?)8.3, (decimal?)-6.6 }
                });
            Matrix b = new Matrix(new decimal?[,] {
                { (decimal?)2.1, (decimal?)0.0, (decimal?)0.0 },
                { (decimal?)-0.9, (decimal?)null, (decimal?)9.9 }
                });
            Matrix expected = new Matrix(new decimal?[,] {
                { (decimal?)2.8, (decimal?)2.5, (decimal?)0.0 },
                { (decimal?)-2.8, (decimal?)8.3, (decimal?)3.3 }
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
                { (decimal?)0.7, (decimal?)2.5, (decimal?)0.0 },
                { (decimal?)-1.9, (decimal?)8.3, (decimal?)-6.6 }
                });
            Matrix b = new Matrix(new decimal?[,] {
                { (decimal?)2.1, (decimal?)0.0, (decimal?)0.0 },
                { (decimal?)-0.9, (decimal?)-8.3, (decimal?)9.9 },
                { (decimal?)-0.9, (decimal?)-8.3, (decimal?)9.9 }
                });

            var exc = Assert.Throws<ArgumentException>(() => Matrix.Add(a, b));
            Assert.Equal("Cannot add matrices as they don't have the same dimensions.", exc.Message);
        }
    }
}
