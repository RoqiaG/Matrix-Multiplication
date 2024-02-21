using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class MatrixMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 square matrices in an efficient way [Strassen's Method]
        /// </summary>
        /// <param name="M1">First square matrix</param>
        /// <param name="M2">Second square matrix</param>
        /// <param name="N">Dimension (power of 2)</param>
        /// <returns>Resulting square matrix</returns>
        static public int[,] MatrixMultiply(int[,] M1, int[,] M2, int N)
        {
            //add two matrices
            void add(int[,] mA, int[,] mB, int[,] mC, int index)
            {
                for (int i = 0; i < index; i++)
                {
                    for (int j = 0; j < index; j++)
                    {
                        mC[i, j] = mA[i, j] + mB[i, j];
                    }
                }
            }
            //sub two matrices
            void sub(int[,] mA, int[,] mB, int[,] mC, int index)
            {
                for (int i = 0; i < index; i++)
                {
                    for (int j = 0; j < index; j++)
                    {
                        mC[i, j] = mA[i, j] - mB[i, j];
                    }
                }
            }

            //m1
            int[,] result = new int[N, N];
            int[,] q11 = new int[N / 2, N / 2];
            int[,] q12 = new int[N / 2, N / 2];
            int[,] q21 = new int[N / 2, N / 2];
            int[,] q22 = new int[N / 2, N / 2];
            //m2
            int[,] b11 = new int[N / 2, N / 2];
            int[,] b12 = new int[N / 2, N / 2];
            int[,] b21 = new int[N / 2, N / 2];
            int[,] b22 = new int[N / 2, N / 2];
            int size = N / 2;



            if (N <= 64)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        result[i, j] = 0;
                        for (int k = 0; k < N; k++)
                        {
                            result[i, j] += M1[i, k] * M2[k, j];
                        }
                    }
                }
                return result;
            }
            //dividing the matrices into sub-matrices:
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    q11[i, j] = M1[i, j];
                    q12[i, j] = M1[i, j + size];
                    q21[i, j] = M1[i + size, j];
                    q22[i, j] = M1[i + size, j + size];

                    b11[i, j] = M2[i, j];
                    b12[i, j] = M2[i, j + size];
                    b21[i, j] = M2[i + size, j];
                    b22[i, j] = M2[i + size, j + size];
                }
            }

            // Calculating p1 to p7:
            int[,] result1 = new int[size, size];
            int[,] result2 = new int[size, size];
            int[,] P1 = new int[N / 2, N / 2];
            int[,] P2 = new int[N / 2, N / 2];
            int[,] P3 = new int[N / 2, N / 2];
            int[,] P4 = new int[N / 2, N / 2];
            int[,] P5 = new int[N / 2, N / 2];
            int[,] P6 = new int[N / 2, N / 2];
            int[,] P7 = new int[N / 2, N / 2];
            //p1
            sub(b12, b22, result2, size);
            P1 = MatrixMultiply(q11, result2, size);
            //p2
            add(q11, q12, result1, size);
            P2 = MatrixMultiply(result1, b22, size);
            //p3
            add(q21, q22, result1, size);
            P3 = MatrixMultiply(result1, b11, size);
            //p4
            sub(b21, b11, result2, size);
            P4 = MatrixMultiply(q22, result2, size);
            //p5
            add(q11, q22, result1, size);
            add(b11, b22, result2, size);
            P5 = MatrixMultiply(result1, result2, size);
            //p6
            sub(q12, q22, result1, size);
            add(b21, b22, result2, size);
            P6 = MatrixMultiply(result1, result2, size);
            //p7
            sub(q11, q21, result1, size);
            add(b11, b12, result2, size);
            P7 = MatrixMultiply(result1, result2, size);

            // calculating r11,r12,r21,r22:
            int[,] r11 = new int[N / 2, N / 2];
            int[,] r12 = new int[N / 2, N / 2];
            int[,] r21 = new int[N / 2, N / 2];
            int[,] r22 = new int[N / 2, N / 2];
            //r11
            add(P5, P4, result1, size);
            add(result1, P6, result2, size);
            sub(result2, P2, r11, size);
            //r12
            add(P1, P2, r12, size);
            //r21
            add(P3, P4, r21, size);
            //r22
            add(P5, P1, result1, size);
            sub(result1, P7, result2, size);
            sub(result2, P3, r22, size);

            // Grouping the results obtained in a single matrix:
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = r11[i, j];
                    result[i, j + size] = r12[i, j];
                    result[i + size, j] = r21[i, j];
                    result[i + size, j + size] = r22[i, j];
                }
            }
            return result;
        }
        #endregion
    }
}
