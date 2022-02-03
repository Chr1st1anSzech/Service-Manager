using Dienste_Verwaltung.src.DataModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dienste_Verwaltung.src.Selectors
{
    public class GroupServiceItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate GroupTemplate { get; set; }
        public DataTemplate ServiceTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is ServiceGroup group)
            {
                return GroupTemplate;
            }
            else
            {
                return ServiceTemplate;
            }
        }
    }
}
