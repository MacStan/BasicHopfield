using System;
using System.Collections.Generic;

namespace TspHopfield
{
    using Containers;

    //using NLog;

    public class Network
    {
        //private static Logger logger = LogManager.GetCurrentClassLogger();

        private int A { get; } = 500;
        private int B { get; } = 500;
        private int C { get; } = 200;
        private int D { get; } = 500;
        private double U0 { get; } = 0.02;
        private double Tau { get; } = 1;
        private double Timestep { get; } = 0.000001;

        private double Bias { get => C * (size * 1.5); }

        private double[,] distances;
        private double[,] States { get; set; }

        private Random Rng { get; } = new Random();

        private int size = 0;
        private WeightsContainer weights;

        public Network(int size, double[,] distances)
        {
            //logger.Info($"Creating network size: {size};");
            this.distances = distances;
            this.size = size;
            States = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    States[i, j] = 1;
                    States[i, j] /= size;
                    States[i, j] /= size;

                    var a = (Rng.NextDouble()) / 50;
                    States[i, j] += Rng.NextDouble() > 0.5 ? a : -a;
                }
            }
        }

        public void PrintWeights()
        {
            Console.WriteLine("WEIGHTS");
            for (int c1 = 0; c1 < size; c1++)
            {
                Console.WriteLine($"From {c1}");
                for (int p1 = 0; p1 < size; p1++)
                {
                    double weightedSum = 0.0;
                    for (var c2 = 0; c2 < size; c2++)
                    {
                        for (var p2 = 0; p2 < size; p2++)
                        {
                            Console.Write($@"{ weights
                                         .cities[c2]
                                         .positions[p2]
                                         .Cities[c1]
                                         .Position[p1]:-###0.0;+###0.0}  ");
                            weightedSum += weights
                                        .cities[c2]
                                        .positions[p2]
                                        .Cities[c1]
                                        .Position[p1];
                        }
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                    Console.WriteLine($@"{ weightedSum:-###0.0;+###0.0}  ");
                }
                Console.WriteLine();
            }
        }

        public void PrintState()
        {
            for (int city = 0; city < size; city++)
            {
                Console.Write($"SSS ");
                for (int pos = 0; pos < size; pos++)
                {
                    Console.Write($"{States[city, pos]:0.0##} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            for (int city = 0; city < size; city++)
            {
                bool onlyZeros = true;
                for (int pos = 0; pos < size; pos++)
                {
                    onlyZeros = GetActivation(States[city, pos]) == 0 ? onlyZeros : false;
                    Console.Write($"{GetActivation(States[city, pos]):-0.0##;+0.0##} ");
                }
                if (onlyZeros) Console.Write(" FAIL HERE ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void CreateWeights()
        {
            //logger.Info("Creating Weights");

            weights = new WeightsContainer();

            for (int city1 = 0; city1 < size; city1++)
            {
                //  logger.Debug($"City1: {city1}");
                var fstCity = new FstCity();
                for (int pos1 = 0; pos1 < size; pos1++)
                {
                    // logger.Debug($" Pos1: {pos1}");
                    var firstPosition = new Position();
                    for (var city2 = 0; city2 < size; city2++)
                    {
                        // logger.Debug($"  City2: {city2}");
                        var positions = new List<double>();
                        var msg = String.Empty;
                        for (var pos2 = 0; pos2 < size; pos2++)
                        {
                            var weight = CountWeight(city1, pos1, city2, pos2);
                            positions.Add(weight);
                            msg += ($"{weight} ");
                        }
                        // logger.Debug($"    Poss: {msg} ");
                        firstPosition.Cities.Add(
                            new SecCity(positions));
                    }
                    fstCity.positions.Add(firstPosition);
                }
                weights.cities.Add(fstCity);
            }
        }

        private int KD(int a, int b)
        {
            return a == b ? 1 : 0;
        }

        public double CountWeight(int x, int i, int y, int j)
        {
            var a = -A * KD(x, y) * (1 - KD(i, j));
            var b = -B * KD(i, j) * (1 - KD(x, y));
            var c = -C;
            var sup_i = (i - 1) >= 0 ? i : size;

            var d1 = -D;
            var d2 = distances[x, y];
            var d3 = KD(j, (i + 1) % size);
            var d4 = KD(j, sup_i - 1);
            var d = d1 * d2 * (d3 + d4);

            return a + b + c + d;
        }

        public double GetActivation(double input)
        {
            var sigm = 0.5 * (1 + Math.Tanh(input / U0));
            var activ = sigm < 0.2 ? 0 : sigm;
            activ = sigm > 0.8 ? 1 : activ;

            return sigm;
        }

        public void Update()
        {
            var inputsChange = new double[size, size];

            for (int city = 0; city < size; city++)
            {
                for (int position = 0; position < size; position++)
                {
                    inputsChange[city, position] -= Timestep * States[city, position];
                    inputsChange[city, position] += Timestep * Bias;
                    var weightedSum = 0.0;
                    for (int firstCity = 0; firstCity < size; firstCity++)
                    {
                        for (int firstPos = 0; firstPos < size; firstPos++)
                        {
                            if (firstCity != city)
                            {
                                if (position != firstPos)
                                {
                                    weightedSum +=
                                        States[firstCity, firstPos]
                                        * weights
                                        .cities[firstCity]
                                        .positions[firstPos]
                                        .Cities[city]
                                        .Position[position]
                                        * Timestep;
                                }
                            }
                        }
                    }
                    inputsChange[city, position] += weightedSum;
                }
            }
            UpdateInputs(inputsChange);
        }

        public void Update2()
        {
            var statesChange = new double[size, size];
            for (int city = 0; city < size; city++)
            {
                for (int pos = 0; pos < size; pos++)
                {
                    statesChange[city, pos] = Timestep * StateFromMotion(city, pos);
                }
            }
            for (int city = 0; city < size; city++)
            {
                for (int pos = 0; pos < size; pos++)
                {
                    States[city, pos] += statesChange[city, pos];
                }
            }
        }

        public double StateFromMotion(int city, int pos)
        {
            double stateChange =
                -States[city, pos]
                - GetA(city, pos)
                - GetB(city, pos)
                - GetC()
                - GetD(pos, city);

            return stateChange;
        }

        public double GetA(int city, int position)
        {
            double sum = 0.0;
            for (int pos = 0; pos < size; pos++)
            {
                sum += GetActivation(States[city, pos]);
            }
            sum -= GetActivation(States[city, position]);
            sum *= A;
            return sum;
        }

        public double GetB(int mainCity, int position)
        {
            double sum = 0.0;
            for (int city = 0; city < size; city++)
            {
                sum += GetActivation(States[city, position]);
            }
            sum -= GetActivation(States[mainCity, position]);
            sum *= B;
            return sum;
        }

        public double GetC()
        {
            var sum = 0.0;
            for (int city = 0; city < size; city++)
            {
                for (int pos = 0; pos < size; pos++)
                {
                    sum += GetActivation(States[city, pos]);
                }
            }
            sum -= size;
            sum *= C;
            return sum;
        }

        public double GetD(int position, int mainCity)
        {
            var sum = 0.0;
            for (int city = 0; city < size; city++)
            {
                var minPos = position - 1 < 0 ? size - 1 : position - 1;
                var maxPos = position + 1 >= size ? 0 : position + 1;
                sum += distances[mainCity, city] * (
                    GetActivation(States[city, minPos])
                + GetActivation(States[city, maxPos]));
            }

            sum *= D;
            return sum;
        }

        public void UpdateInputs(double[,] statesChange)
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    States[i, j] += Timestep * statesChange[i, j];
        }
    }
}