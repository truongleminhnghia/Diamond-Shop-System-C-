﻿#pragma checksum "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4380A068554DA16A82464BCC14EDB14148648997"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DiamondShopSystem.WPFApp.UI.OrderDetails;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DiamondShopSystem.WPFApp.UI.OrderDetails {
    
    
    /// <summary>
    /// WSearchOrderDetail
    /// </summary>
    public partial class WSearchOrderDetail : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProductId;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OrderId;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Quantity;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonSearch;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAdd;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid grdOrderDetail;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DiamondShopSystem.WPFApp;component/ui/orderdetails/wsearchorderdetail.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ProductId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.OrderId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.Quantity = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ButtonSearch = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
            this.ButtonSearch.Click += new System.Windows.RoutedEventHandler(this.ButtonSearch_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonAdd = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
            this.ButtonAdd.Click += new System.Windows.RoutedEventHandler(this.ButtonAdd_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 38 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonCloess_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.grdOrderDetail = ((System.Windows.Controls.DataGrid)(target));
            
            #line 43 "..\..\..\..\..\UI\OrderDetails\WSearchOrderDetail.xaml"
            this.grdOrderDetail.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.grdProduct_MouseDouble_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

