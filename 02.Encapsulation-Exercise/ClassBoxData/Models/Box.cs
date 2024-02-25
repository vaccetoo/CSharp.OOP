
namespace ClassBoxData.Models
{
    public class Box
    {
        private double length;
        private double width;
        private double height;


        public Box(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
        }


        public double Length
        {
            get => length;

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(Length)} cannot be zero or negative.");
                }

                length = value;
            }
        }

        public double Width
        {
            get => width;

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(Width)} cannot be zero or negative.");
                }

                width = value;
            }
        }

        public double Height
        {
            get => height;

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(Height)} cannot be zero or negative.");
                }

                height = value;
            }
        }


        public double SurfaceArea()
            => 2 * Length * Width + LateralSurfaceArea();

        public double LateralSurfaceArea()
            => 2 * Length * Height + 2 * Width * Height;

        public double Volume()
            => Length * Width * Height;

    }
}
