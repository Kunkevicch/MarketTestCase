using System;

namespace MarketTestCase
{
    public interface IPicker
    {
        public event Action ItemPicked;
        public event Action ItemThrowed;
    }
}
