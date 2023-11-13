namespace IRIS.DataFlow.Color
{
    public class DataFlowRGB
    {
        private int Red { get; set; }
        private int Green { get; set; }
        private int Blue { get; set; }

        public DataFlowRGB(int Red, int Green, int Blue)
        {
            this.Red = Red;
            this.Green = Green;
            this.Blue = Blue;
        }

        public DataFlowRGB(double[] normalizedColor)
        {
            this.Red = (int)(normalizedColor[0] * 255);
            this.Green =(int)(normalizedColor[1] * 255);
            this.Blue = (int)(normalizedColor[2] * 255);
        }

        public DataFlowRGB(double RedRatio, double GreenRatio, double BlueRatio)
        {
            this.Red = (int)(RedRatio * 255);
            this.Green = (int)(GreenRatio * 255);
            this.Blue =(int)(BlueRatio * 255);
        }

        public DataFlowRGB(float RedRatio, float GreenRatio, float BlueRatio)
        {
            this.Red = (int)(RedRatio * 255);
            this.Green = (int)(GreenRatio * 255);
            this.Blue = (int)(BlueRatio * 255);
        }

        public double[] Normalize()
        {
            return new double[] {
                (double)this.Red / 255,
                (double)this.Green / 255,
                (double)this.Blue / 255,
            };
        }

        override
        public string ToString()
        {
            return new string(Red + " " + Green + " " + Blue + " ");
        }

        public DataFlowRGB Interpolate(DataFlowRGB flowRGB, double ratio)
        {
            double[] input = flowRGB.Normalize();
            double[] curColor = Normalize();

            if (ratio <= 0.0)
            {
                return this;
            }
            else if (ratio >= 1.0)
            {
                return flowRGB;
            }
            else
            {
                return new DataFlowRGB(
                    curColor[0] + (input[0] - curColor[0] * ratio),
                    curColor[1] + (input[1] - curColor[1] * ratio),
                    curColor[2] + (input[2] - curColor[2] * ratio)
                    );
            }
        }
    }
}
