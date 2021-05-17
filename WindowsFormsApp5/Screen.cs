using GraphicsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsLibrary
{
    /// <summary>
    ///   Класс главного экрана для графики. Поддерживает мировые координаты.
    /// </summary>
    public class Screen : Canvas
    {
	// Окно для преобразования мировых координат в экранные
	/// <summary>
	///   Левая плоскость окна
	/// </summary>
        double left;
	/// <summary>
	///   Правая плоскость окна
	/// </summary>
        double right;
	/// <summary>
	///   Нижняя плоскость окна
	/// </summary>
        double bottom;
	/// <summary>
	///   Верхняя плоскость окна
	/// </summary>
        double top;

	/// <summary>
	///   Создает экран. Окно устанавливается как экранные координаты, с началом координат - левый нижний угол
	/// </summary>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        /// <param name="bpp">бит на пиксель</param>
        public Screen(int width, int height, int bpp) : base(width, height, bpp)
        {
            left = 0;
            right = width;
            top = 0;
            bottom = height;
        }

	/// <summary>
	///   Устанавливает новое окно преобразования мировых координат в экранные
	/// </summary>
        /// <param name="left">левая плоскость</param>
        /// <param name="right">правая плоскость</param>
        /// <param name="bottom">нижняя плоскость</param>
        /// <param name="top">верхняя плоскость</param>
        public void SetWindow(double left, double right, double bottom, double top)
        {
            this.left = left;
            this.right = right;
            this.bottom = bottom;
            this.top = top;
        }

	/// <summary>
	///   Преобразует мировые координаты в экранные
	/// </summary>
        /// <param name="xw">мировая координата x</param>
        /// <param name="yw">мировая координата y</param>
        /// <param name="x">экранная координата x</param>
        /// <param name="y">экранная координата y</param>
        public void ToScreen(double xw, double yw, out int x, out int y)
        {
            x = Convert.ToInt32((xw - left) / (right - left) * width);
            y = height - Convert.ToInt32((yw - bottom) / (top - bottom) * height);
        }

	/// <summary>
	///   Рисует линию в мировых координатах
	/// </summary>
        /// <param name="xw1">координата x начальной точки</param>
        /// <param name="yw1">координата y начальной точки</param>
        /// <param name="xw2">координата x конечной точки</param>
        /// <param name="yw2">координата y конечной точки</param>
        /// <param name="color">цвет линии</param>
        public void DrawLine(double xw1, double yw1, double xw2, double yw2, int color)
        {
            int x1 = 0;
            int x2 = 0;
            int y1 = 0;
            int y2 = 0;
            ToScreen(xw1, yw1, out x1, out y1);
            ToScreen(xw2, yw2, out x2, out y2);
            base.DrawLine(x1, y1, x2, y2, color);
        }

	/// <summary>
	///   Рисует модель контура полигона
	/// </summary>
        /// <param name="model">модель контура полигона</param>
        public void DrawModel(Model model)
        {
            for (int i = 0; i < model.vertices.Count; i++)
            {
                if (i == model.vertices.Count - 1)
                {
                    DrawLine(
                        model.vertices[i].x,
                        model.vertices[i].y,
                        model.vertices[0].x,
                        model.vertices[0].y,
                        color: model.color);
                }
                else
                {
                    DrawLine(
                       model.vertices[i].x,
                       model.vertices[i].y,
                       model.vertices[i + 1].x,
                       model.vertices[i + 1].y,
                       color: model.color);
                }
            }
        }
    }
}
