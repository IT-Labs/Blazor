using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages
{
    public partial class Counter
    {
        private int currentCount = 0;

        public void IncrementCount()
        {
            currentCount++;
        }
    }
}
