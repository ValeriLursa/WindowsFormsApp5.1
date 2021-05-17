using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLibrary
{
    /// <summary>
    ///   Вершина для 2D моделей
    /// </summary>
    public struct Vertex
    {
	public double x, y;
    }
    
    /// <summary>
    ///   Класс модели, содержащий вершины
    /// </summary>
    public class Model
    {
	/// <summary>
	///   Матрица для трансформаций
	/// </summary>
        Matrix3 matrix;
	/// <summary>
	///   Список вершин модели
	/// </summary>
        public List<Vertex> vertices;
	/// <summary>
	///   Цвет контура
	/// </summary>
        public int color;

	/// <summary>
	///   Создает пустую модель
	/// </summary>
        /// <param name="color">цвет контура</param>
        public Model(int color)
        {
            vertices = new List<Vertex>();
            matrix = new Matrix3();
            this.color = color;
        }
	
	/// <summary>
	///   Добавляет вершину в модель
	/// </summary>
        /// <param name="x">координата x вершины</param>
        /// <param name="y">координата y вершины</param>
        public void AddVertex(double x, double y)
        {
            vertices.Add(new Vertex { x = x, y = y });
        }

	/// <summary>
	///   Перемещение модели
	/// </summary>
        /// <param name="tx">величина перемещения по оси x</param>
        /// <param name="ty">величина перемещения по оси y</param>
        public void Translate(double tx, double ty)
        {
            matrix.Translate(tx, ty);
            CalcVertexes();
        }

	/// <summary>
	///   Масштабирование модели
	/// </summary>
        /// <param name="sx">коэффициент масштабирования по оси x</param>
        /// <param name="sy">коэффициент масштабирования по оси y</param>
        public void Scale(double sx, double sy)
        {
            matrix.Scale(sx, sy);
            CalcVertexes();
        }

	/// <summary>
	///   Поворот модели
	/// </summary>
        /// <param name="angle">угол поворота против часовой стрелки в радианах</param>
        public void Rotate(double angle)
        {
            matrix.Rotate(angle);
            CalcVertexes();
        }

	/// <summary>
	///   Пересчитывает координаты модели согласно матрице трансформаций
	/// </summary>
        private void CalcVertexes()
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                Vector3 vector = new Vector3(vertices[i].x, vertices[i].y);
                Vector3 v = matrix.Mult(vector);
                vertices[i] = new Vertex { x = v.vect[0], y = v.vect[1] };
            }
        }
    }
}
