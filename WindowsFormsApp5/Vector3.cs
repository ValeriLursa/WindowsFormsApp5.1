using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLibrary
{
    /// <summary>
    ///   Вектор с 3 компонентами для 2D графики
    /// </summary>
    public class Vector3
    {
	/// <summary>
	///   внутренний массив
	/// </summary>
        public double[] vect;

	/// <summary>
	///   Создает вектор
	/// </summary>
        /// <param name="x">координата x вектора</param>
        /// <param name="y">координата y вектора</param>
        public Vector3(double x, double y)
        {
            vect = new double[3];
            vect[0] = x;
            vect[1] = y;
            vect[2] = 1.0;
        }
    }
}
