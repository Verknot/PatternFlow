using System;

namespace PatternFlow.QuadraticEquation;

public class QuadraticEquation
{
    private readonly ILogger _logger;

    public QuadraticEquation(ILogger logger)
    {
        _logger = logger;
    }

    public double[] Solve(double a, double b, double c)
    {
        if (double.IsNaN(a) || double.IsNaN(b) || double.IsNaN(c) ||
            double.IsInfinity(a) || double.IsInfinity(b) || double.IsInfinity(c))
        {
            _logger.Log($"Некорректное значение {a}, {b}, {c}");
            throw new ArgumentException($"Некорректное значение {a}, {b}, {c}");
        }

        if (Math.Abs(a) < double.Epsilon)
        {
            _logger.Log("Значение а не может быть равно 0");
            throw new ArgumentException("Значение а не может быть равно 0");
        }

        double discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            _logger.Log("Нет корней уравнения");
            return new double[0];
        }
        else if (discriminant < double.Epsilon)
        {
            double value = -b / (2 * a);
            _logger.Log($"Один корень {value}");
            return new double[] { value };
        }
        else
        {
            double sqrtD = Math.Sqrt(discriminant);
            double value1 = (-b + sqrtD) / (2 * a);
            double value2 = (-b - sqrtD) / (2 * a);
            _logger.Log($"Два корня {value1} и {value2}");
            return new double[] { value1, value2 };
        }
    }
}