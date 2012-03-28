﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Routing.Silverlight.Models;
using helpers = Silverlight.Common.Helpers;
using System.Windows.Controls.Primitives;
using Routing.Silverlight.Controls;
using ReactiveUI;
using Routing.Silverlight.Address_Validation;
using System.Diagnostics;

namespace Routing.Silverlight.Views
{
    public partial class CreateScenario : Page
    {
        ScenarioViewModel ViewModel;

        public CreateScenario()
        {
            InitializeComponent();

            DataContext = ViewModel = new ScenarioViewModel();
        }

        private void Search_Address_TextChanged(object sender, global::Silverlight.Common.Controls.TextChangedEventArgs e)
        {
            var control = sender as Control;
            var padri = control.GetVisualAncestors().ToList();

            var point = (sender as Control).DataContext as LocationViewModel;
            //if (point.Destination.IsValid && e.IsSilent)
            //    return;
            //ViewModel.F2_Location(point, popupAncor, e.IsSilent, e.Text);

            var helper = new Address_Validation_Helper(point.Destination, popupAncor);
            helper.Validate_Address(e.IsSilent, e.Text).ContinueWith(d => 
            {
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => { 
                    point.Change_Destination(d.Result);
                });
            });
        }

        private void Destination_TextChanged(object sender, global::Silverlight.Common.Controls.TextChangedEventArgs e)
        {
            var point = (sender as Control).DataContext as LocationViewModel;
            //if (point == null || point.Destination.IsValid && e.IsSilent)
            //    return;
            ViewModel.F2_Destination(point,e.IsSilent, e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Carica();
        }

        private void Import_Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Importa();
        }
 

    }
}
