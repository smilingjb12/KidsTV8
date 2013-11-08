namespace KidsTV8
{
    public class Cell
    {
        protected int value;

        public Cell()
        {
            value = 0;
        }

        public virtual void Write(int value)
        {
            this.value = value;
        }

        public virtual int Read()
        {
            return value;
        }

        public override string ToString()
        {
            if (value == 0) return ".";
            return value.ToString();
        }

        public void InvertValue()
        {
            value = value == 1 ? 0 : 1;
        }
    }
}