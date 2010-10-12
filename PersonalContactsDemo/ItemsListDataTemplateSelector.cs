using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using Caliburn.Micro;
using PersonalContactsDemo.ViewModels;

namespace PersonalContactsDemo
{
    public class ItemsListDataTemplateSelector:DataTemplateSelector
    {

        public override DataTemplate  SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is Screen)
            {


                if (item is SearchScreenViewModel)
                {
                    return element.FindResource("SearchScreenTemplate") as DataTemplate;
                }

                if (item is ExplorePersonContactViewModel)
                {
                    return element.FindResource("ExploringPersonContactTemplate") as DataTemplate;
                }

                if (item is AddPersonContactViewModel)
                {
                    return element.FindResource("AddingPersonContactTemplate") as DataTemplate;
                }

            }

            return null;
        }
    }
}
