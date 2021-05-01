using Zenject;

namespace Bread
{
    public class CreamBread : BreadBase
    {
        public class Factory : PlaceholderFactory<CreamBread>
        {
        }
    }
}