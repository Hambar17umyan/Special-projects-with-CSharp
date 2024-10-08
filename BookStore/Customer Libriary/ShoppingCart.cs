﻿using Application_Core.Internal_Models;
using Application_Core.Public_Models;
using System.Collections;

namespace Customer_Libriary
{
    public class ShoppingCart : IEnumerable<(int isbn, uint count)>
    {
        private List<(int isbn, uint count)> _itemISBNs;
        internal ShoppingCart()
        {
            _itemISBNs = new List<(int, uint)>();
        }
        private bool CheckIfItemExists(int isbn)
        {
            string directoryPath = @"..\..\..\..\";
            string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");

            foreach (var item in txtFiles)
            {
                var y = item.Trim().Split('.');
                if (y[0] == isbn.ToString())
                {
                    return true;
                }
            }
            return false;
        }
        internal Message TryAddingToCart(BookToCartDTO book)
        {
            var isbn = book.ISBN;
            var count = book.Count;
            if (CheckIfItemExists(isbn))
            {
                if (Helper.CheckIfThereAreSufficientBooks(isbn, count))
                {
                    for (int i = 0; i < _itemISBNs.Count; i++)
                    {
                        if (_itemISBNs[i].isbn == isbn)
                        {
                            _itemISBNs[i] = (_itemISBNs[i].isbn, _itemISBNs[i].count + 1);
                            return Message.Success;
                        }
                    }
                    _itemISBNs.Add((isbn, 1));
                    return Message.Success;
                }
                else
                {
                    return new Message("Cannot add to cart.", $"There aren't sufficient books with the ISBN number {isbn} in our store!");
                }
            }
            else
            {
                return new Message("Cannot add to cart.", $"There are no books with the ISBN number {isbn} in our store!");
            }
        }
        private bool CheckIfItemIsInCart(int isbn)
        {
            foreach (var i in _itemISBNs)
            {
                if (i.isbn == isbn)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckIfThereAreSufficientBooksInTheCart(int isbn, int count)
        {
            foreach (var i in _itemISBNs)
            {
                if (i.isbn == isbn)
                {
                    if (i.count >= count)
                    {
                        return true;
                    }
                    else return false;
                }
            }

            return true;
        }
        internal Message TryRemovingFromCart(int isbn, int count)
        {
            if (CheckIfItemIsInCart(isbn))
            {
                if (CheckIfThereAreSufficientBooksInTheCart(isbn, count))
                {
                    for(int i = 0; i < _itemISBNs.Count; i++)
                    {
                        if(_itemISBNs[i].isbn == isbn)
                        {
                            if (_itemISBNs[i].count > count)
                            {
                                _itemISBNs[i] = (_itemISBNs[i].isbn,  _itemISBNs[i].count - 1);
                            }
                            else
                            {
                                _itemISBNs.RemoveAt(i);
                            }

                            break;
                        }
                    }
                    return Message.Success;
                }
                else
                {
                    return new Message("Cannot delete from cart.", $"There aren't sufficient books with the ISBN number {isbn} in your cart!");
                }
            }
            else return new Message("Cannot delete from cart.", $"There aren't any books with the ISBN number {isbn} in your cart!");
        }
        public List<BookInCartDTO> GetCartAsList()
        {
            var res = new List<BookInCartDTO>();
            foreach (var item in _itemISBNs)
            {
                res.Add(new BookInCartDTO(item.isbn, item.count));
            }
            return res;
        }
        internal void ClearTheCart()
        {
            _itemISBNs = new List<(int isbn, uint count)> ();
        }
        internal Message CheckIfAllIsOk()
        {
            foreach (var item in _itemISBNs)
            {
                int isbn = item.isbn;
                uint count = item.count;
                if (CheckIfItemExists(isbn))
                {
                    if (Helper.CheckIfThereAreSufficientBooks(isbn, count))
                    {
                        return Message.Success;
                    }
                    else
                    {
                        return new Message("Cannot purchase items.", $"There aren't sufficient books with the ISBN number {isbn} in our store!");
                    }
                }
                else
                {
                    return new Message("Cannot purchase items.", $"There are no books with the ISBN number {isbn} in our store!");
                }

            }
            return new Message("Unhandled case");
        }
        public IEnumerator<(int isbn, uint count)> GetEnumerator()
        {
            foreach (var item in _itemISBNs)
            {
                yield return item;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}