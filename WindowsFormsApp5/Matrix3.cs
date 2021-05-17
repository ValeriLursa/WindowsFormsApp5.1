using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLibrary
{
    /// <summary>
    ///   Класс матрицы 3x3 для 2D графики
    /// </summary>
    public class Matrix3
    {
	/// <summary>
	///   внутренний массив матрицы
	/// </summary>
        double[,] matrix;

	/// <summary>
	///   Создает новую единичную матрицу
	/// </summary>
        public Matrix3()
        {
            matrix = new double[3, 3];
            Identity();
        }

	/// <summary>
	///   Устанавливает единицы по главной диагонали, остальные значения - 0
	/// </summary>
        public void Identity()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i==j)
                    {
                        matrix[i, j] = 1;
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
        }

	/// <summary>
	///   Создает матрицу переноса
	/// </summary>
        /// <param name="tx">величина перемещения по оси x</param>
        /// <param name="ty">величина перемещения по оси y</param>
        public void Translate(double tx, double ty)
        {
            matrix[0, 2] = tx;
            matrix[1, 2] = ty;
        }

	/// <summary>
	///   Создает матрицу масштабирования
	/// </summary>
        /// <param name="sx">коэффициент масштабирования по оси x</param>
        /// <param name="sy">коэффициент масштабирования по оси y</param>
        public void Scale(double sx, double sy)
        {
            matrix[0, 0] = sx;
            matrix[1, 1] = sy;
        }

	/// <summary>
	///   Создает матрицу поворота
	/// </summary>
        /// <param name="angle">угол поворота против часовой стрелки в радианах</param>
        public void Rotate(double angle)
        {
            matrix[0, 0] = Math.Cos(angle);
            matrix[0, 1] = -Math.Sin(angle);
            matrix[1, 0] = Math.Sin(angle);
            matrix[1, 1] = Math.Cos(angle);
        }

	/// <summary>
	///   Умножение текущей матрицы на вектор
	/// </summary>
        /// <param name="v">вектор на который умножается матрица</param>
        /// <returns>вектор, полученный в результате произведения</returns>
        public Vector3 Mult(Vector3 v)
        {
            return new Vector3(
                v.vect[0] * matrix[0, 0] + v.vect[1] * matrix[0, 1] + matrix[0, 2],
                v.vect[0] * matrix[1, 0] + v.vect[1] * matrix[1, 1] + matrix[1, 2]);
        }
	/// <summary>
	///   Умножение текущей матрицы на матрицу
	/// </summary>
        /// <param name="m">матрица на которую умножается текущая матрица</param>
	public void Mult(Matrix3 m)
        {
            double[,] tempM = new double[3,3];
            tempM[0, 0] = matrix[0, 0] * m.matrix[0, 0] + matrix[0, 1] * m.matrix[1, 0] + matrix[0, 2] * m.matrix[2, 0];
            tempM[0, 1] = matrix[0, 0] * m.matrix[0, 1] + matrix[0, 1] * m.matrix[1, 1] + matrix[0, 2] * m.matrix[2, 1];
            tempM[0, 2] = matrix[0, 0] * m.matrix[0, 2] + matrix[0, 1] * m.matrix[1, 2] + matrix[0, 2] * m.matrix[2, 2];
            tempM[1, 0] = matrix[1, 0] * m.matrix[0, 0] + matrix[1, 1] * m.matrix[1, 0] + matrix[1, 2] * m.matrix[2, 0];
            tempM[1, 1] = matrix[1, 0] * m.matrix[0, 1] + matrix[1, 1] * m.matrix[1, 1] + matrix[1, 2] * m.matrix[2, 1];
            tempM[1, 2] = matrix[1, 0] * m.matrix[0, 2] + matrix[1, 1] * m.matrix[1, 2] + matrix[1, 2] * m.matrix[2, 2];
            tempM[2, 0] = matrix[2, 0] * m.matrix[0, 0] + matrix[2, 1] * m.matrix[1, 0] + matrix[2, 2] * m.matrix[2, 0];
            tempM[2, 1] = matrix[2, 0] * m.matrix[0, 1] + matrix[2, 1] * m.matrix[1, 1] + matrix[2, 2] * m.matrix[2, 1];
            tempM[2, 2] = matrix[2, 0] * m.matrix[0, 2] + matrix[2, 1] * m.matrix[1, 2] + matrix[2, 2] * m.matrix[2, 2];
            matrix[0, 0] = tempM[0, 0];
            matrix[0, 1] = tempM[0, 1];
            matrix[0, 2] = tempM[0, 2];
            matrix[1, 0] = tempM[1, 0];
            matrix[1, 1] = tempM[1, 1];
            matrix[1, 2] = tempM[1, 2];
            matrix[2, 0] = tempM[2, 0];
            matrix[2, 1] = tempM[2, 1];
            matrix[2, 2] = tempM[2, 2];
        }
    }
}
