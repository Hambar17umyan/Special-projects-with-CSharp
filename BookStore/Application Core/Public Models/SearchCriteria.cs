using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Core.Public_Models
{
    public enum SortingCriteria
    {
        PriceAtoZ,
        PriceZtoA,
        TitleAtoZ,
        TitleZtoA,
        Categories,
        ISBN
    }
    public class SearchCriteria
    {
        public bool Sort { get; protected set; }
        public SortingCriteria SortArg { get; protected set; }

        public bool IsAuthor { get; protected set; }
        public string? IsAuthorArg { get; protected set; }

        public bool TitleConsistsOf { get; protected set; }
        public string? TitleConsistsOfArg { get; protected set; }

        public bool DescriptionConsistsOf { get; protected set; }
        public string? DescriptionConsistsOfArg { get; protected set; }

        public bool CategoriesAre { get; protected set; }
        public BookCategory CategoriesAreArg { get; protected set; }

        public bool PriceIsHigherThan { get; protected set; }
        public decimal PriceIsHigherThanArg { get; protected set; }

        public bool PriceIsLowerThan { get; protected set; }
        public decimal PriceIsLowerThanArg { get; protected set; }

        public void SortByCriteria(SortingCriteria criteria)
        {
            SortArg = criteria;
        }


        public void CheckIfIsAuthor(string name)
        {
            IsAuthor = true;
            IsAuthorArg = name;
        }

        public void CheckIfTitleConsistsOf(string title)
        {
            TitleConsistsOf = true;
            TitleConsistsOfArg = title;
        }

        public void CheckIfDescriptionConsistsOf(string title)
        {
            DescriptionConsistsOf = true;
            DescriptionConsistsOfArg = title;
        }

        public void CheckIfCategoriesAre(params BookCategory[] categories)
        {
            BookCategory category = 0;
            foreach (var a in categories)
                category |= a;

            CategoriesAre = true;
            CategoriesAreArg = category;
        }

        public void CheckIfPriceIsHigherThan(decimal price)
        {
            PriceIsHigherThan = true;
            PriceIsHigherThanArg = price;
        }

        public void CheckIfPriceIsLowerThan(decimal price)
        {
            PriceIsLowerThan = true;
            PriceIsLowerThanArg = price;
        }


    }
}
