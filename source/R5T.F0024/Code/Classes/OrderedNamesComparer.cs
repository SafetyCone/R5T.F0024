﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace R5T.F0024
{
    public class OrderedNamesComparer : IComparer<string>
    {
        private List<string> OrderedNames { get; } = new List<string>();


        public OrderedNamesComparer(IEnumerable<string> orderedNames)
        {
            this.OrderedNames.AddRange(orderedNames);
        }

        public int Compare(string x, string y)
        {
            var indexOfX = this.OrderedNames.IndexOf(x);
            var indexOfY = this.OrderedNames.IndexOf(y);

            var xWasFound = F0000.Instances.StringOperator.WasFound(indexOfX);
            var yWasFound = F0000.Instances.StringOperator.WasFound(indexOfY);

            if(xWasFound)
            {
                if(yWasFound)
                {
                    return indexOfX.CompareTo(indexOfY);
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if(yWasFound)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
