using AudioVisualizerWinFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MachineLearning
{
    class NeuralNet : IComparable
    {
        private int[] layers;
        private int[] activations;
        private double[][] neurons;
        private double[][] biases;
        private double[][][] weights;

        public double[] Output
        {
            get
            {
                return neurons[neurons.Length - 1];
            }
        }

        public double Fitness { get; set; }

        public NeuralNet (int[] layers)
        {
            this.layers = new int[layers.Length];
            for (int i = 0; i < layers.Length; i++)
            {
                this.layers[i] = layers[i];
            }
            InitNeurons();
            InitBiases();
            InitWeights();
        }

        private void InitWeights(bool random = true)
        {
            Random r = new Random();
            List<double[][]> weightsList = new List<double[][]>();
            for (int i = 1; i < layers.Length; i++)
            {
                List<double[]> layerWeightsList = new List<double[]>();
                int neuronsInPrevLayer = layers[i - 1];
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    double[] neuronWeights = new double[neuronsInPrevLayer];
                    for (int k = 0; k < neuronsInPrevLayer; k++)
                    {
                        if (random)
                        {
                            neuronWeights[k] = r.NextDouble() - 0.5;
                        }
                        else
                        {
                            neuronWeights[k] = 0;
                        }
                    }
                    layerWeightsList.Add(neuronWeights);
                }
                weightsList.Add(layerWeightsList.ToArray());
            }
            weights = weightsList.ToArray();
        }

        private void InitBiases(bool random = true)
        {
            Random r = new Random();
            List<double[]> biasesList = new List<double[]>();
            foreach (int layer in layers)
            {
                double[] bias = new double[layer];
                for (int i = 0; i < bias.Length; i++)
                {
                    if (random)
                    {
                        bias[i] = r.NextDouble() - 0.5;
                    }
                    else
                    {
                        bias[i] = 0;
                    }
                }

                biasesList.Add(bias);
            }
            biases = biasesList.ToArray();
        }

        private void InitNeurons()
        {
            List<double[]> neuronsList = new List<double[]>();
            foreach (int layer in layers)
            {
                neuronsList.Add(new double[layer]);
            }
            neurons = neuronsList.ToArray();
        }

        public double[] FeedForward(double[] inputs)
        {
            //Set the inputs layer
            for (int i = 0; i < inputs.Length; i++)
            {
                neurons[0][i] = inputs[i];
            }

            //Geed the input layer value forward
            for (int i = 1; i < layers.Length; i++)
            {
                for (int j = 0; j < neurons[i].Length; j++)
                {
                    double value = 0f;
                    for (int k = 0; k < neurons[i - 1].Length; k++)
                    {
                        //Multiply by weights
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }
                    //Add bias
                    neurons[i][j] = Activate(value + biases[i][j]);
                }
            }
            return neurons[neurons.Length - 1];
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            NeuralNet other = obj as NeuralNet;
            if (Fitness < other.Fitness)
                return 1;
            else if (Fitness > other.Fitness)
                return -1;
            else
                return 0;
        }

        private double Activate(double v)
        {
            return Math.Tanh(v);
        }

        public NeuralNet Copy ()
        {
            NeuralNet copy = new NeuralNet(layers);

            //for (int i = 0; i < neurons.Length; i++)
            //{
            //    for (int j = 0; j < neurons[i].Length; j++)
            //    {
            //        copy.neurons[i][j] = neurons[i][j];
            //    }
            //}

            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    copy.biases[i][j] = biases[i][j];
                }
            }

            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        copy.weights[i][j][k] = weights[i][j][k];
                    }
                }
            }

            return copy;
        }

        public void Mutate (double chance, double rate)
        {
            Random r = new Random();
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    if (r.NextDouble() <= chance)
                        biases[i][j] += (r.NextDouble() * 2 - 1) * rate;
                    //biases[i][j] = Math.Min(Math.Max(-2, biases[i][j]), 2);
                }
            }

            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        if (r.NextDouble() <= chance)
                            weights[i][j][k] += (r.NextDouble() * 2 - 1) * rate;
                        //weights[i][j][k] = Math.Min(Math.Max(-1, weights[i][j][k]), 1);
                    }
                }
            }
        }

        public void Save (string path)
        {
            string[] lines = new string[3];

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < layers.Length; i++)
            {
                sb.Append(layers[i]).Append(" ");
            }
            sb.Remove(sb.Length - 1, 1);
            lines[0] = sb.ToString();

            sb = new StringBuilder();
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    sb.Append(biases[i][j]).Append(" ");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            lines[1] = sb.ToString();

            sb = new StringBuilder();
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        sb.Append(weights[i][j][k]).Append(" ");
                    }
                }
            }
            sb.Remove(sb.Length - 1, 1);
            lines[2] = sb.ToString();

            File.WriteAllLines(path, lines);
        }

        public void Load(string path)
        {
            string[] lines = File.ReadAllLines(path);

            string[] line = lines[0].Split(" ");
            layers = new int[line.Length];
            for (int i = 0; i < line.Length; i++)
            {
                if (!int.TryParse(line[i], out layers[i]))
                {
                    layers[i] = 1;
                }
            }

            InitNeurons();
            InitBiases(false);
            InitWeights(false);

            line = lines[1].Split(" ");
            int index = 0;
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    if (!double.TryParse(line[index], out biases[i][j]))
                    {
                        biases[i][j] = 0;
                    }
                    index++;
                }
            }

            line = lines[2].Split(" ");
            index = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        if (!double.TryParse(line[index], out weights[i][j][k]))
                        {
                            weights[i][j][k] = 0;
                        }
                        index++;
                    }
                }
            }
        }
    }
}
