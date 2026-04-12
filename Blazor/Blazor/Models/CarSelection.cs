namespace Blazor.Models
{
    public class CarSelection
    {
        public event Action? Changed;

        private Brand? brand;
        public Brand? Brand
        {
            get => brand;
            set
            {
                if (brand == value) return;
                brand = value;
                Changed?.Invoke();
            }
        }
    }
}