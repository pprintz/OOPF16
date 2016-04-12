using System;
/// <summary>
/// 20135332
/// Peter Viggo Printz Madsen
/// Eksamens opgave OOP F16
/// </summary>
namespace Eksamensopgave2016
{
    class SeasonalProduct : Product
    {
        public SeasonalProduct(string name, decimal price, int productID) : base(name, price, productID)
        {
        }
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set; }

        private bool _inSeason;
        public bool InSeason
        {
            get
            {
                return _inSeason;
            }
            set
            {
                if (value)
                {
                    SeasonStartDate = DateTime.Now;
                }
                _inSeason = value;
            }
        }

    }
}
