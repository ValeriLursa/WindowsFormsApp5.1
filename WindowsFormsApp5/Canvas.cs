using System;

namespace GraphicsLibrary
{
    /// <summary>
    /// Холст - изображение в памяти
    /// </summary>
    public class Canvas
    {
        /// <summary>
        /// Ширина изображения (строки)
        /// </summary>
        public int width;
        /// <summary>
        /// Высота изображения (число строк)
        /// </summary>
        public int height;
        /// <summary>
        /// Число бит на пиксель
        /// </summary>
        public int bpp;
        /// <summary>
        /// Данные изображения (Пиксели хранятся построчно)
        /// </summary>
        public int[] data;
        public int[] dataPriz;
        /// <summary>
        /// Создает новый холст
        /// </summary>
        /// <param name="width">ширина</param>
        /// <param name="height">высота</param>
        /// <param name="bpp">бит на пиксель</param>
        public Canvas(int width, int height, int bpp)
        {
            this.width = width;
            this.height = height;
            this.bpp = bpp;
            data = new int[width * height];
            dataPriz = new int[width * height];
        }

        /// <summary>
        /// Рисует точку на холсте, отсекает (не рисует) точки не попадающие на холст
        /// </summary>
        /// <param name="x">координата x</param>
        /// <param name="y">координата y</param>
        /// <param name="color">цвет точки</param>
        public void DrawPixel(int x, int y, int color)
        {
            if (0 <= x && x < width)
                if (0 <= y && y < height)
                    data[x + y * width] = color;
        }
        public void Draw(int x, int y, int color)
        {
            if (0 <= x && x < width)
                if (0 <= y && y < height)
                    dataPriz[x + y * width] = color;
        }
        /// <summary>
        /// Рисует другой холст на холсте (поверх)
        /// </summary>
        /// <param name="a_x">координата x</param>
        /// <param name="a_y">координата y</param>
        /// <param name="a_canvas">холст, который будет нарисован</param>
        public void DrawCanvasCol(int a_x, int a_y, Canvas a_canvas, int col)
        {
            if (a_canvas is null)
            {
                throw new ArgumentNullException(paramName: nameof(a_canvas));
            }

            for (int x = a_x; x < a_canvas.width; x++)
                for (int y = a_y; y < a_canvas.height; y++)
                //DrawPixel(x, y, a_canvas.data[x - a_x + ((y - a_y) * a_canvas.width)]);
                {
                    a_canvas.data[x + y * a_canvas.width] = col;
                    DrawPixel(x, y, a_canvas.data[x+ y * a_canvas.width]);
                        }
        }

        /// <summary>
        /// Рисует холст с прозрачностью на холсте
        /// </summary>
        /// <param name="a_x">координата x</param>
        /// <param name="a_y">координата y</param>
        /// <param name="a_canvas">холст, который будет нарисован</param>
        /// <param name="transp_color">цвет прозрачных точек</param>
        public void DrawCanvas(int a_x, int a_y, Canvas a_canvas, int transp_color)
        {
            if (a_canvas is null)
            {
                throw new ArgumentNullException(paramName: nameof(a_canvas));
            }

            for (int x = a_x; x < a_canvas.width; x++)
                for (int y = a_y; y < a_canvas.height; y++)
                {
                    int col = a_canvas.data[x + y * a_canvas.width];
                    if (!transp_color.Equals(col)) Draw(x, y, col);
                }
        }

        /// <summary>
        /// Рисует линию на холсте
        /// </summary>
        /// <param name="x1">координата x начальной точки</param>
        /// <param name="y1">координата y начальной точки</param>
        /// <param name="x2">координата x конечной точки</param>
        /// <param name="y2">координата y конечной точки</param>
        /// <param name="color">цвет линии</param>
        public void DrawLine(int x1, int y1, int x2, int y2, int color)
        {
            int deltax = x2 - x1;
            int deltay = y2 - y1;
            int dir = deltay > 0 ? 1 : -1;
            int y = y1 - dir;
            int x = x1;
            if (deltax == 0) // вертикальная линия
            {
                while (true)
                {
                    y += dir;
                    DrawPixel(x1, y, color);
                    if (y == y2) break;
                }

            }
            else if (deltay == 0) // горизонтальная линия
            {
                dir = deltax > 0 ? 1 : -1;
                x -= dir;
                while (true)
                {
                    x += dir;
                    DrawPixel(x, y1, color);
                    if (x == x2) break;
                }
            }
            else
            {
                float k = Convert.ToSingle(deltay) / deltax;
                float xx = x1;
                float yy = y1;
                if (Math.Abs(k) <= 1.0f) // линия, протяженная по x
                    for (x = 0; x <= Math.Abs(deltax); x++)
                    {
                        DrawPixel(Convert.ToInt32(xx), Convert.ToInt32(yy), color);
                        xx += dir;
                        yy += k * dir;
                    }
                else  // линия, протяженная по y
                {
                    dir = deltax > 0 ? 1 : -1;
                    for (y = 0; y <= Math.Abs(deltay); y++)
                    {
                        DrawPixel(Convert.ToInt32(xx), Convert.ToInt32(yy), color);
                        yy += dir;
                        xx += (1.0f / k) * dir;
                    }
                }
            }
        }

        /// <summary>
        /// Рисует прямоугольник на холсте
        /// </summary>
        /// <param name="x1">координата x левого верхего угла</param>
        /// <param name="y1">координата y левого верхего угла</param>
        /// <param name="x2">координата x правого нижнего угла</param>
        /// <param name="y2">координата y правого нижнего угла</param>
        /// <param name="color">цвет границы</param>
        public void Rectangle(int x1, int y1, int x2, int y2, int color)
        {
            for (int x = x1; x <= x2; x++)
            {
                DrawPixel(x, y1, color);
                DrawPixel(x, y2, color);
            }
            for (int y = y1; y <= y2; y++)
            {
                DrawPixel(x1, y, color);
                DrawPixel(x2, y, color);
            }
        }

        /// <summary>
        /// Рисует заполненный прямоугольник на холсте
        /// </summary>
        /// <param name="x1">координата x левого верхего угла</param>
        /// <param name="y1">координата y левого верхего угла</param>
        /// <param name="x2">координата x правого нижнего угла</param>
        /// <param name="y2">координата y правого нижнего угла</param>
        /// <param name="color">цвет заполнения</param>
        public void FillRectangle(int x1, int y1, int x2, int y2, int color)
        {
            for (int x = x1; x <= x2; x++)
                for (int y = y1; y <= y2; y++)
                    DrawPixel(x, y, color);
        }
    }
}

