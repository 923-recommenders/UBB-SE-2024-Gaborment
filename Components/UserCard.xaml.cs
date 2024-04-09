﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UBB_SE_2024_Gaborment.Components;

namespace UBB_SE_2024_Gaborment
{
    /// <summary>
    /// Interaction logic for UserCard.xaml
    /// </summary>
    public partial class UserCard : UserControl
    {
        public UserCard()
        {
            InitializeComponent();
        }
        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        public static readonly DependencyProperty FirstNameProperty = DependencyProperty.Register("FirstName", typeof(string), typeof(UserCard));

        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        public static readonly DependencyProperty LastNameProperty = DependencyProperty.Register("LastName", typeof(string), typeof(UserCard));
    }
}
